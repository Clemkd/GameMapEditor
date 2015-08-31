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
        private static NewMapFrame NewMapFrame;

        private TilesetPanel tilesetPanel;
        private MapBrowserPanel mapBrowserPanel;
        private List<MapPanel> mapPanels;
        private ConsolePanel consolePanel;
        private HistoryPanel historyPanel;

        public MainFrame()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            NewMapFrame = new NewMapFrame();
            NewMapFrame.Validated += NewMapFrame_Validated;

            // Chargement des panels
            this.tilesetPanel = new TilesetPanel();
            this.tilesetPanel.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
            this.tilesetPanel.CloseButtonVisible = false;
            this.tilesetPanel.TilesetSelectionChanged += TilesetPanel_TilesetSelectionChanged;
            this.tilesetPanel.TilesetChanged += TilesetPanel_TilesetChanged;
            this.tilesetPanel.Show(DockPanel);
            this.tilesetPanel.DockState = DockState.DockLeft;

            this.mapPanels = new List<MapPanel>();

            this.LoadMapBrowserPanel();
            this.LoadConsolePanel();
            this.LoadHistoryPanel();
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
            MapPanel.OpenNewDocument(this.DockPanel, this.mapPanels, this.tilesetPanel.TilesetImage, this.tilesetPanel.TilesetSelection, new GameMap(mapName));
        }

        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMapFrame.ShowDialog();
        }

        private void explorateurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadMapBrowserPanel();
        }

        private void historiqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadHistoryPanel();
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

        private void LoadMapBrowserPanel()
        {
            if (this.mapBrowserPanel == null || this.mapBrowserPanel.IsDisposed)
            {
                this.mapBrowserPanel = new MapBrowserPanel();
                this.mapBrowserPanel.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
                this.mapBrowserPanel.Show(DockPanel);
                this.mapBrowserPanel.DockState = DockState.DockRight;
            }
        }

        private void LoadConsolePanel()
        {
            if (this.consolePanel == null || this.consolePanel.IsDisposed)
            {
                this.consolePanel = new ConsolePanel();
                this.consolePanel.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
                this.consolePanel.Show(DockPanel);
                this.consolePanel.DockState = DockState.DockBottom;
            }
        }

        private void LoadHistoryPanel()
        {
            if (this.historyPanel == null || this.historyPanel.IsDisposed)
            {
                this.historyPanel = new HistoryPanel();
                this.historyPanel.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
                this.historyPanel.Show(DockPanel);
                this.historyPanel.DockState = DockState.DockBottomAutoHide;
            }
        }
    }
}
