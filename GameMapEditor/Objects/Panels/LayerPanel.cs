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
    public delegate void MapLayerAddedEventArgs(object sender, GameMapLayer layer);
    public delegate void MapLayerSelectionChangedEventArgs(object sender, int index);

    public partial class LayerPanel : DockContent, IDisposable
    {
        public event MapLayerAddedEventArgs MapLayerAdded;
        public event MapLayerSelectionChangedEventArgs MapLayerSelectionChanged;

        public static LayerPanel Instance = new LayerPanel();

        private LayerPanel()
        {
            this.HideOnClose = true;
            InitializeComponent();

            LayerControl.LayerVisibleStateChanged += Layer_VisibleStateChanged;
            LayerControl.LayerTypeChanged += Layer_TypeChanged;
            LayerControl.LayerRemoveButtonClicked += LayerControl_LayerRemoveButtonClicked;
            LayerControl.LayerDownButtonClicked += LayerControl_LayerDownButtonClicked;
            LayerControl.LayerUpButtonClicked += LayerControl_LayerUpButtonClicked;
        }

        private void LayerControl_LayerUpButtonClicked(object sender, EventArgs e)
        {
            LayerControl control = sender as LayerControl;
            int index = this.layerPanelCTM.Controls.IndexOf(control);
            this.SwapUp(index);
        }

        private void LayerControl_LayerDownButtonClicked(object sender, EventArgs e)
        {
            LayerControl control = sender as LayerControl;
            int index = this.layerPanelCTM.Controls.IndexOf(control);
            this.SwapDown(index);
        }

        private void LayerControl_LayerRemoveButtonClicked(object sender, EventArgs e)
        {
            LayerControl control = sender as LayerControl;
            int index = this.layerPanelCTM.Controls.IndexOf(control);
            this.Remove(index);
        }

        /// <summary>
        /// Échange l'élément par celui de dessous si possible
        /// </summary>
        /// <param name="index">L'index de l'élément à échanger de place</param>
        private void SwapDown(int index)
        {
            if (this.layerPanelCTM.Controls.Count > 0)
            {
                MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                if (mapPanel != null && mapPanel.Map.SwapLayers(index, index + 1))
                {
                    this.layerPanelCTM.Swap(index, index + 1);
                }
            }
        }

        /// <summary>
        /// Échange l'élément par celui de dessus si possible
        /// </summary>
        /// <param name="index">L'index de l'élément à échanger de place</param>
        private void SwapUp(int index)
        {
            if (this.layerPanelCTM.Controls.Count > 0)
            {
                MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                if (mapPanel != null && mapPanel.Map.SwapLayers(index, index - 1))
                {
                    this.layerPanelCTM.Swap(index, index - 1);
                }
            }
        }

        public void Clear()
        {
            this.layerPanelCTM.Controls.Clear();
        }

        /// <summary>
        /// Vide la liste et charge les layers de la carte
        /// </summary>
        /// <param name="map">La carte parente des layers à charger</param>
        /// <param name="index">L'index du layer selectionné (0 par défaut)</param>
        public void LoadLayers(GameMap map, int index = 0)
        {
            this.Clear();

            foreach (GameMapLayer layer in map.Layers)
            {
                this.layerPanelCTM.Add(layer);
            }
            this.layerPanelCTM.SelectedIndex = index;
            this.layerPanelCTM.Refresh();
        }

        /// <summary>
        /// Met à jour le panel et recharge la liste de layers
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();

            MapPanel mapPanel = this.DockPanel.ActiveDocument as MapPanel;
            this.Enabled = mapPanel != null;
            if (mapPanel != null)
            {
                // TODO : Réviser
                this.LoadLayers(mapPanel.Map, mapPanel.SelectedLayerIndex);
            }
        }

        public void Add(GameMapLayer layer)
        {
            this.layerPanelCTM.Insert(0, layer);
        }

        public void Remove(int index)
        {
            MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
            if (mapPanel != null)
            {
                mapPanel.Map.RemoveLayerAt(index);
                this.layerPanelCTM.Remove(index);
            }
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
            this.SwapUp(this.layerPanelCTM.SelectedIndex);
        }

        private void toolStripButtonDownLayer_Click(object sender, EventArgs e)
        {
            this.SwapDown(this.layerPanelCTM.SelectedIndex);
        }

        private void toolStripButtonRemoveLayer_Click(object sender, EventArgs e)
        {
            if (this.layerPanelCTM.Controls.Count > 1)
            {
                this.Remove(this.layerPanelCTM.SelectedIndex);
            }
            else ConsolePanel.Instance.WriteLine("Impossible de supprimer la dernière couche de la carte", RowType.Error);
        }

        private void Formular_MapLayerAdded(object sender, GameMapLayer layer)
        {
            if (this.layerPanelCTM.Controls.Count < GameMap.MAX_LAYER_COUNT)
            {
                this.Add(layer);
                this.MapLayerAdded?.Invoke(sender, layer);
            }
            else
                ConsolePanel.Instance.
                    WriteLine("Impossible d'ajouter la nouvelle couche : Le nombre maximal de couches est atteint (" +
                    GameMap.MAX_LAYER_COUNT + ")", RowType.Error);
        }

        private void layerPanelCTM_ItemSelectionChanged(object sender, int index)
        {
            this.MapLayerSelectionChanged?.Invoke(this, index);
        }

        private void Layer_TypeChanged(object sender, EventArgs e)
        {
            if (this.layerPanelCTM.Controls.Count > 0)
            {
                MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                if (mapPanel != null)
                {
                    var layerControl = sender as LayerControl;
                    int index = this.layerPanelCTM.Controls.IndexOf(layerControl);
                    GameMapLayer layer = mapPanel.Map?.GetLayerAt(index);

                    if (layer != null)
                        layer.Type = layer.Type == LayerType.Lower ? LayerType.Upper : LayerType.Lower;
                }
            }
        }

        // TODO : Corriger (disfonctionnement survenu après un Undo, l'état d'affichage ne se modifie pas)
        private void Layer_VisibleStateChanged(object sender, EventArgs e)
        {
            if (this.layerPanelCTM.Controls.Count > 0)
            {
                MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                if (mapPanel != null)
                {
                    var layerControl = sender as LayerControl;
                    int index = this.layerPanelCTM.Controls.IndexOf(layerControl);

                    GameMapLayer layer = mapPanel.Map?.GetLayerAt(index);
                    if (layer != null)
                        layer.Visible = !layer.Visible;
                }
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if(!this.Enabled)
                this.Clear();
        }

        /// <summary>
        /// Obtient le nombre de layers présents dans la liste du control
        /// </summary>
        public int LayersCount
        {
            get { return this.layerPanelCTM.Controls.Count; }
        }
    }
}
