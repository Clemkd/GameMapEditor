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
    public delegate void ItemSelectionChangedEventArgs(object sender, int index);
    public delegate void ItemsSwappedEventArgs(object sender, int index1, int index2);
    public delegate void ItemRemovedEventArgs(object sender, int index);
    public delegate void ItemAddedEventArgs(object sender, int index, GameMapLayer layer);
    public delegate void ItemTypeChangedEventArgs(object sender, int index, LayerType type);
    public delegate void ItemVisibleStateChangedventArgs(object sender, int index, bool value);
    public delegate void ItemsCountChangedEventArgs(object sender, int count);

    public partial class LayerPanelControl : Panel
    {
        private const int ITEM_MARGIN_BOTTOM = 2;

        public event ItemSelectionChangedEventArgs ItemSelectionChanged;
        public event ItemsSwappedEventArgs ItemsSwapped;
        public event ItemRemovedEventArgs ItemRemoved;
        public event ItemAddedEventArgs ItemAdded;
        public event ItemTypeChangedEventArgs ItemTypeChanged;
        public event ItemVisibleStateChangedventArgs ItemVisibleStateChanged;
        public event ItemsCountChangedEventArgs ItemsCountChanged;

        private int selectedIndex;

        public LayerPanelControl() : base()
        {
            this.selectedIndex = 0;
            
            this.HorizontalScroll.Enabled = false;
            this.HorizontalScroll.Visible = false;
            this.AutoScroll = true;

            LayerControl.LayerClicked += LayerControl_Click;
            LayerControl.LayerVisibleStateChanged += Layer_VisibleStateChanged;
            LayerControl.LayerTypeChanged += Layer_TypeChanged;
            LayerControl.LayerRemoveButtonClicked += LayerControl_LayerRemoveButtonClicked;
            LayerControl.LayerDownButtonClicked += LayerControl_LayerDownButtonClicked;
            LayerControl.LayerUpButtonClicked += LayerControl_LayerUpButtonClicked;
        }

        public void Initialize()
        {
            this.Clear();
            this.Refresh();
        }

        private void LayerControl_Click(object sender)
        {
            LayerControl control = sender as LayerControl;
            this.SelectedIndex = this.Controls.IndexOf(control);
        }

        private void LayerControl_LayerUpButtonClicked(object sender)
        {
            LayerControl control = sender as LayerControl;
            int index = this.Controls.IndexOf(control);
            this.Swap(index, index - 1);
        }

        private void LayerControl_LayerDownButtonClicked(object sender)
        {
            LayerControl control = sender as LayerControl;
            int index = this.Controls.IndexOf(control);
            this.Swap(index, index + 1);
        }

        private void LayerControl_LayerRemoveButtonClicked(object sender)
        {
            LayerControl control = sender as LayerControl;
            int index = this.Controls.IndexOf(control);
            this.RemoveAt(index);
        }

        private void Layer_TypeChanged(object sender, LayerType type)
        {
            LayerControl control = sender as LayerControl;
            int index = this.Controls.IndexOf(control);
            this.ItemTypeChanged?.Invoke(this, index, type);
        }

        private void Layer_VisibleStateChanged(object sender, bool value)
        {
            LayerControl control = sender as LayerControl;
            int index = this.Controls.IndexOf(control);
            this.ItemVisibleStateChanged?.Invoke(this, index, value);
        }

        /// <summary>
        /// Vide le control et charge la liste de layers
        /// </summary>
        /// <param name="layers">La liste de layers à charger</param>
        public virtual void Load(List<GameMapLayer> layers)
        {
            this.Clear();
            foreach(GameMapLayer layer in layers)
            {
                this.Controls.Add(new LayerControl(layer.Name, layer.Visible, layer.Type));
            }
            this.ItemsCountChanged?.Invoke(this, this.ControlsCount);
            this.Refresh();
        }

        /// <summary>
        /// Vide la liste de layers
        /// </summary>
        public virtual void Clear()
        {
            this.Controls.Clear();
        }

        /// <summary>
        /// Ajoute l'élément sans le selectionner
        /// </summary>
        /// <param name="layer">La couche à ajouter</param>
        public virtual void Add(GameMapLayer layer)
        {
            LayerControl layerControl = new LayerControl(layer.Name, layer.Visible, layer.Type);
            this.Controls.Add(layerControl);
            this.ItemAdded?.Invoke(this, this.ControlsCount - 1, layer);
            this.ItemsCountChanged?.Invoke(this, this.ControlsCount);
        }

        /// <summary>
        /// Insert l'élément à la position spécifiée
        /// </summary>
        /// <param name="index">L'index de la position d'insertion</param>
        /// <param name="layer">La couche à insérer</param>
        public virtual void InsertAt(int index, GameMapLayer layer)
        {
            if (index >= 0 && layer != null)
            {
                this.Controls.Add(new LayerControl(layer.Name, layer.Visible, layer.Type));
                LayerControl layerControl = this.Controls[this.Controls.Count - 1] as LayerControl;
                this.Controls.SetChildIndex(layerControl, index);
                this.Refresh();
                this.SelectedIndex = index;
                this.ItemAdded?.Invoke(this, index, layer);
                this.ItemsCountChanged?.Invoke(this, this.ControlsCount);
            }
            else throw new IndexOutOfRangeException("Insertion de la couche impossible, l'index spécifié est en dehors des limites du tableau.");
        }

        /// <summary>
        /// Supprime l'élément à la position spécifiée
        /// </summary>
        /// <param name="index">L'index de l'élément à supprimer</param>
        public virtual void RemoveAt(int index)
        {
            if (index >= 0 && index < this.Controls.Count)
            {
                this.Controls.RemoveAt(index);
                this.Refresh();

                if (this.Controls.Count > 0)
                {
                    this.SelectedIndex = 0;
                }

                this.ItemRemoved?.Invoke(this, index);
                this.ItemsCountChanged?.Invoke(this, this.ControlsCount);
            }
            else throw new IndexOutOfRangeException("La couche spécifiée n'existe pas et ne peut donc pas être supprimée.");
        }

        /// <summary>
        /// Échange de position les deux éléments identifiés par leur index
        /// </summary>
        /// <param name="index1">Le premier élément</param>
        /// <param name="index2">Le second élément</param>
        /// <returns>True si l'échange s'est effectué, false dans le cas contraire</returns>
        public virtual bool Swap(int index1, int index2)
        {
            if (index1 == index2)
                return false;

            if (index1 < this.Controls.Count && index2 < this.Controls.Count &&
                index1 >= 0 && index2 >= 0)
            {
                var layerControl1 = this.Controls[index1] as LayerControl;
                var layerControl2 = this.Controls[index2] as LayerControl;

                this.Controls.SetChildIndex(layerControl1, index2);
                this.Controls.SetChildIndex(layerControl2, index1);

                this.SelectedIndex = layerControl1.Selected ? index2 : index1;

                this.Refresh();

                this.ItemsSwapped?.Invoke(this, index1, index2);
                return true;
            }
            return false;
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

        /// <summary>
        /// Met à jour l'affichage du control et de ses enfants
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();

            for (int i = 0; i < this.Controls.Count; i++)
            {
                LayerControl layerControl = this.Controls[i] as LayerControl;
                this.Controls[i].Location = new Point(0, i * (this.Controls[i].Height + ITEM_MARGIN_BOTTOM) - this.VerticalScroll.Value);
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
                else throw new IndexOutOfRangeException("Impossible de changer la selection, l'index spécifié est en dehors des limites du tableau.");
            }
        }

        /// <summary>
        /// Obtient la liste des noms des éléments enfants du control
        /// </summary>
        public List<string> ControlsName
        {
            get
            {
                List<string> names = new List<string>();

                foreach(LayerControl control in this.Controls)
                {
                    names.Add(control.Name);
                }

                return names;
            }
        }

        /// <summary>
        /// Obtient le nombre d'éléments enfants couramment contenu dans le control
        /// </summary>
        public int ControlsCount
        {
            get { return this.Controls.Count; }
        }

        /// <summary>
        /// Libère les ressources utilisées
        /// </summary>
        public new void Dispose()
        {
            LayerControl.LayerClicked -= LayerControl_Click;
            LayerControl.LayerVisibleStateChanged -= Layer_VisibleStateChanged;
            LayerControl.LayerTypeChanged -= Layer_TypeChanged;
            LayerControl.LayerRemoveButtonClicked -= LayerControl_LayerRemoveButtonClicked;
            LayerControl.LayerDownButtonClicked -= LayerControl_LayerDownButtonClicked;
            LayerControl.LayerUpButtonClicked -= LayerControl_LayerUpButtonClicked;
            this.Dispose(true);
        }
    }
}
