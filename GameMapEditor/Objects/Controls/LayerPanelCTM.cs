using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor.Objects.Controls
{
    public delegate void PanelItemSelectionChangedEventArgs(object sender, int index);

    public class LayerPanelCTM : Panel
    {
        public event PanelItemSelectionChangedEventArgs ItemSelectionChanged;

        private int selectedIndex;

        public LayerPanelCTM() : base()
        {
        }

        public void Add(GameMapLayer layer)
        {
            LayerControl layerControl = new LayerControl(layer.Name, layer.Visible, layer.Type);
            layerControl.LayerClicked += LayerControl_Click;
            this.Controls.Add(layerControl);
            this.RefreshPositions();
            this.SelectedIndex = this.Controls.Count - 1;
        }

        public void Add(int index, GameMapLayer layer)
        {
            if (index >= 0)
            {
                LayerControl layerControl = new LayerControl(layer.Name, layer.Visible, layer.Type);
                layerControl.LayerClicked += LayerControl_Click;

                this.Controls.Add(layerControl);
                this.Controls.SetChildIndex(layerControl, index);
                this.RefreshPositions(index);
                this.SelectedIndex = index;
            }
            else
                throw new IndexOutOfRangeException("Insertion de la couche impossible, l'index spécifié est en dehors des limites du tableau.");
        }

        public void Remove(int index)
        {
            if (index >= 0 && index < this.Controls.Count)
            {
                (this.Controls[index] as LayerControl).LayerClicked -= LayerControl_Click;
                this.Controls.RemoveAt(index);
                this.RefreshPositions(index);

                if(this.Controls.Count > 0)
                    this.SelectedIndex = 0;
            }
            else
                throw new IndexOutOfRangeException("La couche spécifiée n'existe pas et ne peut donc pas être supprimée.");
        }

        public void Swap(int index1, int index2)
        {
            if (index1 == index2)
                return;

            if (index1 < this.Controls.Count && index2 < this.Controls.Count &&
                index1 >= 0 && index2 >= 0)
            {
                var layerControl1 = this.Controls[index1] as LayerControl;
                var layerControl2 = this.Controls[index2] as LayerControl;

                this.Controls.SetChildIndex(layerControl1, index2);
                this.Controls.SetChildIndex(layerControl2, index1);

                this.SelectedIndex = layerControl1.Selected ? index2 : index1;

                this.RefreshPositions(index1 < index2 ? index1 : index2);
            }
        }

        public LayerControl SelectedControl()
        {
            if(this.Controls.Count > 0)
            {
                return this.Controls[this.SelectedIndex] as LayerControl;
            }

            return null;
        }

        private void RefreshPositions(int index = 0)
        {
            for (int i = index; i < this.Controls.Count; i++)
            {
                LayerControl layerControl = this.Controls[i] as LayerControl;
                this.Controls[i].Location = new Point(0, i * (this.Controls[i].Height + 2) - this.VerticalScroll.Value);
                layerControl.Index = i.ToString();
            }
        }

        private void RefreshSelection(int index)
        {
            Console.WriteLine(index);
            for (int i = 0; i < this.Controls.Count; i++)
            {
                LayerControl layerControl = this.Controls[i] as LayerControl;
                layerControl.Selected = index == i;
            }
        }

        private void LayerControl_Click(object sender)
        {
            for(int i = 0; i < this.Controls.Count; i++)
            {
                LayerControl layerControl = this.Controls[i] as LayerControl;
                if(layerControl == sender)
                {
                    this.SelectedIndex = i;
                }
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            foreach (LayerControl layerControl in this.Controls)
            {
                layerControl.RefreshSize();
            }
        }

        [Instable]
        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            set
            {
                if (value >= 0 && value < this.Controls.Count)
                {
                    this.selectedIndex = value;
                    this.RefreshSelection(value);
                    this.ItemSelectionChanged?.Invoke(this, value);
                }
                else
                    throw new IndexOutOfRangeException("Impossible de changer la selection, l'index spécifié est en dehors des limites du tableau.");
            }
        }
    }
}
