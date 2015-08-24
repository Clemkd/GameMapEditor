using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public partial class MainFrame : Form
    {
        private TilesetFrame tilesetFrame;
        private MapBrowserFrame mapBrowserFrame;
        private MapFrame mapFrame;
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

            this.mapFrame = new MapFrame();
            this.mapFrame.DockAreas = DockAreas.Document;
            this.mapFrame.Show(DockPanel);
            this.mapFrame.DockState = DockState.Document;

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

        private void TilesetFrame_TilesetChanged(object sender, Image tileset)
        {
            this.mapFrame.TilesetImage = tileset;
        }

        private void TilesetFrame_TilesetSelectionChanged(object sender, Rectangle selection)
        {
            this.mapFrame.TilesetSelection = selection;
        }
    }
}
