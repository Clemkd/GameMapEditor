using System.Windows.Forms;

namespace GameMapEditor.Objects.Controls
{
    partial class LayerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelLayerName = new System.Windows.Forms.Label();
            this.labelIndex = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemUp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pictureBoxLayerType = new System.Windows.Forms.PictureBox();
            this.pictureBoxVisibleState = new System.Windows.Forms.PictureBox();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLayerType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVisibleState)).BeginInit();
            this.SuspendLayout();
            // 
            // labelLayerName
            // 
            this.labelLayerName.AutoSize = true;
            this.labelLayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLayerName.Location = new System.Drawing.Point(72, 8);
            this.labelLayerName.Name = "labelLayerName";
            this.labelLayerName.Size = new System.Drawing.Size(42, 16);
            this.labelLayerName.TabIndex = 2;
            this.labelLayerName.Text = "Layer";
            this.labelLayerName.DoubleClick += new System.EventHandler(this.LayerControl_DoubleClick);
            this.labelLayerName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LayerControl_MouseClick);
            // 
            // labelIndex
            // 
            this.labelIndex.AutoSize = true;
            this.labelIndex.BackColor = System.Drawing.Color.Transparent;
            this.labelIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIndex.Location = new System.Drawing.Point(50, 15);
            this.labelIndex.Name = "labelIndex";
            this.labelIndex.Size = new System.Drawing.Size(11, 12);
            this.labelIndex.TabIndex = 0;
            this.labelIndex.Text = "0";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDelete,
            this.toolStripSeparator1,
            this.toolStripMenuItemDown,
            this.toolStripMenuItemUp});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(153, 98);
            // 
            // toolStripMenuItemDown
            // 
            this.toolStripMenuItemDown.Image = global::GameMapEditor.Properties.Resources.arrow270medium;
            this.toolStripMenuItemDown.Name = "toolStripMenuItemDown";
            this.toolStripMenuItemDown.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemDown.Text = "Descendre";
            this.toolStripMenuItemDown.Click += new System.EventHandler(this.toolStripMenuItemDown_Click);
            // 
            // toolStripMenuItemUp
            // 
            this.toolStripMenuItemUp.Image = global::GameMapEditor.Properties.Resources.arrow090medium;
            this.toolStripMenuItemUp.Name = "toolStripMenuItemUp";
            this.toolStripMenuItemUp.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemUp.Text = "Monter";
            this.toolStripMenuItemUp.Click += new System.EventHandler(this.toolStripMenuItemUp_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(126, 6);
            // 
            // pictureBoxLayerType
            // 
            this.pictureBoxLayerType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxLayerType.Image = global::GameMapEditor.Properties.Resources.categoryaccessupper;
            this.pictureBoxLayerType.Location = new System.Drawing.Point(33, 6);
            this.pictureBoxLayerType.Name = "pictureBoxLayerType";
            this.pictureBoxLayerType.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxLayerType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxLayerType.TabIndex = 1;
            this.pictureBoxLayerType.TabStop = false;
            this.pictureBoxLayerType.Click += new System.EventHandler(this.pictureBoxLayerType_Click);
            // 
            // pictureBoxVisibleState
            // 
            this.pictureBoxVisibleState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxVisibleState.Location = new System.Drawing.Point(7, 6);
            this.pictureBoxVisibleState.Name = "pictureBoxVisibleState";
            this.pictureBoxVisibleState.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxVisibleState.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxVisibleState.TabIndex = 0;
            this.pictureBoxVisibleState.TabStop = false;
            this.pictureBoxVisibleState.Click += new System.EventHandler(this.pictureBoxVisibleState_Click);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Image = global::GameMapEditor.Properties.Resources.crossscript;
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemDelete.Text = "Supprimer";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // LayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelIndex);
            this.Controls.Add(this.labelLayerName);
            this.Controls.Add(this.pictureBoxLayerType);
            this.Controls.Add(this.pictureBoxVisibleState);
            this.MinimumSize = new System.Drawing.Size(159, 32);
            this.Name = "LayerControl";
            this.Size = new System.Drawing.Size(159, 32);
            this.Load += new System.EventHandler(this.LayerControl_Load);
            this.DoubleClick += new System.EventHandler(this.LayerControl_DoubleClick);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LayerControl_MouseClick);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLayerType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVisibleState)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxVisibleState;
        private System.Windows.Forms.PictureBox pictureBoxLayerType;
        private System.Windows.Forms.Label labelLayerName;
        private System.Windows.Forms.Label labelIndex;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem toolStripMenuItemDelete;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuItemDown;
        private ToolStripMenuItem toolStripMenuItemUp;
    }
}
