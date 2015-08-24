using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public enum HistoryType
    {
        Map,
        Fill,
        Pencil
    }

    public class HistoryFrame : DockContent
    {
        private ListView ListViewHistory;
        private ColumnHeader IconColumn;
        private ColumnHeader ActionColumn;
        private ImageList IconList;
        private List<Color> ColorList;
        private IContainer components;
        

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryFrame));
            this.ListViewHistory = new System.Windows.Forms.ListView();
            this.IconColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ActionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IconList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ListViewHistory
            // 
            this.ListViewHistory.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.ListViewHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListViewHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IconColumn,
            this.ActionColumn});
            this.ListViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewHistory.FullRowSelect = true;
            this.ListViewHistory.GridLines = true;
            this.ListViewHistory.Location = new System.Drawing.Point(0, 0);
            this.ListViewHistory.MultiSelect = false;
            this.ListViewHistory.Name = "ListViewHistory";
            this.ListViewHistory.Size = new System.Drawing.Size(524, 300);
            this.ListViewHistory.SmallImageList = this.IconList;
            this.ListViewHistory.TabIndex = 0;
            this.ListViewHistory.TileSize = new System.Drawing.Size(32, 32);
            this.ListViewHistory.UseCompatibleStateImageBehavior = false;
            this.ListViewHistory.View = System.Windows.Forms.View.Details;
            // 
            // IconColumn
            // 
            this.IconColumn.Text = "";
            this.IconColumn.Width = 24;
            // 
            // ActionColumn
            // 
            this.ActionColumn.Text = "Action";
            this.ActionColumn.Width = 500;
            // 
            // IconList
            // 
            this.IconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconList.ImageStream")));
            this.IconList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconList.Images.SetKeyName(0, "map.png");
            this.IconList.Images.SetKeyName(1, "paintcan.png");
            this.IconList.Images.SetKeyName(2, "pencil.png");
            // 
            // HistoryFrame
            // 
            this.ClientSize = new System.Drawing.Size(524, 300);
            this.Controls.Add(this.ListViewHistory);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "HistoryFrame";
            this.Text = "Historique";
            this.ResumeLayout(false);

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.InitializeComponent();
            this.ColorList = new List<Color>() { SystemColors.WindowText, Color.FromArgb(255, 50, 50), Color.FromArgb(50, 50, 255) };
        }

        /// <summary>
        /// Ajoute une nouvelle ligne d'informations dans l'historique
        /// </summary>
        /// <param name="type">Le type de la ligne</param>
        /// <param name="action">L'action</param>
        public void WriteLine(HistoryType type, string action) // TODO : Changer par un ActionType
        {
            ListViewItem item = new ListViewItem();
            item.ImageIndex = (int)type;
            item.SubItems.Add(action);
            this.ListViewHistory.Items.Add(item);
        }

        /// <summary>
        /// Supprime toutes les lignes de l'historique
        /// </summary>
        public void Clear()
        {
            ListViewHistory.Clear();
        }
    }
}
