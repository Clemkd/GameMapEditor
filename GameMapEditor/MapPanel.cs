using GameMapEditor.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public partial class MapPanel : DockContent, IDisposable
    {
        #region Fields
        private const string UNSAVED_DOCUMENT_MARK = "*";

        private Point mapOrigin;
        private bool isGridActived;
        private bool isTilesetSelectionShowProcessActived;
        private bool isSaved;
        
        private Pen gridColor;
        private Point mouseLocation;

        private Rectangle tilesetSelection;
        private BitmapImage texture;
        private int selectedLayerIndex;

        private GameMap gameMap;
        private Point location;
        private Point oldLocation;
        #endregion

        public MapPanel()
        {
            this.InitializeComponent();
            this.mapOrigin = new Point();
            this.IsGridActived = true;
            this.IsSaved = false;
            this.IsTilesetSelectionShowProcessActived = true;
            this.gridColor = new Pen(Color.FromArgb(255, 130, 130, 130), 2);
            this.mouseLocation = new Point();
            this.location = new Point();
            this.oldLocation = new Point();
            this.selectedLayerIndex = 0;

            this.RefreshScrollComponents();
        }

        #region FrameEvents
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void GameMap_MapChanged(object sender)
        {
            this.IsSaved = false;
            this.picMap.Refresh();
        }

        private void picMap_Resize(object sender, EventArgs e)
        {
            this.RefreshScrollComponents(this.hScrollBarPicMap.Value, this.vScrollBarPicMap.Value);

            // TODO : Réviser
            /**** Centre la map ****/
            if(!this.hScrollBarPicMap.Enabled)
                this.mapOrigin.X = ((GlobalData.TileSize.Width * GlobalData.MapSize.Width) / 2) - (this.picMap.Size.Width / 2);

            if (!this.vScrollBarPicMap.Enabled)
                this.mapOrigin.Y = ((GlobalData.TileSize.Height * GlobalData.MapSize.Height) / 2) - (this.picMap.Size.Height / 2);
            /***********************/
        }

        private void picMap_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.LightGray);
            this.gameMap.Draw(this.mapOrigin, e);
            this.DrawSelection(this.isTilesetSelectionShowProcessActived, e);
            this.DrawGrid(this.isGridActived, e);
        }

        private void picMap_MouseDown(object sender, MouseEventArgs e)
        {
            this.gameMap.SetTiles(this.selectedLayerIndex, this.location.X, this.location.Y, this.texture);
        }

        private void picMap_MouseMove(object sender, MouseEventArgs e)
        {
            this.mouseLocation = e.Location;

            location.X = (int)((e.Location.X + this.mapOrigin.X) / GlobalData.TileSize.Width);
            location.Y = (int)((e.Location.Y + this.mapOrigin.Y) / GlobalData.TileSize.Height);

            // Si le bouton gauche de la souris est actuellement pressé,
            // si la position de la souris à évolué (ref : tile) et
            // s'il existe une selection du tileset et une texture courante pour le tileset
            if (e.Button == MouseButtons.Left &&
                (oldLocation.X != location.X || oldLocation.Y != location.Y) &&
                this.tilesetSelection != null && this.texture != null)
            {
                this.oldLocation.X = this.location.X;
                this.oldLocation.Y = this.location.Y;

                this.gameMap.SetTiles(this.selectedLayerIndex, this.location.X, this.location.Y, this.texture);
            }

            this.picMap.Refresh();
        }

        private void picMap_MouseUp(object sender, MouseEventArgs e)
        {
            // Save gameMap state (Undo / Redo list)
            //Debug.WriteLine(this.gameMap.ToString());
        }

        private void vScrollBarPicMap_Scroll(object sender, ScrollEventArgs e)
        {
            this.mapOrigin.Y = vScrollBarPicMap.Value;
            this.picMap.Refresh();
        }

        private void hScrollBarPicMap_Scroll(object sender, ScrollEventArgs e)
        {
            this.mapOrigin.X = hScrollBarPicMap.Value;
            this.picMap.Refresh();
        }

        private void toolStripBtnGrid_Click(object sender, EventArgs e)
        {
            this.IsGridActived = !this.IsGridActived;
        }

        private void toolStripBtnTilesetSelection_Click(object sender, EventArgs e)
        {
            this.IsTilesetSelectionShowProcessActived = !this.IsTilesetSelectionShowProcessActived;
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Ouvre un nouveau document de map
        /// </summary>
        /// <param name="dockPanel">Le panel de gestion du document</param>
        /// <param name="mapPanels">La liste des documents de map</param>
        /// <param name="tilesetImage">La texture du tileset courant</param>
        /// <param name="tilesetSelection">La selection du tileset courant</param>
        public static MapPanel OpenNewDocument(DockPanel dockPanel, List<MapPanel> mapPanels, BitmapImage tilesetImage, Rectangle tilesetSelection, string mapName)
        {
            dockPanel.SuspendLayout(true);
            MapPanel mapPanel = new MapPanel();

            mapPanel.Texture = tilesetImage;
            mapPanel.TilesetSelection = tilesetSelection;
            mapPanel.Create(mapName);

            mapPanel.Show(dockPanel);
            mapPanel.DockState = DockState.Document;
            mapPanel.Dock = DockStyle.Fill;

            mapPanels.Add(mapPanel);

            dockPanel.ResumeLayout(true, true);

            return mapPanel;
        }

        /// <summary>
        /// Ouvre un nouveau document de map
        /// </summary>
        /// <param name="dockPanel">Le panel de gestion du document</param>
        /// <param name="mapPanels">La liste des documents de map</param>
        /// <param name="tilesetImage">La texture du tileset courant</param>
        /// <param name="tilesetSelection">La selection du tileset courant</param>
        public static MapPanel OpenNewDocument(DockPanel dockPanel, List<MapPanel> mapPanels, BitmapImage tilesetImage, Rectangle tilesetSelection, GameMap map)
        {
            dockPanel.SuspendLayout(true);
            MapPanel mapPanel = new MapPanel();

            mapPanel.Texture = tilesetImage;
            mapPanel.TilesetSelection = tilesetSelection;
            mapPanel.Open(map);

            mapPanel.Show(dockPanel);
            mapPanel.DockState = DockState.Document;
            mapPanel.Dock = DockStyle.Fill;

            mapPanels.Add(mapPanel);

            dockPanel.ResumeLayout(true, true);

            return mapPanel;
        }

        /// <summary>
        /// Remplis la map par la texture selectionnée du tileset
        /// </summary>
        public void Fill()
        {
            if (this.texture != null)
            {
                this.gameMap.Fill(this.selectedLayerIndex, this.texture);
                this.picMap.Refresh();
            }
        }

        public void Save()
        {
            this.Map.Save();
            this.IsSaved = true;
        }

        public void Open(GameMap map)
        {
            this.Map = map;
            this.Map.MapChanged += GameMap_MapChanged;
            this.IsSaved = true;
        }

        public void Create(string mapName)
        {
            this.Map = new GameMap(mapName);
            this.Map.MapChanged += GameMap_MapChanged;
            this.IsSaved = false;
        }

        /// <summary>
        /// Dessine la grille si l'état donné est vrai
        /// </summary>
        /// <param name="state">L'état de dessin</param>
        /// <param name="e">Les données de dessin</param>
        private void DrawGrid(bool state, PaintEventArgs e)
        {
            if (!state) return;

            for (int x = 0; x < GlobalData.MapSize.Width + 1; x++)
                e.Graphics.DrawLine(this.GridColor,
                    x * GlobalData.TileSize.Width - this.mapOrigin.X,
                    -this.mapOrigin.Y,
                    x * GlobalData.TileSize.Width - this.mapOrigin.X,
                    GlobalData.MapSize.Height * GlobalData.TileSize.Height - this.mapOrigin.Y);

            for (int y = 0; y < GlobalData.MapSize.Height + 1; y++)
                e.Graphics.DrawLine(this.GridColor,
                    -this.mapOrigin.X,
                    y * GlobalData.TileSize.Height - this.mapOrigin.Y,
                    GlobalData.MapSize.Width * GlobalData.TileSize.Width - this.mapOrigin.X,
                    y * GlobalData.TileSize.Height - this.mapOrigin.Y);
        }

        /// <summary>
        /// Dessine la selection du tileset
        /// </summary>
        /// <param name="state">L'état de dessin</param>
        /// <param name="e">Les données de dessin</param>
        private void DrawSelection(bool state, PaintEventArgs e)
        {
            if (!state || this.tilesetSelection == null || this.texture == null) return;

            int xLocation = (int)((this.mouseLocation.X + this.mapOrigin.X) / GlobalData.TileSize.Width);
            int yLocation = (int)((this.mouseLocation.Y + this.mapOrigin.Y) / GlobalData.TileSize.Height);

            if (xLocation >= 0 && xLocation < GlobalData.MapSize.Width && yLocation >= 0 && yLocation < GlobalData.MapSize.Height)
            {
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetOpacity(0.5f);

                e.Graphics.DrawImage(this.texture.BitmapSource,
                    new Rectangle(
                        xLocation * GlobalData.TileSize.Width - this.mapOrigin.X,
                        yLocation * GlobalData.TileSize.Height - this.mapOrigin.Y,
                        this.tilesetSelection.Width,
                        this.tilesetSelection.Height),
                        this.tilesetSelection.Location.X,
                        this.tilesetSelection.Location.Y,
                        this.tilesetSelection.Width,
                        this.tilesetSelection.Height,
                        GraphicsUnit.Pixel,
                        attributes);
            }
        }

        /// <summary>
        /// Met à jour les données des scrollbars
        /// </summary>
        private void RefreshScrollComponents(int scrollX = 0, int scrollY = 0)
        {
            this.hScrollBarPicMap.Enabled = this.picMap.Size.Width <= GlobalData.TileSize.Width * GlobalData.MapSize.Width;
            this.vScrollBarPicMap.Enabled = this.picMap.Size.Height <= GlobalData.TileSize.Height * GlobalData.MapSize.Height;

            this.vScrollBarPicMap.Minimum = 0;
            this.vScrollBarPicMap.SmallChange = GlobalData.MapSize.Height * GlobalData.TileSize.Height / 20;
            this.vScrollBarPicMap.LargeChange = GlobalData.MapSize.Height * GlobalData.TileSize.Height / 5;
            int scrollHeightValue = GlobalData.MapSize.Height * GlobalData.TileSize.Height + 100 - this.picMap.Size.Height;
            this.vScrollBarPicMap.Maximum = scrollHeightValue > 0 ? scrollHeightValue : 1;
            this.vScrollBarPicMap.Maximum += this.vScrollBarPicMap.LargeChange;
            this.vScrollBarPicMap.Value = scrollY < this.vScrollBarPicMap.Maximum ? scrollY : this.vScrollBarPicMap.Maximum;

            this.hScrollBarPicMap.Minimum = 0;
            this.hScrollBarPicMap.SmallChange = GlobalData.MapSize.Width * GlobalData.TileSize.Width / 20;
            this.hScrollBarPicMap.LargeChange = GlobalData.MapSize.Width * GlobalData.TileSize.Width / 5;
            int scrollWidthValue = GlobalData.MapSize.Width * GlobalData.TileSize.Width + 100 - this.picMap.Size.Width;
            this.hScrollBarPicMap.Maximum = scrollWidthValue > 0 ? scrollWidthValue : 1;
            this.hScrollBarPicMap.Maximum += this.hScrollBarPicMap.LargeChange;
            this.hScrollBarPicMap.Value = scrollX < this.hScrollBarPicMap.Maximum ? scrollX : this.hScrollBarPicMap.Maximum;

            this.picMap.Refresh();
        }

        public new void Dispose()
        {
            this.Map.MapChanged -= GameMap_MapChanged;
            this.gridColor.Dispose();
            base.Dispose(true);
        }
        #endregion

        #region Properties
        public bool IsGridActived
        {
            get { return this.isGridActived; }
            set
            {
                this.isGridActived = value;
                this.toolStripBtnGrid.Checked = this.IsGridActived;
                this.picMap.Refresh();
            }
        }

        public Pen GridColor
        {
            get { return this.gridColor ?? Pens.Black; }
            set { this.gridColor = value; this.picMap.Refresh(); }
        }

        public bool IsTilesetSelectionShowProcessActived
        {
            get { return this.isTilesetSelectionShowProcessActived; }
            set
            {
                this.isTilesetSelectionShowProcessActived = value;
                this.toolStripBtnTilesetSelection.Checked = this.IsTilesetSelectionShowProcessActived;
                this.picMap.Refresh();
            }
        }

        public Rectangle TilesetSelection
        {
            set { this.tilesetSelection = value; }
        }

        public BitmapImage Texture
        {
            set { this.texture = value; }
        }

        public int SelectedLayerIndex
        {
            get { return this.selectedLayerIndex; }
            set { this.selectedLayerIndex = value; }
        }

        public GameMap Map
        {
            get { return this.gameMap; }
            private set { this.gameMap = value; }
        }

        public bool IsSaved
        {
            get { return this.isSaved; }
            private set { this.isSaved = value; this.Text = (this.isSaved ? string.Empty : UNSAVED_DOCUMENT_MARK) + this.gameMap?.Name; }
        }

        public new string Name
        {
            get { return this.Map.Name; }
        }
        #endregion
    }
}
