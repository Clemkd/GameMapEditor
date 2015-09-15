using ProtoBuf;
using System;
using System.Collections.Generic;
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
        private List<GameTile> tiles;

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
            this.tiles = new List<GameTile>();
            for (int index = 0; index < GlobalData.MapSize.Width * GlobalData.MapSize.Height; index++)
            {
                this.tiles.Add(new GameTile(index));
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
                this.tiles.ForEach(tile => tile.Draw(origin, e));
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
                if (x >= 0 && x < GlobalData.MapSize.Width && y >= 0 && y < GlobalData.MapSize.Height)
                {
                    int index = GameTile.EncodeFormattedIndex(new Point(x, y), GlobalData.MapSize.Width);
                    if (index < this.tiles.Count)
                    {
                        return this.tiles[index];
                    }
                }
                return null;
            }
            set
            {
                if (x >= 0 && x < GlobalData.MapSize.Width && y >= 0 && y < GlobalData.MapSize.Height)
                {
                    int index = GameTile.EncodeFormattedIndex(new Point(x, y), GlobalData.MapSize.Width);
                    if (index < this.tiles.Count)
                    {
                        this.tiles[index] = value;
                        RaiseLayerChangedEvent();
                    }
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
            for(int y = 0; y<14;y++)
            {
                for (int x = 0; x < 20; x++)
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

        public List<GameTile> Tiles
        {
            get { return this.tiles; }
        }
    }
}
