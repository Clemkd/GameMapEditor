using System;
using System.Windows.Forms;

namespace GameMapEditor.Frames
{
    public partial class MapPanelFrame : Form
    {
        private const string DEFAULT_MAP_NAME = "Carte";
        private static int IndexUnique = 0;

        public delegate void NewMapEventArgs(string mapName);
        public event NewMapEventArgs MapValidated;


        public MapPanelFrame()
        {
            InitializeComponent();
        }

        private void NewMapFrame_Load(object sender, EventArgs e)
        {
            this.txtMapName.Text = DEFAULT_MAP_NAME + IndexUnique.ToString();
        }

        private void btnValidNewMap_Click(object sender, EventArgs e)
        {
            this.RaiseNewMapFrameValidatedEvent();
            IndexUnique++;
            this.Close();
        }

        private void RaiseNewMapFrameValidatedEvent()
        {
            if(this.MapValidated != null)
            {
                this.MapValidated(this.txtMapName.Text);
            }
        }
    }
}
