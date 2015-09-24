using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GameMapEditor.Frames
{
    public partial class MapPanelFrame : Form
    {
        public delegate void NewMapEventArgs(object sender, string mapName);
        public event NewMapEventArgs MapValidated;

        public static MapPanelFrame Instance = new MapPanelFrame();

        private MapPanelFrame()
        {
            InitializeComponent();
        }

        private string SafeFilename(string filename)
        {
            return string.Join("", filename.Split(Path.GetInvalidFileNameChars()));
        }

        private void btnValidNewMap_Click(object sender, EventArgs e)
        {
            string filename = this.txtMapName.Text;

            if(string.IsNullOrWhiteSpace(filename))
            {
                ConsolePanel.Instance.WriteLine("Le nom de la carte est incorrect", RowType.Error);
                return;
            }

            if (File.Exists(string.Format("{0}\\{1}", GlobalData.MAPS_DIRECTORY_PATH, string.Format("{0}.frog", filename))))
            {
                DialogResult result = MessageBox.Show(this, "Une carte de jeu avec le même nom existe déjà.\nSouhaitez-vous la remplacer ?",
                    "Fichier existant", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                    return;
            }

            this.MapValidated?.Invoke(this, filename);
            this.Close();
                
        }

        private void txtMapName_Leave(object sender, EventArgs e)
        {
            this.txtMapName.Text = SafeFilename(this.txtMapName.Text);
        }
    }
}
