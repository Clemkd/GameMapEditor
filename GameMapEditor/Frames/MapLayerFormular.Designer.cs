namespace GameMapEditor.Frames
{
    partial class MapLayerFormular
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapLayerFormular));
            this.checkBoxLayerState = new System.Windows.Forms.CheckBox();
            this.ImageListVisibleState = new System.Windows.Forms.ImageList(this.components);
            this.ImageListLayerType = new System.Windows.Forms.ImageList(this.components);
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonValidNewOverlay = new System.Windows.Forms.Button();
            this.groupBoxOverlay = new System.Windows.Forms.GroupBox();
            this.checkBoxLayerType = new System.Windows.Forms.CheckBox();
            this.groupBoxOverlay.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxLayerState
            // 
            this.checkBoxLayerState.Checked = true;
            this.checkBoxLayerState.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLayerState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.checkBoxLayerState.ImageIndex = 0;
            this.checkBoxLayerState.ImageList = this.ImageListVisibleState;
            this.checkBoxLayerState.Location = new System.Drawing.Point(21, 85);
            this.checkBoxLayerState.Name = "checkBoxLayerState";
            this.checkBoxLayerState.Size = new System.Drawing.Size(105, 34);
            this.checkBoxLayerState.TabIndex = 0;
            this.checkBoxLayerState.Text = "Visible";
            this.checkBoxLayerState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxLayerState.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.checkBoxLayerState.UseVisualStyleBackColor = true;
            this.checkBoxLayerState.CheckedChanged += new System.EventHandler(this.checkBoxVisibleState_CheckedChanged);
            // 
            // ImageListVisibleState
            // 
            this.ImageListVisibleState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListVisibleState.ImageStream")));
            this.ImageListVisibleState.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListVisibleState.Images.SetKeyName(0, "eye.png");
            this.ImageListVisibleState.Images.SetKeyName(1, "eye-close.png");
            // 
            // ImageListLayerType
            // 
            this.ImageListLayerType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListLayerType.ImageStream")));
            this.ImageListLayerType.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListLayerType.Images.SetKeyName(0, "category-access-upper.png");
            this.ImageListLayerType.Images.SetKeyName(1, "category-access-lower.png");
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(18, 28);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Nom :";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(21, 44);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(257, 20);
            this.textBoxName.TabIndex = 2;
            // 
            // buttonValidNewOverlay
            // 
            this.buttonValidNewOverlay.Location = new System.Drawing.Point(224, 158);
            this.buttonValidNewOverlay.Name = "buttonValidNewOverlay";
            this.buttonValidNewOverlay.Size = new System.Drawing.Size(88, 23);
            this.buttonValidNewOverlay.TabIndex = 3;
            this.buttonValidNewOverlay.Text = "Valider";
            this.buttonValidNewOverlay.UseVisualStyleBackColor = true;
            this.buttonValidNewOverlay.Click += new System.EventHandler(this.buttonValidNewOverlay_Click);
            // 
            // groupBoxOverlay
            // 
            this.groupBoxOverlay.Controls.Add(this.checkBoxLayerType);
            this.groupBoxOverlay.Controls.Add(this.textBoxName);
            this.groupBoxOverlay.Controls.Add(this.checkBoxLayerState);
            this.groupBoxOverlay.Controls.Add(this.labelName);
            this.groupBoxOverlay.Location = new System.Drawing.Point(12, 12);
            this.groupBoxOverlay.Name = "groupBoxOverlay";
            this.groupBoxOverlay.Size = new System.Drawing.Size(300, 140);
            this.groupBoxOverlay.TabIndex = 4;
            this.groupBoxOverlay.TabStop = false;
            this.groupBoxOverlay.Text = "Couche";
            // 
            // checkBoxLayerType
            // 
            this.checkBoxLayerType.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.checkBoxLayerType.ImageIndex = 1;
            this.checkBoxLayerType.ImageList = this.ImageListLayerType;
            this.checkBoxLayerType.Location = new System.Drawing.Point(132, 85);
            this.checkBoxLayerType.Name = "checkBoxLayerType";
            this.checkBoxLayerType.Size = new System.Drawing.Size(146, 34);
            this.checkBoxLayerType.TabIndex = 4;
            this.checkBoxLayerType.Text = "Couche supérieure";
            this.checkBoxLayerType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxLayerType.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.checkBoxLayerType.UseVisualStyleBackColor = true;
            this.checkBoxLayerType.CheckedChanged += new System.EventHandler(this.checkBoxLayerType_CheckedChanged);
            // 
            // MapLayerFormular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 191);
            this.Controls.Add(this.groupBoxOverlay);
            this.Controls.Add(this.buttonValidNewOverlay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapLayerFormular";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nouvelle couche";
            this.groupBoxOverlay.ResumeLayout(false);
            this.groupBoxOverlay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxLayerState;
        private System.Windows.Forms.ImageList ImageListVisibleState;
        private System.Windows.Forms.ImageList ImageListLayerType;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonValidNewOverlay;
        private System.Windows.Forms.GroupBox groupBoxOverlay;
        private System.Windows.Forms.CheckBox checkBoxLayerType;
    }
}