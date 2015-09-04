﻿namespace GameMapEditor
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Couches supérieures", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Couches inférieures", System.Windows.Forms.HorizontalAlignment.Center);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerPanel));
            this.listViewLayers = new System.Windows.Forms.ListView();
            this.ColumnHeaderLayerState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeaderLayerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageListLayerType = new System.Windows.Forms.ImageList(this.components);
            this.ImageListVisibleState = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddLayer = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewOverlay
            // 
            this.listViewLayers.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewLayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewLayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeaderLayerState,
            this.ColumnHeaderLayerName});
            this.listViewLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLayers.FullRowSelect = true;
            this.listViewLayers.GridLines = true;
            listViewGroup1.Header = "Couches supérieures";
            listViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup1.Name = "ListViewGroupUpperLayers";
            listViewGroup2.Header = "Couches inférieures";
            listViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup2.Name = "ListViewGroupLowerLayers";
            this.listViewLayers.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.listViewLayers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewLayers.LabelWrap = false;
            this.listViewLayers.Location = new System.Drawing.Point(0, 0);
            this.listViewLayers.MultiSelect = false;
            this.listViewLayers.Name = "listViewOverlay";
            this.listViewLayers.Size = new System.Drawing.Size(200, 115);
            this.listViewLayers.SmallImageList = this.ImageListLayerType;
            this.listViewLayers.StateImageList = this.ImageListVisibleState;
            this.listViewLayers.TabIndex = 0;
            this.listViewLayers.TileSize = new System.Drawing.Size(60, 60);
            this.listViewLayers.UseCompatibleStateImageBehavior = false;
            this.listViewLayers.View = System.Windows.Forms.View.Details;
            this.listViewLayers.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewOverlay_ItemSelectionChanged);
            // 
            // ColumnHeaderLayerState
            // 
            this.ColumnHeaderLayerState.Text = "Status";
            this.ColumnHeaderLayerState.Width = 48;
            // 
            // ColumnHeaderLayerName
            // 
            this.ColumnHeaderLayerName.Text = "Couche";
            this.ColumnHeaderLayerName.Width = 113;
            // 
            // ImageListLayerType
            // 
            this.ImageListLayerType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListLayerType.ImageStream")));
            this.ImageListLayerType.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListLayerType.Images.SetKeyName(0, "category-access-upper.png");
            this.ImageListLayerType.Images.SetKeyName(1, "category-access-lower.png");
            // 
            // ImageListVisibleState
            // 
            this.ImageListVisibleState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListVisibleState.ImageStream")));
            this.ImageListVisibleState.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListVisibleState.Images.SetKeyName(0, "eye.png");
            this.ImageListVisibleState.Images.SetKeyName(1, "eye-close.png");
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddLayer});
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
            // LayerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 140);
            this.Controls.Add(this.listViewLayers);
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

        private System.Windows.Forms.ListView listViewLayers;
        private System.Windows.Forms.ColumnHeader ColumnHeaderLayerName;
        private System.Windows.Forms.ImageList ImageListVisibleState;
        private System.Windows.Forms.ColumnHeader ColumnHeaderLayerState;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddLayer;
        private System.Windows.Forms.ImageList ImageListLayerType;
    }
}