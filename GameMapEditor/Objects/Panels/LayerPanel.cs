using GameMapEditor.Frames;
using GameMapEditor.Objects;
using GameMapEditor.Objects.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public partial class LayerPanel : DockContent, IDisposable
    {
        public static LayerPanel Instance = new LayerPanel();

        private LayerPanel()
        {
            this.HideOnClose = true;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            this.Initialize();
        }

        public virtual void Initialize()
        {
            this.InitializeComponent();
            this.Enabled = false;
            this.LayerPane.Initialize();
            this.LayerPane.ItemsCountChanged += LayerPane_ItemsCountChanged;
        }

        public virtual void Reset()
        {
            this.LayerPane.Initialize();
            this.toolStripLabelLayerCount.Text = "...";
            this.Enabled = false;
        }

        private void LayerPane_ItemsCountChanged(object sender, int count)
        {
            this.toolStripLabelLayerCount.Text = $"({count}/{GameMap.MAX_LAYER_COUNT})";
        }

        /// <summary>
        /// Vide la liste des layers
        /// </summary>
        public void Clear()
        {
            this.LayerPane.Clear();
        }

        private void toolStripButtonAddLayer_Click(object sender, System.EventArgs e)
        {
            MapLayerFrame formular = new MapLayerFrame();
            formular.MapLayerAdded += Formular_MapLayerAdded;
            formular.ShowDialog();
            formular.MapLayerAdded -= Formular_MapLayerAdded;
        }

        private void toolStripButtonUpLayer_Click(object sender, EventArgs e)
        {
            this.LayerPane.Swap(this.LayerPane.SelectedIndex, this.LayerPane.SelectedIndex - 1);
        }

        private void toolStripButtonDownLayer_Click(object sender, EventArgs e)
        {
            this.LayerPane.Swap(this.LayerPane.SelectedIndex, this.LayerPane.SelectedIndex + 1);
        }

        private void toolStripButtonRemoveLayer_Click(object sender, EventArgs e)
        {
            if (this.LayerPane.ControlsCount > 0)
            {
                this.LayerPane.RemoveAt(this.layerPanelControl.SelectedIndex);
            }
        }

        private void Formular_MapLayerAdded(object sender, GameMapLayer layer)
        {
            if (this.layerPanelControl.Controls.Count < GameMap.MAX_LAYER_COUNT)
            {
                this.LayerPane.InsertAt(0, layer);
                this.Refresh();
            }
            else
                ConsolePanel.Instance.WriteLine($"Impossible d'ajouter la nouvelle couche. Le nombre maximal de couches est atteint (maximum : {GameMap.MAX_LAYER_COUNT})", RowType.Error);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if(!this.Enabled)
                this.Clear();
        }

        public LayerPanelControl LayerPane
        {
            get { return this.layerPanelControl; }
        }
    }
}
