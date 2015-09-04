using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMapEditor.Objects
{
    [Serializable]
    public class GameMapLayer : IDrawable
    {
        private string name;
        private LayerType type;
        private bool visible;
        private GameTile[,] tiles;

        public GameMapLayer()
        {
            this.tiles = new GameTile[GlobalData.MapSize.Width, GlobalData.MapSize.Height];
            this.InitializeComponent();
        }

        /// <summary>
        /// Initialise les données internes de l'objet
        /// </summary>
        private void InitializeComponent()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
                for (int y = 0; y < tiles.GetLength(1); y++)
                    this.tiles[x, y] = new GameTile(x, y);
        }

        public void Draw(Point origin, PaintEventArgs e)
        {
            foreach (GameTile tile in this.tiles)
                tile.Draw(origin, e);
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
                if (x >= 0 && x < this.tiles.GetLength(0) && y >= 0 && y < this.tiles.GetLength(1))
                    return this.tiles[x, y];
                return null;
            }
            set
            {
                if (x > 0 && x < this.tiles.GetLength(0) && y > 0 && y < this.tiles.GetLength(1))
                    this.tiles[x, y] = value;
            }
        }
        
        /// <summary>
        /// Obtient ou défini la nom du layer
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Obtient ou défini le type du layer
        /// </summary>
        public LayerType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// Obtient ou défini l'état d'affichage du layer
        /// </summary>
        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }
    }
}
