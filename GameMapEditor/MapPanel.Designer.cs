using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMapEditor
{
    partial class MapPanel
    {
        private ToolStrip ToolStrip;
        private ToolStripButton toolStripBtnGrid;
        private PictureBox picMap;
        private VScrollBar vScrollBarPicMap;
        private HScrollBar hScrollBarPicMap;
        private ToolStripButton toolStripBtnTilesetSelection;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapPanel));
            this.picMap = new System.Windows.Forms.PictureBox();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnTilesetSelection = new System.Windows.Forms.ToolStripButton();
            this.vScrollBarPicMap = new System.Windows.Forms.VScrollBar();
            this.hScrollBarPicMap = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // picMap
            // 
            this.picMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picMap.Location = new System.Drawing.Point(2, 23);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(278, 232);
            this.picMap.TabIndex = 0;
            this.picMap.TabStop = false;
            this.picMap.Paint += new System.Windows.Forms.PaintEventHandler(this.picMap_Paint);
            this.picMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseDown);
            this.picMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseMove);
            this.picMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseUp);
            this.picMap.Resize += new System.EventHandler(this.picMap_Resize);
            // 
            // ToolStrip
            // 
            this.ToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnGrid,
            this.toolStripBtnTilesetSelection});
            this.ToolStrip.Location = new System.Drawing.Point(0, 275);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ToolStrip.Size = new System.Drawing.Size(300, 25);
            this.ToolStrip.TabIndex = 1;
            // 
            // toolStripBtnGrid
            // 
            this.toolStripBtnGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnGrid.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnGrid.Image")));
            this.toolStripBtnGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnGrid.Name = "toolStripBtnGrid";
            this.toolStripBtnGrid.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnGrid.Text = "Afficher / Cacher la grille";
            this.toolStripBtnGrid.Click += new System.EventHandler(this.toolStripBtnGrid_Click);
            // 
            // toolStripBtnTilesetSelection
            // 
            this.toolStripBtnTilesetSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnTilesetSelection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnTilesetSelection.Image")));
            this.toolStripBtnTilesetSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnTilesetSelection.Name = "toolStripBtnTilesetSelection";
            this.toolStripBtnTilesetSelection.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnTilesetSelection.Text = "Afficher / Cacher la selection du tileset";
            this.toolStripBtnTilesetSelection.Click += new System.EventHandler(this.toolStripBtnTilesetSelection_Click);
            // 
            // vScrollBarPicMap
            // 
            this.vScrollBarPicMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBarPicMap.Location = new System.Drawing.Point(283, 23);
            this.vScrollBarPicMap.Name = "vScrollBarPicMap";
            this.vScrollBarPicMap.Size = new System.Drawing.Size(17, 241);
            this.vScrollBarPicMap.TabIndex = 2;
            this.vScrollBarPicMap.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBarPicMap_Scroll);
            // 
            // hScrollBarPicMap
            // 
            this.hScrollBarPicMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBarPicMap.Location = new System.Drawing.Point(2, 258);
            this.hScrollBarPicMap.Name = "hScrollBarPicMap";
            this.hScrollBarPicMap.Size = new System.Drawing.Size(281, 17);
            this.hScrollBarPicMap.TabIndex = 3;
            this.hScrollBarPicMap.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarPicMap_Scroll);
            // 
            // MapPanel
            // 
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.picMap);
            this.Controls.Add(this.hScrollBarPicMap);
            this.Controls.Add(this.vScrollBarPicMap);
            this.Controls.Add(this.ToolStrip);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Text = "Map";
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
