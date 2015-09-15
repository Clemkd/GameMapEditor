using System;
using System.Windows.Forms;

namespace GameMapEditor.Frames
{
    public partial class MapPanelFrame : Form
    {
        public delegate void NewMapEventArgs(string mapName);
        public event NewMapEventArgs MapValidated;

        public static MapPanelFrame Instance = new MapPanelFrame();

        private MapPanelFrame()
        {
            InitializeComponent();
        }

        private void NewMapFrame_Load(object sender, EventArgs e)
        {
            // TODO : Utiliser des Regex + lecture de fichiers pour donner de l'intelligence à la génération de nom de nouvelle carte
            this.txtMapName.Text = "Carte";
        }

        private void btnValidNewMap_Click(object sender, EventArgs e)
        {
            this.MapValidated?.Invoke(this.txtMapName.Text);
            this.Close();
        }
    }
}
