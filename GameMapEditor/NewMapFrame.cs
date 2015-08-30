using System;
using System.Windows.Forms;

namespace GameMapEditor
{
    public partial class NewMapFrame : Form
    {
        private const string DEFAULT_MAP_NAME = "Map";
        private static int IndexUnique = 0;

        public delegate void NewMapEventArgs(string mapName);
#pragma warning disable CS0108
        public event NewMapEventArgs Validated;
#pragma warning restore CS0108

        public NewMapFrame()
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
            if(this.Validated != null)
            {
                this.Validated(this.txtMapName.Text);
            }
        }
    }
}
