using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMapEditor
{
    partial class ConsolePanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsolePanel));
            this.ListViewConsole = new System.Windows.Forms.ListView();
            this.IconColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SourceColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MessageColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IconList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ListViewConsole
            // 
            this.ListViewConsole.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.ListViewConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListViewConsole.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IconColumn,
            this.SourceColumn,
            this.MessageColumn});
            this.ListViewConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewConsole.FullRowSelect = true;
            this.ListViewConsole.GridLines = true;
            this.ListViewConsole.Location = new System.Drawing.Point(0, 0);
            this.ListViewConsole.MultiSelect = false;
            this.ListViewConsole.Name = "ListViewConsole";
            this.ListViewConsole.Size = new System.Drawing.Size(630, 300);
            this.ListViewConsole.SmallImageList = this.IconList;
            this.ListViewConsole.TabIndex = 0;
            this.ListViewConsole.TileSize = new System.Drawing.Size(32, 32);
            this.ListViewConsole.UseCompatibleStateImageBehavior = false;
            this.ListViewConsole.View = System.Windows.Forms.View.Details;
            // 
            // IconColumn
            // 
            this.IconColumn.Text = "";
            this.IconColumn.Width = 24;
            // 
            // SourceColumn
            // 
            this.SourceColumn.Text = "Source";
            this.SourceColumn.Width = 103;
            // 
            // MessageColumn
            // 
            this.MessageColumn.Text = "Message";
            this.MessageColumn.Width = 500;
            // 
            // IconList
            // 
            this.IconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconList.ImageStream")));
            this.IconList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconList.Images.SetKeyName(0, "error.png");
            this.IconList.Images.SetKeyName(1, "exclamation.png");
            // 
            // ConsolePanel
            // 
            this.ClientSize = new System.Drawing.Size(630, 300);
            this.Controls.Add(this.ListViewConsole);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "ConsolePanel";
            this.Text = "Console";
            this.ResumeLayout(false);
        }

        private ListView ListViewConsole;
        private ColumnHeader IconColumn;
        private ColumnHeader SourceColumn;
        private ColumnHeader MessageColumn;
        private ImageList IconList;
    }
}
