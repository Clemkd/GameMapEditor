using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMapEditor
{
    partial class TilesetPanel
    {
        private PictureBox picTileset;
        private VScrollBar vPicTilesetScrollBar;
        private HScrollBar hPicTilesetScrollBar;
        private ComboBox comboTilesetFileSelecter;
        private ToolStrip toolStrip;
        private ToolStripButton toolStripButtonGrid;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TilesetPanel));
            this.picTileset = new System.Windows.Forms.PictureBox();
            this.vPicTilesetScrollBar = new System.Windows.Forms.VScrollBar();
            this.hPicTilesetScrollBar = new System.Windows.Forms.HScrollBar();
            this.comboTilesetFileSelecter = new System.Windows.Forms.ComboBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonGrid = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.picTileset)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // picTileset
            // 
            this.picTileset.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picTileset.Location = new System.Drawing.Point(0, 27);
            this.picTileset.Name = "picTileset";
            this.picTileset.Size = new System.Drawing.Size(185, 83);
            this.picTileset.TabIndex = 0;
            this.picTileset.TabStop = false;
            this.picTileset.SizeChanged += new System.EventHandler(this.picTileset_SizeChanged);
            this.picTileset.Paint += new System.Windows.Forms.PaintEventHandler(this.picTileset_Paint);
            this.picTileset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picTileset_MouseDown);
            this.picTileset.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picTileset_MouseMove);
            this.picTileset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picTileset_MouseUp);
            // 
            // vPicTilesetScrollBar
            // 
            this.vPicTilesetScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vPicTilesetScrollBar.Location = new System.Drawing.Point(185, 27);
            this.vPicTilesetScrollBar.Name = "vPicTilesetScrollBar";
            this.vPicTilesetScrollBar.Size = new System.Drawing.Size(17, 83);
            this.vPicTilesetScrollBar.TabIndex = 1;
            this.vPicTilesetScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vPicTilesetScrollBar_Scroll);
            // 
            // hPicTilesetScrollBar
            // 
            this.hPicTilesetScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hPicTilesetScrollBar.Location = new System.Drawing.Point(0, 110);
            this.hPicTilesetScrollBar.Name = "hPicTilesetScrollBar";
            this.hPicTilesetScrollBar.Size = new System.Drawing.Size(185, 17);
            this.hPicTilesetScrollBar.TabIndex = 2;
            this.hPicTilesetScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hPicTilesetScrollBar_Scroll);
            // 
            // comboTilesetFileSelecter
            // 
            this.comboTilesetFileSelecter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboTilesetFileSelecter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTilesetFileSelecter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboTilesetFileSelecter.FormattingEnabled = true;
            this.comboTilesetFileSelecter.Location = new System.Drawing.Point(0, 3);
            this.comboTilesetFileSelecter.Name = "comboTilesetFileSelecter";
            this.comboTilesetFileSelecter.Size = new System.Drawing.Size(202, 21);
            this.comboTilesetFileSelecter.TabIndex = 3;
            this.comboTilesetFileSelecter.SelectedIndexChanged += new System.EventHandler(this.comboTilesetFileSelecter_SelectedIndexChanged);
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonGrid});
            this.toolStrip.Location = new System.Drawing.Point(0, 131);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(202, 25);
            this.toolStrip.TabIndex = 4;
            // 
            // toolStripButtonGrid
            // 
            this.toolStripButtonGrid.Checked = true;
            this.toolStripButtonGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGrid.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGrid.Image")));
            this.toolStripButtonGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGrid.Name = "toolStripButtonGrid";
            this.toolStripButtonGrid.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonGrid.Text = "Afficher / Cacher la grille";
            this.toolStripButtonGrid.Click += new System.EventHandler(this.toolStripButtonGrid_Click);
            // 
            // TilesetPanel
            // 
            this.ClientSize = new System.Drawing.Size(202, 156);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.comboTilesetFileSelecter);
            this.Controls.Add(this.hPicTilesetScrollBar);
            this.Controls.Add(this.vPicTilesetScrollBar);
            this.Controls.Add(this.picTileset);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(50, 50);
            this.Name = "TilesetPanel";
            this.Text = "Tileset";
            ((System.ComponentModel.ISupportInitialize)(this.picTileset)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
