using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameMapEditor.Properties;

namespace GameMapEditor.Objects.Controls
{
    public delegate void ItemClickedEventArgs(object sender);
    public delegate void ItemChangedEventArgs(object sender);

    public partial class LayerControl : UserControl
    {
        public event ItemClickedEventArgs LayerClicked;
        public event ItemClickedEventArgs LayerDoubleClicked;
        public event ItemChangedEventArgs LayerVisibleStateChanged;
        public event ItemChangedEventArgs LayerTypeChanged;

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
                this.pictureBoxVisibleState.Image = this.Visible ? Resources.eye : Resources.eye_close;
                this.LayerVisibleStateChanged?.Invoke(this);
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
                this.pictureBoxLayerType.Image = value == LayerType.Lower ? Resources.category_access_lower : Resources.category_access_upper;
                this.LayerTypeChanged?.Invoke(this);
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

        private void LayerControl_Click(object sender, EventArgs e)
        {
            this.LayerClicked?.Invoke(this);
        }

        private void LayerControl_DoubleClick(object sender, EventArgs e)
        {
            this.LayerDoubleClicked?.Invoke(this);
        }

        private void pictureBoxLayerType_Click(object sender, EventArgs e)
        {
            this.Type = this.type == LayerType.Lower ? LayerType.Upper : LayerType.Lower;
        }

        private void pictureBoxVisibleState_Click(object sender, EventArgs e)
        {
            this.Visible = !this.visible;
        }
    }
}
