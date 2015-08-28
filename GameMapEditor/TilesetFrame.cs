using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public class TilesetFrame : DockContent
    {
        private static Pen DefaultCursorColor = Pens.DarkGreen;

        private PictureBox picTileset;
        private VScrollBar vPicTilesetScrollBar;
        private HScrollBar hPicTilesetScrollBar;

        private Point tilesetOrigin;
        private ComboBox comboTilesetFileSelecter;
        private Rectangle tilesetSelection;
        private List<Bitmap> tilesetImages;
        private Bitmap currentTilesetImage;

        public event TilesetChangedEventHandler TilesetChanged;
        public delegate void TilesetChangedEventHandler(object sender, Bitmap tileset);
        public event TilesetSelectionEventHandler TilesetSelectionChanged;
        public delegate void TilesetSelectionEventHandler(object sender, Rectangle selection);

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TilesetFrame));
            this.picTileset = new System.Windows.Forms.PictureBox();
            this.vPicTilesetScrollBar = new System.Windows.Forms.VScrollBar();
            this.hPicTilesetScrollBar = new System.Windows.Forms.HScrollBar();
            this.comboTilesetFileSelecter = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picTileset)).BeginInit();
            this.SuspendLayout();
            // 
            // picTileset
            // 
            this.picTileset.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picTileset.Location = new System.Drawing.Point(0, 27);
            this.picTileset.Name = "picTileset";
            this.picTileset.Size = new System.Drawing.Size(283, 256);
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
            this.vPicTilesetScrollBar.Location = new System.Drawing.Point(283, 27);
            this.vPicTilesetScrollBar.Name = "vPicTilesetScrollBar";
            this.vPicTilesetScrollBar.Size = new System.Drawing.Size(17, 256);
            this.vPicTilesetScrollBar.TabIndex = 1;
            this.vPicTilesetScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vPicTilesetScrollBar_Scroll);
            // 
            // hPicTilesetScrollBar
            // 
            this.hPicTilesetScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hPicTilesetScrollBar.Location = new System.Drawing.Point(0, 283);
            this.hPicTilesetScrollBar.Name = "hPicTilesetScrollBar";
            this.hPicTilesetScrollBar.Size = new System.Drawing.Size(283, 17);
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
            this.comboTilesetFileSelecter.Size = new System.Drawing.Size(300, 21);
            this.comboTilesetFileSelecter.TabIndex = 3;
            this.comboTilesetFileSelecter.SelectedIndexChanged += new System.EventHandler(this.comboTilesetFileSelecter_SelectedIndexChanged);
            // 
            // TilesetFrame
            // 
            this.ClientSize = new System.Drawing.Size(300, 300);
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
            this.Name = "TilesetFrame";
            this.Text = "Tileset";
            ((System.ComponentModel.ISupportInitialize)(this.picTileset)).EndInit();
            this.ResumeLayout(false);

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.InitializeComponent();

            this.tilesetOrigin = new Point();
            this.tilesetSelection = new Rectangle();
            this.tilesetImages = new List<Bitmap>();
            this.CursorColor = DefaultCursorColor;
            this.IsSelectingTiles = false;

            this.LoadTilesetFileList();
        }

        private void picTileset_Paint(object sender, PaintEventArgs e)
        {
            if (this.currentTilesetImage != null)
            {
                e.Graphics.Clear(Color.WhiteSmoke);
            
                e.Graphics.DrawImage(this.currentTilesetImage, e.ClipRectangle,
                    this.tilesetOrigin.X,
                    this.tilesetOrigin.Y,
                    e.ClipRectangle.Width,
                    e.ClipRectangle.Height,
                    GraphicsUnit.Pixel);
            
                e.Graphics.DrawRectangle(CursorColor,
                    this.TilesetSelection.Location.X - this.tilesetOrigin.X,
                    this.tilesetSelection.Location.Y - this.tilesetOrigin.Y,
                    this.tilesetSelection.Size.Width,
                    this.tilesetSelection.Size.Height);
            }
        }

        private void picTileset_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.currentTilesetImage != null)
            {
                Point pt = new Point(e.Location.X + this.TilesetOrigin.X, e.Location.Y + this.TilesetOrigin.Y);
                Rectangle rect = new Rectangle(Point.Empty, this.TilesetImage.Size);

                if (rect.Contains(pt))
                {
                    pt.X = (pt.X / GlobalData.TileSize.Width) * GlobalData.TileSize.Width;
                    pt.Y = (pt.Y / GlobalData.TileSize.Height) * GlobalData.TileSize.Height;

                    this.tilesetSelection.Location = pt;
                    this.tilesetSelection.Size = GlobalData.TileSize;
                    this.IsSelectingTiles = true;
                    this.picTileset.Refresh();
                }
            }
        }

        private void picTileset_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.currentTilesetImage != null)
            {
                Point pt = new Point(e.Location.X + this.TilesetOrigin.X, e.Location.Y + this.TilesetOrigin.Y);
                Rectangle rect = new Rectangle(Point.Empty, this.TilesetImage.Size);

                if (rect.Contains(pt) && this.IsSelectingTiles && pt.X > this.TilesetSelection.Location.X && pt.Y > this.TilesetSelection.Location.Y)
                {
                    int tmpWidth = (int)((pt.X - this.TilesetSelection.X) / GlobalData.TileSize.Width) + 1;
                    int tmpHeight = (int)((pt.Y - this.TilesetSelection.Location.Y) / GlobalData.TileSize.Height) + 1;

                    this.tilesetSelection.Size = new Size(tmpWidth * GlobalData.TileSize.Width, tmpHeight * GlobalData.TileSize.Height);
                    this.picTileset.Refresh();

                }
            }
        }

        private void picTileset_MouseUp(object sender, MouseEventArgs e)
        {
            this.IsSelectingTiles = false;
            this.RaiseTilesetSelectionChangedEvent(this.TilesetSelection);
        }

        private void vPicTilesetScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            
            this.tilesetOrigin.Y = vPicTilesetScrollBar.Value;
            this.picTileset.Refresh();
        }

        private void hPicTilesetScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.tilesetOrigin.X = hPicTilesetScrollBar.Value;
            this.picTileset.Refresh();
        }

        private void picTileset_SizeChanged(object sender, EventArgs e)
        {
            this.RefreshScrollComponents(this.hPicTilesetScrollBar.Value, this.vPicTilesetScrollBar.Value);
        }

        private void comboTilesetFileSelecter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tilesetImages.Count > this.comboTilesetFileSelecter.SelectedIndex)
            {
                this.currentTilesetImage = this.tilesetImages[this.comboTilesetFileSelecter.SelectedIndex];
                this.RaiseTilesetChangedEvent(this.currentTilesetImage);
                this.RefreshScrollComponents();
            }
        }

        private void LoadTilesetFileList()
        {
            string[] tilesetFiles = Directory.GetFiles(".", "*.png", SearchOption.TopDirectoryOnly);
            foreach (string file in tilesetFiles)
            {
                this.tilesetImages.Add(new Bitmap(Bitmap.FromFile(file)));
                this.comboTilesetFileSelecter.Items.Add(Path.GetFileName(file));
            }
        }

        private void RefreshScrollComponents(int scrollX = 0, int scrollY = 0)
        {
            if (this.currentTilesetImage != null)
            {
                this.vPicTilesetScrollBar.Minimum = 0;
                this.vPicTilesetScrollBar.SmallChange = this.currentTilesetImage.Size.Height / 20;
                this.vPicTilesetScrollBar.LargeChange = this.currentTilesetImage.Size.Height / 5;
                int scrollHeightValue = this.currentTilesetImage.Size.Height + 50 - this.picTileset.Size.Height;
                this.vPicTilesetScrollBar.Maximum = scrollHeightValue > 0 ? scrollHeightValue : 1;
                this.vPicTilesetScrollBar.Maximum += this.vPicTilesetScrollBar.LargeChange;
                this.vPicTilesetScrollBar.Value = scrollY < this.vPicTilesetScrollBar.Maximum ? scrollY : this.vPicTilesetScrollBar.Maximum;

                this.hPicTilesetScrollBar.Minimum = 0;
                this.hPicTilesetScrollBar.SmallChange = this.currentTilesetImage.Size.Width / 20;
                this.hPicTilesetScrollBar.LargeChange = this.currentTilesetImage.Size.Width / 5;
                int scrollWidthValue = this.currentTilesetImage.Size.Width + 50 - this.picTileset.Size.Width;
                this.hPicTilesetScrollBar.Maximum = scrollWidthValue > 0 ? scrollWidthValue : 1;
                this.hPicTilesetScrollBar.Maximum += this.hPicTilesetScrollBar.LargeChange;
                this.hPicTilesetScrollBar.Value = scrollX < this.hPicTilesetScrollBar.Maximum ? scrollX : this.hPicTilesetScrollBar.Maximum;

                this.picTileset.Refresh();
            }
        }

        private void RaiseTilesetChangedEvent(Bitmap tileset)
        {
            if(this.TilesetChanged != null)
            {
                this.TilesetChanged(this, tileset);
            }
        }

        private void RaiseTilesetSelectionChangedEvent(Rectangle selection)
        {
            if(this.TilesetSelectionChanged != null)
            {
                this.TilesetSelectionChanged(this, selection);
            }
        }

        public Pen CursorColor
        {
            get;
            set;
        }

        public bool IsSelectingTiles
        {
            get;
            private set;
        }

        public Point TilesetOrigin
        {
            get { return this.tilesetOrigin; }
        }

        public Rectangle TilesetSelection
        {
            get { return this.tilesetSelection; }
        }

        public Bitmap TilesetImage
        {
            get { return this.currentTilesetImage; }
        }
    }
}
