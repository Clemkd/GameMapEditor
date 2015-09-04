using GameMapEditor.Frames;
using GameMapEditor.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public partial class MainFrame : Form
    {
        private static MapPanelFormular NewMapFrame;

        private TilesetPanel tilesetPanel;
        private MapBrowserPanel mapBrowserPanel;
        private List<MapPanel> mapPanels;
        private ConsolePanel consolePanel;
        private LayerPanel layerPanel;

        public MainFrame()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            NewMapFrame = new MapPanelFormular();
            NewMapFrame.MapValidated += NewMapFrame_Validated;

            // Chargement des panels
            this.tilesetPanel = new TilesetPanel();
            this.tilesetPanel.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
            this.tilesetPanel.CloseButtonVisible = false;
            this.tilesetPanel.TilesetSelectionChanged += TilesetPanel_TilesetSelectionChanged;
            this.tilesetPanel.TilesetChanged += TilesetPanel_TilesetChanged;
            this.tilesetPanel.Show(this.DockPanel);
            this.tilesetPanel.DockTo(this.DockPanel, DockStyle.Left);

            this.mapPanels = new List<MapPanel>();

            this.LoadConsolePanel();
            this.LoadMapBrowserPanel();
            this.LoadLayerPanel();
        }

        private void TilesetPanel_TilesetChanged(object sender, BitmapImage texture)
        {
            this.mapPanels.ForEach(x => x.Texture = texture);
        }

        private void TilesetPanel_TilesetSelectionChanged(object sender, Rectangle selection)
        {
            this.mapPanels.ForEach(x => x.TilesetSelection = selection);
        }

        private void toolStripBtnFill_Click(object sender, EventArgs e)
        {
            MapPanel mapPanel = this.DockPanel.ActiveDocument as MapPanel;
            if (mapPanel != null) mapPanel.Fill();
        }

        private void NewMapFrame_Validated(string mapName)
        {
            MapPanel.OpenNewDocument(
                this.DockPanel, 
                this.mapPanels, 
                this.tilesetPanel.TilesetImage, 
                this.tilesetPanel.TilesetSelection, 
                new GameMap(mapName));
        }

        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMapFrame.ShowDialog();
        }

        private void explorateurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadMapBrowserPanel();
        }

        private void couchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadLayerPanel();
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadConsolePanel();
        }

        private void PanelToolsSaveCurrent_Click(object sender, EventArgs e)
        {
            // Sauvegarder map courante
            MapPanel mapPanel = this.DockPanel.ActiveDocument as MapPanel;
            if (mapPanel != null)
            {
                mapPanel.Save();
                consolePanel.WriteLine(DateTime.Now.ToString(), "Carte de jeu enregistrée avec succés.", RowType.Information);
            }
        }

        private void PanelToolsSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                // Sauvegarder tout
                foreach (MapPanel document in this.DockPanel.Documents)
                    document.Save();
                consolePanel.WriteLine(DateTime.Now.ToString(), "Cartes de jeu enregistrées avec succés.", RowType.Information);
            }
            catch (Exception ex)
            {
                consolePanel.WriteLine(DateTime.Now.ToString(),
                    "Une erreur est survenue lors de la sauvegarde de la carte : " + ex.Message,
                    RowType.Error);
                ErrorLog.Write(ex);
            }
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "FRoG Creator map (*.frog)|*.frog";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    GameMap map = GameMap.Load(openFileDialog.FileName);
                    MapPanel mapPanel = MapPanel.OpenNewDocument(
                        this.DockPanel,
                        this.mapPanels,
                        this.tilesetPanel.TilesetImage,
                        this.tilesetPanel.TilesetSelection,
                        map);
                    mapPanel.Map.FilesDependences.ForEach(x => consolePanel.WriteLine(mapPanel.Map.Name, x));
                    layerPanel.Clear();
                    mapPanel.Map.Layers.ForEach(layer => layerPanel.AddLayer(layer));
                }
                catch (Exception ex)
                {
                    consolePanel.WriteLine("Loader",
                        "Une erreur est survenue lors du chargement de la carte : " + ex.Message,
                        RowType.Error);
                    ErrorLog.Write(ex);
                }
            }
        }

        private void LayerPanel_MapLayerAdded(GameMapLayer layer)
        {
            MapPanel mapPanel = this.DockPanel.ActiveDocument as MapPanel;
            if (mapPanel != null)
            {
                mapPanel.Map.AddLayer(layer);
                layerPanel.AddLayer(layer);
                consolePanel.WriteLine(DateTime.Now.ToString(), "La couche a été ajouté avec succés à la carte en cours", RowType.Information);
            }
        }

        private void toolStripButtonDestinationFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Path.GetFullPath("."));
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void LoadMapBrowserPanel()
        {
            if (this.mapBrowserPanel == null || this.mapBrowserPanel.IsDisposed)
            {
                this.mapBrowserPanel = new MapBrowserPanel();
                this.mapBrowserPanel.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
                this.mapBrowserPanel.Show(DockPanel);
                this.mapBrowserPanel.DockTo(this.DockPanel, DockStyle.Right);
            }
        }

        private void LoadConsolePanel()
        {
            if (this.consolePanel == null || this.consolePanel.IsDisposed)
            {
                this.consolePanel = new ConsolePanel();
                this.consolePanel.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
                this.consolePanel.Show(this.DockPanel);
                this.consolePanel.DockTo(this.DockPanel, DockStyle.Bottom);
            }
        }

        private void LoadLayerPanel()
        {
            if (this.layerPanel == null || this.layerPanel.IsDisposed)
            {
                this.layerPanel = new LayerPanel();
                this.layerPanel.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
                this.layerPanel.Show(this.DockPanel);
                this.layerPanel.DockTo(this.DockPanel, DockStyle.Right);
                this.layerPanel.MapLayerAdded += LayerPanel_MapLayerAdded;
            }
        }

        private void DockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            MapPanel mapPanel = this.DockPanel.ActiveDocument as MapPanel;
            if (mapPanel != null)
            {
                layerPanel.Clear();
                mapPanel.Map.Layers.ForEach(layer => layerPanel.AddLayer(layer));
            }
        }
    }
}
