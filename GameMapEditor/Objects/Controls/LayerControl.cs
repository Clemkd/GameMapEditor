using System;
using System.Drawing;
using System.Windows.Forms;
using GameMapEditor.Properties;

namespace GameMapEditor.Objects.Controls
{
    public delegate void ClickedEventArgs(object sender);
    public delegate void VisibleStateChangedEventArgs(object sender, bool value);
    public delegate void TypeChangedEventArgs(object sender, LayerType type);
    public delegate void MenuItemClickedEventArgs(object sender);

    public partial class LayerControl : UserControl
    {
        public static event ClickedEventArgs LayerClicked;
        public static event ClickedEventArgs LayerDoubleClicked;
        public static event VisibleStateChangedEventArgs LayerVisibleStateChanged;
        public static event TypeChangedEventArgs LayerTypeChanged;
        public static event MenuItemClickedEventArgs LayerRemoveButtonClicked;
        public static event MenuItemClickedEventArgs LayerDownButtonClicked;
        public static event MenuItemClickedEventArgs LayerUpButtonClicked;

        private static Color UnselectedBackColor = SystemColors.Window;
        private static Color SelectedBackColor = Color.LightSteelBlue;

        private LayerType type;
        private bool visible;
        private bool selected;

        public LayerControl(string name, bool visible, LayerType type)
        {
            this.visible = visible;
            this.type = type;
            this.Initialize();
            this.Index = "0";
            this.Name = name;
            this.Text = name;
            this.Selected = false;
        }

        protected void Initialize()
        {
            this.InitializeComponent();
            this.pictureBoxVisibleState.Image = this.visible ? Resources.eye : Resources.eyeclose;
            this.pictureBoxLayerType.Image = this.type == LayerType.Lower ? Resources.categoryaccesslower : Resources.categoryaccessupper;
        }

        private void LayerControl_Load(object sender, EventArgs e)
        {
            this.Refresh();
        }

        public override void Refresh()
        {
            base.Refresh();

            // Limitation effectuée par la propriété MinimumSize du control
            this.Width = this.Parent.Width;
        }


        private void LayerControl_MouseClick(object sender, MouseEventArgs e)
        {
            LayerClicked?.Invoke(this);
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip.Show(this, e.Location);
            }
        }

        private void LayerControl_DoubleClick(object sender, EventArgs e)
        {
            LayerDoubleClicked?.Invoke(this);
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
            LayerRemoveButtonClicked?.Invoke(this);
        }

        private void toolStripMenuItemDown_Click(object sender, EventArgs e)
        {
            LayerDownButtonClicked?.Invoke(this);
        }

        private void toolStripMenuItemUp_Click(object sender, EventArgs e)
        {
            LayerUpButtonClicked?.Invoke(this);
        }

        /// <summary>
        /// Obtient ou définit le texte affiché sur la représentation graphique du control, correspondant au nom du layer associé
        /// </summary>
        public new string Text
        {
            get { return this.labelLayerName.Text; }
            set { this.labelLayerName.Text = value; }
        }

        /// <summary>
        /// Obtient ou définit l'index affiché sur la représentation graphique du control
        /// </summary>
        public string Index
        {
            get { return this.labelIndex.Text; }
            set { this.labelIndex.Text = value; }
        }

        /// <summary>
        /// Obtient ou définit la l'état de visibilité du layer associé
        /// </summary>
        public new bool Visible
        {
            get { return this.visible; }
            set
            {
                this.visible = value;
                this.pictureBoxVisibleState.Image = this.Visible ? Resources.eye : Resources.eyeclose;
                LayerVisibleStateChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Obtient ou définit le type du layer associé
        /// </summary>
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
                LayerTypeChanged?.Invoke(this, this.type);
            }
        }

        /// <summary>
        /// Obtient ou définit l'état de selection du control
        /// </summary>
        public bool Selected
        {
            get { return this.selected; }
            set
            {
                this.selected = value;
                this.BackColor = value ? SelectedBackColor : UnselectedBackColor;
            }
        }
    }
}
