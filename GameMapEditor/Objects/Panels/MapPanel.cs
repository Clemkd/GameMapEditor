using GameMapEditor.Objects;
using GameMapEditor.Objects.Class.GameCharacter;
using GameMapEditor.Objects.Enumerations;
using GameMapEditor.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public partial class MapPanel : DockContent
    {
        #region Fields
        private const string UNSAVED_DOCUMENT_MARK = "*";
        private static new GameVector2 Margin = new GameVector2(100, 100);
        private static Pen GridPenColor = new Pen(Color.FromArgb(255, 130, 130, 130), 1);
        private static Cursor EraseCursor = new Cursor(Resources.eraser.GetHicon());
        private static GameCharacter Character = new GameCharacter();

        private GameVector2 mapOrigin;
        private bool isGridActived;
        private bool isTilesetSelectionShowProcessActived;
        private bool isSaved;
        private GameEditorState state;
        
        private Pen gridColor;
        private Point mouseLocation;

        private TextureInfo texture;
        private int selectedLayerIndex;

        private GameMap gameMap;
        private GameVector2 location;
        private Point oldLocation;

        private UndoRedoManager<MemoryStream> undoRedoManager;
        private bool mouseReleased;

        public delegate void UndoRedoUpdatedEventArgs(object sender, UndoRedoManager<MemoryStream> manager);
        public event UndoRedoUpdatedEventArgs UndoRedoUpdated;
        #endregion

        public MapPanel(TextureInfo tilesetInfo, string mapName)
        {
            this.Initialize();
            this.TextureInfo = tilesetInfo;
            this.Create(mapName);
        }

        public MapPanel(TextureInfo tilesetImage, GameMap map)
        {
            this.Initialize();
            this.TextureInfo = tilesetImage;
            this.Open(map);
        }

        /// <summary>
        /// Initialise tous les attributs de l'objet et créer les liens
        /// </summary>
        private void Initialize()
        {
            this.InitializeComponent();
            this.HideOnClose = false;
            this.mapOrigin = new GameVector2();
            this.IsGridActived = true;
            this.IsSaved = false;
            this.IsTilesetSelectionShown = true;
            this.State = GameEditorState.Default;
            this.gridColor = GridPenColor;
            this.mouseLocation = new Point();
            this.location = new GameVector2();
            this.oldLocation = new Point();
            this.selectedLayerIndex = 0;
            this.mouseReleased = true;

            this.undoRedoManager = new UndoRedoManager<MemoryStream>();
            this.undoRedoManager.UndoHappened += UndoRedoSystem_UndoHappened;
            this.undoRedoManager.RedoHappened += UndoRedoSystem_RedoHappened;

            this.RefreshScrollComponents();
        }

        /// <summary>
        /// Évènement appelé lorsqu'un Redo a été effectué
        /// </summary>
        private void UndoRedoSystem_RedoHappened(object sender, MemoryStream e)
        {
            this.Map = GameMap.Load(e);
            this.Map.MapChanged += GameMap_MapChanged;
            this.picMap.Refresh();
        }

        /// <summary>
        /// Évènement appelé lorsqu'un Undo a été effectué
        /// </summary>
        private void UndoRedoSystem_UndoHappened(object sender, MemoryStream e)
        {
            this.Map = GameMap.Load(e);
            this.Map.MapChanged += GameMap_MapChanged;
            this.picMap.Refresh();
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
            this.RefreshScrollComponents();

            if(!this.hScrollBarPicMap.Enabled)
                this.mapOrigin.X = ((GlobalData.TileSize.Width * GlobalData.MapSize.Width) / 2) - (this.picMap.Size.Width / 2);

            if (!this.vScrollBarPicMap.Enabled)
                this.mapOrigin.Y = ((GlobalData.TileSize.Height * GlobalData.MapSize.Height) / 2) - (this.picMap.Size.Height / 2);

            this.Refresh();
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
            if (this.mouseReleased)
            {
                this.undoRedoManager.Add(this.Map.CopyToMemoryStream());
                this.UndoRedoUpdated?.Invoke(this, this.undoRedoManager);
            }
            this.mouseReleased = false;

            this.DoTileEdition(e);
        }

        private void picMap_MouseMove(object sender, MouseEventArgs e)
        {
            this.mouseLocation = e.Location;
            this.location = this.GetTilePosition(e.Location.X + this.mapOrigin.X, e.Location.Y + this.mapOrigin.Y);

            // Si la position de la souris à évolué (ref : tile)
            if (oldLocation.X != location.X || oldLocation.Y != location.Y)
            {
                this.oldLocation.X = this.location.X;
                this.oldLocation.Y = this.location.Y;

                this.DoTileEdition(e);
            }

            this.picMap.Refresh();
        }

        /// <summary>
        /// Réalise la modification des données de la carte selon le model d'édition donné
        /// </summary>
        /// <param name="e">L'évènement souris</param>
        private void DoTileEdition(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || (e.Button == MouseButtons.Left && this.State == GameEditorState.Erase))
            {
                this.gameMap.SetTiles(this.selectedLayerIndex, this.location, null);
                this.Cursor = EraseCursor;
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (this.state != GameEditorState.Erase)
                    this.Cursor = Cursors.Default;

                if (this.texture?.BitmapSelection != null)
                {
                    this.gameMap.SetTiles(this.selectedLayerIndex, this.location, this.texture);
                }
            }
        }

        private void picMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.state != GameEditorState.Erase)
                this.Cursor = Cursors.Default;

            this.mouseReleased = true;
        }

        private void vScrollBarPicMap_Scroll(object sender, ScrollEventArgs e)
        {
            this.mapOrigin.Y = vScrollBarPicMap.Value - Margin.Y;
            this.picMap.Refresh();
        }

        private void hScrollBarPicMap_Scroll(object sender, ScrollEventArgs e)
        {
            this.mapOrigin.X = hScrollBarPicMap.Value - Margin.X;
            this.picMap.Refresh();
        }

        private void toolStripBtnGrid_Click(object sender, EventArgs e)
        {
            this.IsGridActived = !this.IsGridActived;
        }

        private void toolStripBtnTilesetSelection_Click(object sender, EventArgs e)
        {
            this.IsTilesetSelectionShown = !this.IsTilesetSelectionShown;
        }

        private void MapPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.IsSaved)
            {
                DialogResult result = MessageBox.Show(this, "Le document n'a pas été sauvegardé !\nQuitter sans sauvegarder ?",
                        "Sauvegarde", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result != DialogResult.OK)
                    e.Cancel = true;
            }
        }
        #endregion

        #region Methodes

        /// <summary>
        /// Remplis la map par la texture selectionnée du tileset
        /// </summary>
        public void Fill()
        {
            if (this.texture != null)
            {
                this.undoRedoManager.Add(this.Map.CopyToMemoryStream());
                this.gameMap.Fill(this.selectedLayerIndex, this.texture);
                this.picMap.Refresh();
                this.UndoRedoUpdated?.Invoke(this, this.undoRedoManager);
            }
        }

        /// <summary>
        /// Sauvegarde la carte
        /// </summary>
        public void Save()
        {
            this.Map.Save();
            this.IsSaved = true;
        }

        /// <summary>
        /// Ouvre la carte
        /// </summary>
        /// <param name="map"></param>
        public void Open(GameMap map)
        {
            this.Map = map;
            this.Map.MapChanged += GameMap_MapChanged;
            this.IsSaved = true;
        }

        /// <summary>
        /// Créer la nouvelle carte
        /// </summary>
        /// <param name="mapName"></param>
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
            if (state)
            {
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
        }

        /// <summary>
        /// Dessine la selection du tileset si l'état du document est par défaut, sinon dessine l'effet de l'état courant du document
        /// </summary>
        /// <param name="state">L'état de dessin</param>
        /// <param name="e">Les données de dessin</param>
        private void DrawSelection(bool state, PaintEventArgs e)
        {
            if (state)
            {
                GameVector2 location = this.GetTilePosition(this.mouseLocation.X + this.mapOrigin.X, this.mouseLocation.Y + this.mapOrigin.Y);

                if (this.State == GameEditorState.Default)
                {
                    if (this.texture?.BitmapSelection != null)
                    {
                        ImageAttributes attributes = new ImageAttributes();
                        attributes.SetOpacity(0.5f);

                        /*************** Affichage uniquement à l'interieur de la carte *****************/
                        /*int limitX = this.texture.BitmapSelection.Width / GlobalData.TileSize.Width;
                        int limitY = this.texture.BitmapSelection.Height / GlobalData.TileSize.Height;

                        for (int x = 0; x < limitX; x++)
                        {
                            for (int y = 0; y < limitY; y++)
                            {
                                if (location.X + x >= 0 && location.X + x < GlobalData.MapSize.Width &&
                                    location.Y + y >= 0 && location.Y + y < GlobalData.MapSize.Height)
                                {
                                    e.Graphics.DrawImage(this.texture.BitmapSelection,
                                        new Rectangle(
                                            (location.X + x) * GlobalData.TileSize.Width - this.mapOrigin.X,
                                            (location.Y + y) * GlobalData.TileSize.Height - this.mapOrigin.Y,
                                            GlobalData.TileSize.Width,
                                            GlobalData.TileSize.Height),
                                                x * GlobalData.TileSize.Width,
                                                y * GlobalData.TileSize.Height,
                                                GlobalData.TileSize.Width,
                                                GlobalData.TileSize.Height,
                                                GraphicsUnit.Pixel,
                                                attributes);
                                }
                            }
                        }*/

                        /*************** Affichage possible à l'exterieur de la carte *****************/
                        e.Graphics.DrawImage(this.texture.BitmapSelection,
                            new Rectangle(
                                location.X * GlobalData.TileSize.Width - this.mapOrigin.X,
                                location.Y * GlobalData.TileSize.Height - this.mapOrigin.Y,
                                this.texture.BitmapSelection.Width,
                                this.texture.BitmapSelection.Height),
                                    0, 0,
                                    this.texture.BitmapSelection.Width,
                                    this.texture.BitmapSelection.Height,
                                    GraphicsUnit.Pixel,
                                    attributes);
                    }
                }
                else if (this.State == GameEditorState.Erase && GameMap.InBounds(location))
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 255, 45, 45)),
                        location.X * GlobalData.TileSize.Width - this.mapOrigin.X,
                        location.Y * GlobalData.TileSize.Height - this.mapOrigin.Y,
                        GlobalData.TileSize.Width,
                        GlobalData.TileSize.Height);
                }
            }
        }

        /// <summary>
        /// Obtient la position relative en nombre de tiles depuis la position absolue en pixel
        /// </summary>
        /// <param name="absoluteX">La position absolue verticale</param>
        /// <param name="absoluteY">La position absolue horizontale</param>
        /// <returns>La position relative calculée</returns>
        private GameVector2 GetTilePosition(int absoluteX, int absoluteY)
        {
            GameVector2 vector = new GameVector2();

            if (absoluteX < 0)
                vector.X = (int)Math.Floor(absoluteX / (GlobalData.TileSize.Width * 1f));
            else
                vector.X = absoluteX / GlobalData.TileSize.Width;

            if (absoluteY < 0)
                vector.Y = (int)Math.Floor(absoluteY / (GlobalData.TileSize.Height * 1f));
            else
                vector.Y = absoluteY / GlobalData.TileSize.Height;

            return vector;
        }

        /// <summary>
        /// Met à jour les données des scrollbars du document
        /// </summary>
        private void RefreshScrollComponents()
        {
            this.hScrollBarPicMap.Enabled = this.picMap.Size.Width < GlobalData.TileSize.Width * GlobalData.MapSize.Width;
            this.vScrollBarPicMap.Enabled = this.picMap.Size.Height < GlobalData.TileSize.Height * GlobalData.MapSize.Height;

            if (this.vScrollBarPicMap.Enabled)
            {
                int scrollValue = this.mapOrigin.Y + Margin.Y;

                this.vScrollBarPicMap.Minimum = 0;
                this.vScrollBarPicMap.Maximum = (GlobalData.MapSize.Height * GlobalData.TileSize.Height + Margin.Y + 200) - this.picMap.Size.Height;

                this.vScrollBarPicMap.SmallChange = (int)(this.vScrollBarPicMap.Maximum * 0.1);
                this.vScrollBarPicMap.LargeChange = (int)(this.vScrollBarPicMap.Maximum * 0.3);

                this.vScrollBarPicMap.Value = scrollValue < this.vScrollBarPicMap.Maximum ? 
                    (scrollValue > 0 ? scrollValue : 0) : this.vScrollBarPicMap.Maximum;
            }

            if (this.hScrollBarPicMap.Enabled)
            {
                int scrollValue = this.mapOrigin.X + Margin.X;

                this.hScrollBarPicMap.Minimum = 0;
                this.hScrollBarPicMap.Maximum = (GlobalData.MapSize.Width * GlobalData.TileSize.Width + Margin.X + 200) - this.picMap.Size.Width;

                this.hScrollBarPicMap.SmallChange = (int)(this.hScrollBarPicMap.Maximum * 0.1);
                this.hScrollBarPicMap.LargeChange = (int)(this.hScrollBarPicMap.Maximum * 0.3);

                this.hScrollBarPicMap.Value = scrollValue < this.hScrollBarPicMap.Maximum ? 
                    (scrollValue > 0 ? scrollValue : 0) : this.hScrollBarPicMap.Maximum;
            }

            this.picMap.Refresh();
        }

        /// <summary>
        /// Libère les ressources et liens employés par le document
        /// </summary>
        public new void Dispose()
        {
            this.Map.MapChanged -= GameMap_MapChanged;
            this.undoRedoManager.UndoHappened -= UndoRedoSystem_UndoHappened;
            this.undoRedoManager.RedoHappened -= UndoRedoSystem_RedoHappened;
            base.Dispose(true);
            //GC.SuppressFinalize(this);
        }

        private void MapPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Obtient ou définit l'état d'affichage de la grille
        /// </summary>
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

        /// <summary>
        /// Obtient ou définit la couleur de la grille
        /// </summary>
        public Pen GridColor
        {
            get { return this.gridColor ?? GridPenColor; }
            set { this.gridColor = value; this.picMap.Refresh(); }
        }

        /// <summary>
        /// Obtient ou définit l'état d'affichage de la texture selectionnée
        /// </summary>
        public bool IsTilesetSelectionShown
        {
            get { return this.isTilesetSelectionShowProcessActived; }
            set
            {
                this.isTilesetSelectionShowProcessActived = value;
                this.toolStripBtnTilesetSelection.Checked = this.IsTilesetSelectionShown;
                this.picMap.Refresh();
            }
        }

        /// <summary>
        /// Obtient ou définit le bundle de données de texture
        /// </summary>
        public TextureInfo TextureInfo
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        /// <summary>
        /// Obtient ou définit l'index du layer selectionné associé à l'objet
        /// </summary>
        public int SelectedLayerIndex
        {
            get { return this.selectedLayerIndex; }
            set { this.selectedLayerIndex = value; }
        }

        /// <summary>
        /// Obtient ou définit l'état actuel du document
        /// </summary>
        public GameEditorState State
        {
            get { return this.state; }
            set
            {
                this.state = value;
                this.Cursor = value == GameEditorState.Erase ? EraseCursor : Cursors.Default;

            }
        }

        /// <summary>
        /// Obtient ou définit la carte associée à l'objet
        /// </summary>
        public GameMap Map
        {
            get { return this.gameMap; }
            private set { this.gameMap = value; }
        }

        /// <summary>
        /// Obtient ou définit l'état de sauvegarde courant du document
        /// </summary>
        public bool IsSaved
        {
            get { return this.isSaved; }
            private set
            {
                this.isSaved = value;
                this.Text = (this.isSaved ? string.Empty : UNSAVED_DOCUMENT_MARK) + this.gameMap?.Name;
            }
        }

        /// <summary>
        /// Obtient le nom de la carte associée à l'objet
        /// </summary>
        public new string Name
        {
            get { return this.Map.Name; }
            private set {  }
        }

        public UndoRedoManager<MemoryStream> Manager
        {
            get { return this.undoRedoManager; }
        }
        #endregion
    }
}
