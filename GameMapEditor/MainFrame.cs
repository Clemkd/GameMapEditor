using GameMapEditor.Frames;
using GameMapEditor.Objects;
using GameMapEditor.Objects.Controls;
using GameMapEditor.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public partial class MainFrame : Form, IDisposable
    {
        private static List<MapPanel> MapPanels = new List<MapPanel>();

        private ConsoleStreamWriter consoleSW;

        public MainFrame()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Redirection du flux Console vers ConsolePanel
            this.consoleSW = new ConsoleStreamWriter(ConsolePanel.Instance);
            Console.SetOut(this.consoleSW);

            // Chargement des panels
            this.LoadTilesetPanel();
            this.LoadConsolePanel();
            this.LoadBrowserPanel();
            this.LoadLayerPanel();

            // Création des liens
            MapPanelFrame.Instance.MapValidated += NewMapFrame_Validated;
            TilesetPanel.Instance.TilesetSelectionChanged += TilesetPanel_TilesetSelectionChanged;
            TilesetPanel.Instance.TilesetChanged += TilesetPanel_TilesetChanged;
            LayerPanel.Instance.MapLayerAdded += LayerPanel_MapLayerAdded;
            LayerPanel.Instance.MapLayerSelectionChanged += LayerPanel_MapLayerSelectionChanged;
            LayerPanel.Instance.Refresh();
        }

        private void TilesetPanel_TilesetChanged(object sender, TextureInfo texture)
        {
            MapPanels.ForEach(x => x.TextureInfo = texture);
        }

        private void TilesetPanel_TilesetSelectionChanged(object sender, Rectangle selection)
        {
            MapPanels.ForEach(x => x.TilesetSelection = selection);
        }

        private void toolStripBtnFill_Click(object sender, EventArgs e)
        {
            MapPanel mapPanel = this.DockPanel.ActiveDocument as MapPanel;
            if (mapPanel != null) mapPanel.Fill();
        }

        private void NewMapFrame_Validated(string mapName)
        {
            MapPanel mapPanel = new MapPanel(TilesetPanel.Instance.TilesetInfo, TilesetPanel.Instance.TilesetSelection, mapName);
            this.DockPanel.SuspendLayout();
            mapPanel.Show(this.DockPanel);
            mapPanel.DockState = DockState.Document;
            mapPanel.Dock = DockStyle.Fill;
            this.DockPanel.ResumeLayout(true, true);

            MapPanels.Add(mapPanel);
        }

        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapPanelFrame.Instance.ShowDialog();
        }

        private void tilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadTilesetPanel();
        }

        private void explorateurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadBrowserPanel();
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
                ConsolePanel.Instance.WriteLine("Carte de jeu enregistrée avec succés.", RowType.Information);
            }
        }

        private void PanelToolsSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DockPanel.DocumentsCount > 0)
                {
                    // Sauvegarder tout
                    foreach (MapPanel document in this.DockPanel.Documents)
                        document.Save();
                    ConsolePanel.Instance.WriteLine("Cartes de jeu enregistrées avec succés.", RowType.Information);
                }
            }
            catch (Exception ex)
            {
                ConsolePanel.Instance.WriteLine(ex.Message);
                
            }
        }

        private async void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "FRoG Creator map (*.frog)|*.frog";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                IndefinedWaitingFrame waintingFrame = new IndefinedWaitingFrame();
                try
                {
                    waintingFrame.Location = new Point(this.Location.X + (this.Width - waintingFrame.Width) / 2,
                        this.Location.Y + (this.Height - waintingFrame.Height) / 2);
                    waintingFrame.Show(this);

                    GameMap map = await GameMap.Load(openFileDialog.FileName);
                    
                    MapPanel mapPanel = new MapPanel(TilesetPanel.Instance.TilesetInfo, TilesetPanel.Instance.TilesetSelection, map);
                    this.DockPanel.SuspendLayout();
                    mapPanel.Show(this.DockPanel);
                    mapPanel.DockState = DockState.Document;
                    mapPanel.Dock = DockStyle.Fill;
                    this.DockPanel.ResumeLayout(true, true);

                    MapPanels.Add(mapPanel);

                    LayerPanel.Instance.LoadLayers(map);
                }
                catch (Exception ex)
                {
                    ConsolePanel.Instance.WriteLine(ex);
                }
                finally
                {
                    waintingFrame.Close();
                }
            }
        }

        private void LayerPanel_MapLayerAdded(GameMapLayer layer)
        {
            MapPanel mapPanel = this.DockPanel.ActiveDocument as MapPanel;
            if (mapPanel != null)
            {
                mapPanel.Map.AddLayer(layer);
                ConsolePanel.Instance.WriteLine("La couche a été ajouté avec succés à la carte en cours", RowType.Information);
            }
        }

        private void LayerPanel_MapLayerSelectionChanged(int index)
        {
            MapPanel mapPanel = this.DockPanel.ActiveDocument as MapPanel;
            if (mapPanel != null)
            {
                mapPanel.SelectedLayerIndex = index;
            }
        }

        private void toolStripButtonDestinationFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Path.GetFullPath("."));
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadTilesetPanel()
        {
            TilesetPanel.Instance.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
            TilesetPanel.Instance.Show(this.DockPanel);
            TilesetPanel.Instance.DockTo(this.DockPanel, DockStyle.Left);
        }

        private void LoadConsolePanel()
        {
            ConsolePanel.Instance.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
            ConsolePanel.Instance.Show(this.DockPanel);
            ConsolePanel.Instance.DockTo(this.DockPanel, DockStyle.Bottom);
        }

        private void LoadBrowserPanel()
        {
            BrowserPanel.Instance.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
            BrowserPanel.Instance.Show(DockPanel);
            BrowserPanel.Instance.DockTo(this.DockPanel, DockStyle.Right);
        }

        private void LoadLayerPanel()
        {
            LayerPanel.Instance.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
            LayerPanel.Instance.Show(this.DockPanel);
            LayerPanel.Instance.DockTo(this.DockPanel, DockStyle.Right);
        }

        private void DockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            LayerPanel.Instance.Refresh();
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(ExistsUnsavedDocuments)
            {
                DialogResult result = MessageBox.Show(this, "Tous les documents n'ont pas été sauvegardé !\nQuitter sans sauvegarder ?",
                    "Sauvegarde", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
                else e.Cancel = true;
            }
        }

        private bool ExistsUnsavedDocuments
        {
            get
            {
                foreach (MapPanel mapPanel in DockPanel.Documents)
                {
                    if (!mapPanel.IsSaved)
                        return true;
                }
                return false;
            }
        }

        private void toolStripButtonErase_Click(object sender, EventArgs e)
        {
            MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
            if(mapPanel != null)
                mapPanel.Cursor = new Cursor(Resources.eraser.GetHicon());
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
            if (mapPanel != null && mapPanel.CanUndo)
            {
                mapPanel.Undo();
                this.toolStripButtonRedo.Enabled = mapPanel.CanRedo;
                this.toolStripButtonUndo.Enabled = mapPanel.CanUndo;
            }
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
            if (mapPanel != null && mapPanel.CanRedo)
            {
                mapPanel.Redo();
                this.toolStripButtonRedo.Enabled = mapPanel.CanRedo;
                this.toolStripButtonUndo.Enabled = mapPanel.CanUndo;
            }
        }
    }
}
