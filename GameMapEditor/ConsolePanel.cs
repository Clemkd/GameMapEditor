using GameMapEditor.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public enum RowType
    {
        Error,
        Information,
        Normal
    }

    public partial class ConsolePanel : DockContent
    {
        private static Color NormalColor = SystemColors.WindowText;
        private static Color ErrorColor = Color.FromArgb(255, 50, 50);
        private static Color InformationColor = Color.FromArgb(50, 50, 255);
        private List<Color> ColorList;

        public static ConsolePanel Instance = new ConsolePanel();

        private ConsolePanel() : base()
        {
            this.HideOnClose = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.InitializeComponent();
            this.ColorList = new List<Color>() { NormalColor, ErrorColor, InformationColor };     
        }

        /// <summary>
        /// Ajoute une nouvelle ligne d'informations dans la console
        /// </summary>
        /// <param name="message">Le message des données</param>
        /// /// <param name="type">Le type de la ligne</param>
        public void WriteLine(string message, RowType type = RowType.Normal)
        {
            ListViewItem item = new ListViewItem();
            item.ImageIndex = (int)type;
            item.SubItems.Add(DateTime.Now.ToString());
            item.SubItems.Add(message);
            item.ForeColor = ColorList.ElementAt(((int)type < this.ColorList.Count - 1) ? (int)type + 1 : 0);
            this.ListViewConsole.Items.Add(item).EnsureVisible();
        }

        public void WriteLine(Exception ex)
        {
            ListViewItem item = new ListViewItem();
            item.ImageIndex = 0;
            item.SubItems.Add(string.Format("[{0}] {1}", ex.Source, DateTime.Now.ToString()));
            item.SubItems.Add(ex.Message);
            item.ForeColor = ErrorColor;
            this.ListViewConsole.Items.Add(item).EnsureVisible();

            ErrorLog.Write(ex);
        }

        /// <summary>
        /// Supprime toutes les lignes de la console
        /// </summary>
        public void Clear()
        {
            ListViewConsole.Clear();
        }
    }
}
