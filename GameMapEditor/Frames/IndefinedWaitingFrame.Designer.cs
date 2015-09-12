namespace GameMapEditor
{
    partial class IndefinedWaitingFrame
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
            this.progressBarWaintingFrame = new System.Windows.Forms.ProgressBar();
            this.labelWaintingFrame = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBarWaintingFrame
            // 
            this.progressBarWaintingFrame.Location = new System.Drawing.Point(55, 47);
            this.progressBarWaintingFrame.Name = "progressBarWaintingFrame";
            this.progressBarWaintingFrame.Size = new System.Drawing.Size(293, 23);
            this.progressBarWaintingFrame.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarWaintingFrame.TabIndex = 4;
            this.progressBarWaintingFrame.UseWaitCursor = true;
            // 
            // labelWaintingFrame
            // 
            this.labelWaintingFrame.AutoSize = true;
            this.labelWaintingFrame.Location = new System.Drawing.Point(52, 31);
            this.labelWaintingFrame.Name = "labelWaintingFrame";
            this.labelWaintingFrame.Size = new System.Drawing.Size(203, 13);
            this.labelWaintingFrame.TabIndex = 3;
            this.labelWaintingFrame.Text = "Chargement en cours... Veuillez patienter.";
            this.labelWaintingFrame.UseWaitCursor = true;
            // 
            // IndefinedWaitingFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 125);
            this.ControlBox = false;
            this.Controls.Add(this.progressBarWaintingFrame);
            this.Controls.Add(this.labelWaintingFrame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "IndefinedWaitingFrame";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Chargement";
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBarWaintingFrame;
        private System.Windows.Forms.Label labelWaintingFrame;
    }
}