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
            this.HorizontalScroll.Enabled = false;
            this.HorizontalScroll.Visible = false;

            LayerControl.LayerClicked += LayerControl_Click;
        }

        /// <summary>
        /// Ajout d'une couche sans selection de cette dernière
        /// </summary>
        /// <param name="layer">La couche à ajouter</param>
        public void Add(GameMapLayer layer)
        {
            LayerControl layerControl = new LayerControl(layer.Name, layer.Visible, layer.Type);
            this.Controls.Add(layerControl);
        }

        /// <summary>
        /// Insert une couche à la position spécifiée
        /// </summary>
        /// <param name="index">L'index de la position d'insertion</param>
        /// <param name="layer">La couche à insérer</param>
        public void Insert(int index, GameMapLayer layer)
        {
            if (index >= 0)
            {
                this.Add(layer);
                LayerControl layerControl = this.Controls[this.Controls.Count - 1] as LayerControl;
                this.Controls.SetChildIndex(layerControl, index);
                this.Refresh();
                this.SelectedIndex = index;
            }
            else
                throw new IndexOutOfRangeException("Insertion de la couche impossible, l'index spécifié est en dehors des limites du tableau.");
        }

        /// <summary>
        /// Suppression de la couche à la position spécifiée
        /// </summary>
        /// <param name="index">L'index de l'élément à supprimer</param>
        public void Remove(int index)
        {
            if (index >= 0 && index < this.Controls.Count)
            {
                LayerControl layerControl = this.Controls[index] as LayerControl;

                this.Controls.RemoveAt(index);
                this.Refresh();

                if (this.Controls.Count > 0)
                {
                    this.SelectedIndex = 0;
                }
            }
            else
                throw new IndexOutOfRangeException("La couche spécifiée n'existe pas et ne peut donc pas être supprimée.");
        }

        /// <summary>
        /// Échange de position deux éléments identifiés par leur index
        /// </summary>
        /// <param name="index1">Le premier élément</param>
        /// <param name="index2">Le second élément</param>
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

                this.Refresh();
            }
        }

        /// <summary>
        /// Retourne l'élément selectionné de la liste
        /// </summary>
        /// <returns>Le control définissant la couche sélectionnée</returns>
        public LayerControl SelectedItem
        {
            get
            {
                if (this.Controls.Count > 0)
                {
                    return this.Controls[this.SelectedIndex] as LayerControl;
                }

                return null;
            }
        }

        public override void Refresh()
        {
            base.Refresh();

            for (int i = 0; i < this.Controls.Count; i++)
            {
                LayerControl layerControl = this.Controls[i] as LayerControl;
                this.Controls[i].Location = new Point(0, i * (this.Controls[i].Height + 2) - this.VerticalScroll.Value);
                layerControl.Index = i.ToString();
            }
        }

        /// <summary>
        /// Met à jour la liste selon l'élément selectionné
        /// </summary>
        /// <param name="index">L'index de l'élément selectionné</param>
        private void RefreshSelectionState(int index)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                LayerControl layerControl = this.Controls[i] as LayerControl;
                layerControl.Selected = (index == i);
            }
        }

        private void LayerControl_Click(object sender, EventArgs e)
        {
            LayerControl layerC = sender as LayerControl;
            this.SelectedIndex = this.Controls.IndexOf(layerC);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            foreach (LayerControl layerControl in this.Controls)
            {
                layerControl.Refresh();
            }
        }

        /// <summary>
        /// Obtient ou définit l'index de l'élement selectionné
        /// </summary>
        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            set
            {
                if (value >= 0 && value <= this.Controls.Count)
                {
                    this.selectedIndex = value;
                    this.RefreshSelectionState(value);
                    this.ItemSelectionChanged?.Invoke(this, value);
                }
                else
                    throw new IndexOutOfRangeException("Impossible de changer la selection, l'index spécifié est en dehors des limites du tableau.");
            }
        }

        /// <summary>
        /// Libère les ressources utilisées
        /// </summary>
        public new void Dispose()
        {
            LayerControl.LayerClicked -= LayerControl_Click;
            this.Dispose(true);
        }
    }
}
