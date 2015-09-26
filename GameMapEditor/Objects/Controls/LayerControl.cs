using System;
using System.Drawing;
using System.Windows.Forms;
using GameMapEditor.Properties;

namespace GameMapEditor.Objects.Controls
{
    public delegate void ItemClickedEventArgs(object sender, EventArgs e);
    public delegate void ItemChangedEventArgs(object sender, EventArgs e);
    public delegate void ItemMenuItemClickedEventArgs(object sender, EventArgs e);

    public partial class LayerControl : UserControl
    {
        public static event ItemClickedEventArgs LayerClicked;
        public static event ItemClickedEventArgs LayerDoubleClicked;
        public static event ItemChangedEventArgs LayerVisibleStateChanged;
        public static event ItemChangedEventArgs LayerTypeChanged;
        public static event ItemMenuItemClickedEventArgs LayerRemoveButtonClicked;
        public static event ItemMenuItemClickedEventArgs LayerDownButtonClicked;
        public static event ItemMenuItemClickedEventArgs LayerUpButtonClicked;

        private static Color UnselectedBackColor = SystemColors.Window;
        private static Color SelectedBackColor = Color.LightSteelBlue;

        private LayerType type;
        private bool visible;
        private bool selected;

        public LayerControl(string name, bool visible, LayerType type)
        {
            InitializeComponent();

            this.Index = "0";
            this.Name = name;
            this.Text = name;
            this.Visible = visible;
            this.Type = type;
            this.Selected = false;
        }

        private void LayerControl_Load(object sender, EventArgs e)
        {
            this.Refresh();
        }

        public override void Refresh()
        {
            base.Refresh();

            // Limitation effectuée par la propriété MinimumSize du control
            this.Width = this.Parent.Width - 20;
        }

        public new string Text
        {
            get { return this.labelLayerName.Text; }
            set { this.labelLayerName.Text = value; }
        }

        public string Index
        {
            get { return this.labelIndex.Text; }
            set { this.labelIndex.Text = value; }
        }

        public new bool Visible
        {
            get { return this.visible; }
            set
            {
                this.visible = value;
                this.pictureBoxVisibleState.Image = this.Visible ? Resources.eye : Resources.eyeclose;
                LayerVisibleStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public LayerType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
                this.pictureBoxLayerType.Image = value == LayerType.Lower ? Resources.categoryaccesslower : Resources.categoryaccessupper;
                LayerTypeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool Selected
        {
            get { return this.selected; }
            set
            {
                this.selected = value;
                this.BackColor = value ? SelectedBackColor : UnselectedBackColor;
            }
        }

        private void LayerControl_MouseClick(object sender, MouseEventArgs e)
        {
            LayerClicked?.Invoke(this, EventArgs.Empty);
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip.Show(this, e.Location);
            }
        }

        private void LayerControl_DoubleClick(object sender, EventArgs e)
        {
            LayerDoubleClicked?.Invoke(this, EventArgs.Empty);
        }

        private void pictureBoxLayerType_Click(object sender, EventArgs e)
        {
            this.Type = this.type == LayerType.Lower ? LayerType.Upper : LayerType.Lower;
        }

        private void pictureBoxVisibleState_Click(object sender, EventArgs e)
        {
            this.Visible = !this.visible;
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            LayerRemoveButtonClicked?.Invoke(this, e);
        }

        private void toolStripMenuItemDown_Click(object sender, EventArgs e)
        {
            LayerDownButtonClicked?.Invoke(this, e);
        }

        private void toolStripMenuItemUp_Click(object sender, EventArgs e)
        {
            LayerUpButtonClicked?.Invoke(this, e);
        }
    }
}
