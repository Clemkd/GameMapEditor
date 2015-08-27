using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public partial class MainFrame : Form
    {
        private TilesetFrame tilesetFrame;
        private MapBrowserFrame mapBrowserFrame;
        private List<MapFrame> mapFrames;
        private ConsoleFrame consoleFrame;
        private HistoryFrame historyFrame;

        public MainFrame()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Chargement des frames
            this.tilesetFrame = new TilesetFrame();
            this.tilesetFrame.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
            this.tilesetFrame.Show(DockPanel);
            this.tilesetFrame.DockState = DockState.DockLeft;
            this.tilesetFrame.TilesetSelectionChanged += TilesetFrame_TilesetSelectionChanged;
            this.tilesetFrame.TilesetChanged += TilesetFrame_TilesetChanged;

            this.mapFrames = new List<MapFrame>();

            this.mapBrowserFrame = new MapBrowserFrame();
            this.mapBrowserFrame.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
            this.mapBrowserFrame.Show(DockPanel);
            this.mapBrowserFrame.DockState = DockState.DockRight;

            this.consoleFrame = new ConsoleFrame();
            this.consoleFrame.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
            this.consoleFrame.Show(DockPanel);
            this.consoleFrame.DockState = DockState.DockBottom;

            this.historyFrame = new HistoryFrame();
            this.historyFrame.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom;
            this.historyFrame.Show(DockPanel);
            this.historyFrame.DockState = DockState.DockBottomAutoHide;
        }

        private void TilesetFrame_TilesetChanged(object sender, Bitmap tileset)
        {
            this.mapFrames.ForEach(x => x.TilesetImage = tileset);
        }

        private void TilesetFrame_TilesetSelectionChanged(object sender, Rectangle selection)
        {
            this.mapFrames.ForEach(x => x.TilesetSelection = selection);
        }

        private void toolStripBtnFill_Click(object sender, EventArgs e)
        {

        }

        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockPanel.SuspendLayout(true);
            MapFrame mapFrame = new MapFrame();

            mapFrame.TilesetImage = this.tilesetFrame.TilesetImage;
            mapFrame.TilesetSelection = this.tilesetFrame.TilesetSelection;

            mapFrame.Show(DockPanel);
            mapFrame.DockState = DockState.Document;
            mapFrame.Dock = DockStyle.Fill;
            
            this.mapFrames.Add(mapFrame);
            
            DockPanel.ResumeLayout(true, true);
        }
    }
}
