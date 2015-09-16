namespace GameMapEditor.Frames
{
    partial class MapPanelFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapPanelFrame));
            this.lblMapName = new System.Windows.Forms.Label();
            this.grpMapInformation = new System.Windows.Forms.GroupBox();
            this.txtMapName = new System.Windows.Forms.TextBox();
            this.btnValidNewMap = new System.Windows.Forms.Button();
            this.grpMapInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMapName
            // 
            this.lblMapName.AutoSize = true;
            this.lblMapName.Location = new System.Drawing.Point(25, 43);
            this.lblMapName.Name = "lblMapName";
            this.lblMapName.Size = new System.Drawing.Size(88, 13);
            this.lblMapName.TabIndex = 0;
            this.lblMapName.Text = "Nom de la carte :";
            // 
            // grpMapInformation
            // 
            this.grpMapInformation.Controls.Add(this.txtMapName);
            this.grpMapInformation.Controls.Add(this.lblMapName);
            this.grpMapInformation.Location = new System.Drawing.Point(12, 12);
            this.grpMapInformation.Name = "grpMapInformation";
            this.grpMapInformation.Size = new System.Drawing.Size(426, 258);
            this.grpMapInformation.TabIndex = 1;
            this.grpMapInformation.TabStop = false;
            this.grpMapInformation.Text = "Informations";
            // 
            // txtMapName
            // 
            this.txtMapName.Location = new System.Drawing.Point(28, 59);
            this.txtMapName.Name = "txtMapName";
            this.txtMapName.Size = new System.Drawing.Size(364, 20);
            this.txtMapName.TabIndex = 1;
            this.txtMapName.Leave += new System.EventHandler(this.txtMapName_Leave);
            // 
            // btnValidNewMap
            // 
            this.btnValidNewMap.Location = new System.Drawing.Point(321, 287);
            this.btnValidNewMap.Name = "btnValidNewMap";
            this.btnValidNewMap.Size = new System.Drawing.Size(117, 30);
            this.btnValidNewMap.TabIndex = 2;
            this.btnValidNewMap.Text = "Valider";
            this.btnValidNewMap.UseVisualStyleBackColor = true;
            this.btnValidNewMap.Click += new System.EventHandler(this.btnValidNewMap_Click);
            // 
            // MapPanelFrame
            // 
            this.AcceptButton = this.btnValidNewMap;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 329);
            this.Controls.Add(this.btnValidNewMap);
            this.Controls.Add(this.grpMapInformation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapPanelFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nouvelle carte";
            this.grpMapInformation.ResumeLayout(false);
            this.grpMapInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMapName;
        private System.Windows.Forms.GroupBox grpMapInformation;
        private System.Windows.Forms.TextBox txtMapName;
        private System.Windows.Forms.Button btnValidNewMap;
    }
}