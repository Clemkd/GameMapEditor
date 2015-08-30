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
    public class MapPanel : DockContent
    {
        private ToolStrip ToolStrip;
        private ToolStripButton toolStripBtnGrid;
        private PictureBox picMap;

        private Point mapOrigin;
        private bool isGridActived;
        private bool isTilesetSelectionShowProcessActived;
        private VScrollBar vScrollBarPicMap;
        private HScrollBar hScrollBarPicMap;
        private Pen gridColor;
        private Point mouseLocation;

        private Rectangle tilesetSelection;
        private ToolStripButton toolStripBtnTilesetSelection;
        private BitmapImage texture;

        private GameMap gameMap;
        private Point location;
        private Point oldLocation;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapPanel));
            this.picMap = new System.Windows.Forms.PictureBox();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnTilesetSelection = new System.Windows.Forms.ToolStripButton();
            this.vScrollBarPicMap = new System.Windows.Forms.VScrollBar();
            this.hScrollBarPicMap = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // picMap
            // 
            this.picMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picMap.Location = new System.Drawing.Point(2, 23);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(278, 232);
            this.picMap.TabIndex = 0;
            this.picMap.TabStop = false;
            this.picMap.Paint += new System.Windows.Forms.PaintEventHandler(this.picMap_Paint);
            this.picMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseDown);
            this.picMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseMove);
            this.picMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseUp);
            this.picMap.Resize += new System.EventHandler(this.picMap_Resize);
            // 
            // ToolStrip
            // 
            this.ToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnGrid,
            this.toolStripBtnTilesetSelection});
            this.ToolStrip.Location = new System.Drawing.Point(0, 275);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ToolStrip.Size = new System.Drawing.Size(300, 25);
            this.ToolStrip.TabIndex = 1;
            // 
            // toolStripBtnGrid
            // 
            this.toolStripBtnGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnGrid.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnGrid.Image")));
            this.toolStripBtnGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnGrid.Name = "toolStripBtnGrid";
            this.toolStripBtnGrid.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnGrid.Text = "Afficher / Cacher la grille";
            this.toolStripBtnGrid.Click += new System.EventHandler(this.toolStripBtnGrid_Click);
            // 
            // toolStripBtnTilesetSelection
            // 
            this.toolStripBtnTilesetSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnTilesetSelection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnTilesetSelection.Image")));
            this.toolStripBtnTilesetSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnTilesetSelection.Name = "toolStripBtnTilesetSelection";
            this.toolStripBtnTilesetSelection.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnTilesetSelection.Text = "Afficher / Cacher la selection du tileset";
            this.toolStripBtnTilesetSelection.Click += new System.EventHandler(this.toolStripBtnTilesetSelection_Click);
            // 
            // vScrollBarPicMap
            // 
            this.vScrollBarPicMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBarPicMap.Location = new System.Drawing.Point(283, 23);
            this.vScrollBarPicMap.Name = "vScrollBarPicMap";
            this.vScrollBarPicMap.Size = new System.Drawing.Size(17, 241);
            this.vScrollBarPicMap.TabIndex = 2;
            this.vScrollBarPicMap.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBarPicMap_Scroll);
            // 
            // hScrollBarPicMap
            // 
            this.hScrollBarPicMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBarPicMap.Location = new System.Drawing.Point(2, 258);
            this.hScrollBarPicMap.Name = "hScrollBarPicMap";
            this.hScrollBarPicMap.Size = new System.Drawing.Size(281, 17);
            this.hScrollBarPicMap.TabIndex = 3;
            this.hScrollBarPicMap.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarPicMap_Scroll);
            // 
            // MapFrame
            // 
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.picMap);
            this.Controls.Add(this.hScrollBarPicMap);
            this.Controls.Add(this.vScrollBarPicMap);
            this.Controls.Add(this.ToolStrip);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Text = "Map";
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public MapPanel()
        {
            this.InitializeComponent();
            this.mapOrigin = new Point();
            this.IsGridActived = true;
            this.IsTilesetSelectionShowProcessActived = true;
            this.gridColor = new Pen(Color.FromArgb(255, 170, 170, 170), 2);
            this.mouseLocation = new Point();
            this.location = new Point();
            this.oldLocation = new Point();
            this.gameMap = new GameMap();
            this.RefreshScrollComponents();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #region FrameEvents
        private void picMap_Resize(object sender, EventArgs e)
        {
            this.RefreshScrollComponents(this.hScrollBarPicMap.Value, this.vScrollBarPicMap.Value);

            /**** Centre la map ****/
            /*if (!this.hScrollBarPicMap.Enabled && !this.vScrollBarPicMap.Enabled)
            {
                this.mapOrigin = new Point(
                    ((GlobalData.TileSize.Width * GlobalData.MapSize.Width) / 2) - (this.picMap.Size.Width / 2),
                    ((GlobalData.TileSize.Height * GlobalData.MapSize.Height) / 2) - (this.picMap.Size.Height / 2));
            }*/
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
            this.gameMap.SetTiles(this.location.X, this.location.Y, this.texture);
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

                this.gameMap.SetTiles(this.location.X, this.location.Y, this.texture);
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
            mapPanel.MapName = mapName;

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
            if (this.tilesetSelection != null && this.texture != null)
            {
                this.gameMap.Fill(this.texture);
                this.picMap.Refresh();
            }
        }

        public void SaveMap()
        {
            this.gameMap.Save();
        }

        public void LoadMap(GameMap map)
        {
            this.gameMap = map;
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

        public string MapName
        {
            get { return this.gameMap.Name; }
            set { this.gameMap.Name = value; this.Text = value; }
        }
        #endregion
    }
}
