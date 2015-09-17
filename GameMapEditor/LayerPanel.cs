﻿using GameMapEditor.Frames;
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
    public delegate void MapLayerAddedEventArgs(GameMapLayer layer);
    public delegate void MapLayerSelectionChangedEventArgs(int index);

    public partial class LayerPanel : DockContent, IDisposable
    {
        public event MapLayerAddedEventArgs MapLayerAdded;
        public event MapLayerSelectionChangedEventArgs MapLayerSelectionChanged;

        public static LayerPanel Instance = new LayerPanel();

        private LayerPanel()
        {
            this.HideOnClose = true;
            InitializeComponent();
        }

        public void Clear()
        {
            this.layerPanelCTM.Controls.Clear();
        }

        public void LoadLayers(GameMap map)
        {
            this.Clear();

            foreach (GameMapLayer layer in map.Layers)
            {
                this.layerPanelCTM.Add(layer);
            }
            this.layerPanelCTM.SelectedIndex = 0;
        }

        public override void Refresh()
        {
            base.Refresh();

            MapPanel mapPanel = this.DockPanel.ActiveDocument as MapPanel;
            this.Enabled = mapPanel != null;
            if (mapPanel != null)
            {
                this.LoadLayers(mapPanel.Map);
                this.layerPanelCTM.SelectedIndex = mapPanel.SelectedLayerIndex;
            }
        }

        public void Add(GameMapLayer layer)
        {
            this.layerPanelCTM.Insert(0, layer);
        }

        public void RemoveLayer(int index)
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
            if (this.layerPanelCTM.Controls.Count > 0)
            {
                int index1 = this.layerPanelCTM.SelectedIndex;
                MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                if (mapPanel != null && mapPanel.Map.SwapLayers(index1, index1 - 1))
                {
                    this.layerPanelCTM.Swap(index1, index1 - 1);
                }
            }
        }

        private void toolStripButtonDownLayer_Click(object sender, EventArgs e)
        {
            if(this.layerPanelCTM.Controls.Count > 0)
            {
                int index1 = this.layerPanelCTM.SelectedIndex;
                MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                if (mapPanel != null && mapPanel.Map.SwapLayers(index1, index1 + 1))
                {
                    this.layerPanelCTM.Swap(index1, index1 + 1);
                }
            }   
        }

        private void toolStripButtonRemoveLayer_Click(object sender, EventArgs e)
        {
            if (this.layerPanelCTM.Controls.Count > 1)
            {
                this.RemoveLayer(this.layerPanelCTM.SelectedIndex);
            }
            else ConsolePanel.Instance.WriteLine("Impossible de supprimer la dernière couche de la carte", RowType.Error);
        }

        private void toolStripButtonSetVisibleState_Click(object sender, EventArgs e)
        {
            this.layerPanelCTM_LayerVisibleStateChanged(sender);
        }

        private void Formular_MapLayerAdded(GameMapLayer layer)
        {
            if (this.layerPanelCTM.Controls.Count < GameMap.MAX_LAYER_COUNT)
            {
                this.Add(layer);
                RaiseLayerAddedEvent(layer);
            }
            else
                ConsolePanel.Instance.
                    WriteLine("Impossible d'ajouter la nouvelle couche : Le nombre maximal de couches est atteint (" +
                    GameMap.MAX_LAYER_COUNT + ")", RowType.Error);
        }

        private void RaiseLayerAddedEvent(GameMapLayer layer)
        {
            this.MapLayerAdded?.Invoke(layer);
        }

        private void RaiseLayerSelectionChangedEvent(int index)
        {
            this.MapLayerSelectionChanged?.Invoke(index);
        }

        private void layerPanelCTM_ItemSelectionChanged(object sender, int index)
        {
            this.RaiseLayerSelectionChangedEvent(index);
        }

        private void layerPanelCTM_LayerTypeChanged(object sender)
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

        private void layerPanelCTM_LayerVisibleStateChanged(object sender)
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
    }
}
