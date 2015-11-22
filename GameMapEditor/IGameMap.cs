using GameMapEditor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMapEditor
{
    public interface IGameMap
    {
        MemoryStream CopyToMemoryStream();

        void InitializeComponents();

        void Draw(GameVector2 origin, PaintEventArgs e);

        // TODO : Fonctions de LayerCollection
        IGameMapLayer GetLayerAt(int index);
        bool AddLayer(IGameMapLayer layer);
        bool InsertLayerAt(int index, IGameMapLayer layer);
        bool RemoveLayerAt(int index);
        bool ReplaceLayerAt(int index, IGameMapLayer layer);
        bool SwapLayers(int index1, int index2);

        // TODO : Methode de IGameMapLayer
        void Fill(int layerIndex, TextureInfo texture);

        // TODO : Methode de IGameMapLayer
        void SetTiles(int layerIndex, GameVector2 position, TextureInfo texture, bool raise = false);

        void Save();

        Task<IGameMap> Load(string filename);
    }
}
