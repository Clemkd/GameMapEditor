using GameMapEditor.Objects;
using System.Windows.Forms;

namespace GameMapEditor
{
    partial class LayerPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerPanel));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddLayer = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemoveLayer = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUpLayer = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDownLayer = new System.Windows.Forms.ToolStripButton();
            this.layerPanelCTM = new GameMapEditor.Objects.Controls.LayerPanelCTM();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddLayer,
            this.toolStripButtonRemoveLayer,
            this.toolStripButtonUpLayer,
            this.toolStripButtonDownLayer});
            this.toolStrip.Location = new System.Drawing.Point(0, 115);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(200, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonAddLayer
            // 
            this.toolStripButtonAddLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddLayer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddLayer.Image")));
            this.toolStripButtonAddLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddLayer.Name = "toolStripButtonAddLayer";
            this.toolStripButtonAddLayer.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddLayer.Text = "Ajouter une couche";
            this.toolStripButtonAddLayer.Click += new System.EventHandler(this.toolStripButtonAddLayer_Click);
            // 
            // toolStripButtonRemoveLayer
            // 
            this.toolStripButtonRemoveLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemoveLayer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRemoveLayer.Image")));
            this.toolStripButtonRemoveLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoveLayer.Name = "toolStripButtonRemoveLayer";
            this.toolStripButtonRemoveLayer.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRemoveLayer.Text = "Supprimer la couche";
            this.toolStripButtonRemoveLayer.Click += new System.EventHandler(this.toolStripButtonRemoveLayer_Click);
            // 
            // toolStripButtonUpLayer
            // 
            this.toolStripButtonUpLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUpLayer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUpLayer.Image")));
            this.toolStripButtonUpLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUpLayer.Name = "toolStripButtonUpLayer";
            this.toolStripButtonUpLayer.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUpLayer.Text = "Mettre au dessus";
            this.toolStripButtonUpLayer.Click += new System.EventHandler(this.toolStripButtonUpLayer_Click);
            // 
            // toolStripButtonDownLayer
            // 
            this.toolStripButtonDownLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDownLayer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDownLayer.Image")));
            this.toolStripButtonDownLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDownLayer.Name = "toolStripButtonDownLayer";
            this.toolStripButtonDownLayer.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDownLayer.Text = "Mettre en dessous";
            this.toolStripButtonDownLayer.Click += new System.EventHandler(this.toolStripButtonDownLayer_Click);
            // 
            // layerPanelCTM
            // 
            this.layerPanelCTM.AutoScroll = true;
            this.layerPanelCTM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerPanelCTM.Location = new System.Drawing.Point(0, 0);
            this.layerPanelCTM.Name = "layerPanelCTM";
            this.layerPanelCTM.SelectedIndex = 0;
            this.layerPanelCTM.Size = new System.Drawing.Size(200, 115);
            this.layerPanelCTM.TabIndex = 2;
            this.layerPanelCTM.ItemSelectionChanged += new GameMapEditor.Objects.Controls.PanelItemSelectionChangedEventArgs(this.layerPanelCTM_ItemSelectionChanged);
            this.layerPanelCTM.LayerVisibleStateChanged += new GameMapEditor.Objects.Controls.ItemChangedEventArgs(this.layerPanelCTM_LayerVisibleStateChanged);
            this.layerPanelCTM.LayerTypeChanged += new GameMapEditor.Objects.Controls.ItemChangedEventArgs(this.layerPanelCTM_LayerTypeChanged);
            // 
            // LayerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 140);
            this.Controls.Add(this.layerPanelCTM);
            this.Controls.Add(this.toolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(200, 140);
            this.Name = "LayerPanel";
            this.Text = "Couches";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddLayer;
        private System.Windows.Forms.ToolStripButton toolStripButtonUpLayer;
        private System.Windows.Forms.ToolStripButton toolStripButtonDownLayer;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveLayer;
        private Objects.Controls.LayerPanelCTM layerPanelCTM;
    }
}