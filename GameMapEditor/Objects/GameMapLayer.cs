using ProtoBuf;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GameMapEditor
{
    public delegate void LayerChangeEventArgs(object sender);

    //[Serializable]
    [ProtoContract]
    public class GameMapLayer : IDrawable
    {
        //[field: NonSerialized]
        public event LayerChangeEventArgs LayerChanged;

        [ProtoMember(1)]
        private string name;
        [ProtoMember(2)]
        private LayerType type;
        [ProtoMember(3)]
        private bool visible;
        [ProtoMember(4)]
        private GameTile[] tiles;

        // Protobuf constructor
        private GameMapLayer()
        {
        }

        public GameMapLayer(string name)
        {
            this.name = name;
            this.InitializeComponent();
        }

        /// <summary>
        /// Initialise les données internes de l'objet
        /// </summary>
        private void InitializeComponent()
        {
            this.tiles = new GameTile[(GlobalData.MapSize.Width + 1) * (GlobalData.MapSize.Height + 1)];
            for (int index = 0; index < tiles.GetLength(0); index++)
            {
                this.tiles[index] = new GameTile(index);
            }
        }

        /// <summary>
        /// Dessine le layer sur le controle de dessin à l'origine spécifiée
        /// </summary>
        /// <param name="origin">L'origine de dessin, définie par le point supérieur gauche</param>
        /// <param name="e">L'évènement du control de dessin</param>
        public void Draw(Point origin, PaintEventArgs e)
        {
            if (this.visible)
            {
                foreach (GameTile tile in this.tiles)
                    tile?.Draw(origin, e);
            }
        }

        /// <summary>
        /// Retourne la reférence du tile à la position spécifiée et retourne 'null' si inexistant
        /// </summary>
        /// <param name="x">La position horizontale X du tile</param>
        /// <param name="y">La position verticale Y du tile</param>
        /// <returns>La reférence vers le tile, ou null</returns>
        public GameTile this[int x, int y]
        {
            get
            {
                int index = y * (GlobalData.MapSize.Width + 1) + x;
                if (index < tiles.GetLength(0))
                {
                    return this.tiles[index];
                }
                return null;
            }
            set
            {
                int index = y * (GlobalData.MapSize.Width + 1) + x;
                if (index < tiles.GetLength(0))
                {
                    this.tiles[index] = value;
                    RaiseLayerChangedEvent();
                }
            }
        }

        private void RaiseLayerChangedEvent()
        {
            this.LayerChanged?.Invoke(this);
        }

        // TODO : Debug Only
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.name + "\n");
            for(int y = 0; y<15;y++)
            {
                for (int x = 0; x < 21; x++)
                {
                    builder.Append(this[x, y].ToString());
                }
                builder.Append("\n");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Obtient ou définit la nom du layer
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; RaiseLayerChangedEvent(); }
        }

        /// <summary>
        /// Obtient ou définit le type du layer
        /// </summary>
        public LayerType Type
        {
            get { return this.type; }
            set { this.type = value; this.RaiseLayerChangedEvent(); }
        }

        /// <summary>
        /// Obtient ou définit l'état d'affichage du layer
        /// </summary>
        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; this.RaiseLayerChangedEvent(); }
        }
    }
}
