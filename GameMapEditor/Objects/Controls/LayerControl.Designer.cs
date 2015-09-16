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
            this.pictureBoxVisibleState = new System.Windows.Forms.PictureBox();
            this.pictureBoxLayerType = new System.Windows.Forms.PictureBox();
            this.labelLayerName = new System.Windows.Forms.Label();
            this.labelIndex = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVisibleState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLayerType)).BeginInit();
            this.SuspendLayout();
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
            // labelLayerName
            // 
            this.labelLayerName.AutoSize = true;
            this.labelLayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLayerName.Location = new System.Drawing.Point(72, 8);
            this.labelLayerName.Name = "labelLayerName";
            this.labelLayerName.Size = new System.Drawing.Size(42, 16);
            this.labelLayerName.TabIndex = 2;
            this.labelLayerName.Text = "Layer";
            this.labelLayerName.Click += new System.EventHandler(this.LayerControl_Click);
            this.labelLayerName.DoubleClick += new System.EventHandler(this.LayerControl_DoubleClick);
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
            this.Click += new System.EventHandler(this.LayerControl_Click);
            this.DoubleClick += new System.EventHandler(this.LayerControl_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVisibleState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLayerType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxVisibleState;
        private System.Windows.Forms.PictureBox pictureBoxLayerType;
        private System.Windows.Forms.Label labelLayerName;
        private System.Windows.Forms.Label labelIndex;
    }
}
