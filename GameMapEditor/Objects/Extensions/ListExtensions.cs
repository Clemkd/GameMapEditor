
using System.Collections.Generic;

namespace GameMapEditor.Objects
{
    public static class ListExtensions
    {
        public static List<GameMapTile> Clone(this List<GameMapTile> list)
        {
            List<GameMapTile> result = new List<GameMapTile>();
            foreach (GameMapTile tile in list)
            {
                result.Add(tile.Clone() as GameMapTile);
            }

            return result;
        }


        public static List<GameMapLayer> Clone(this List<GameMapLayer> list)
        {
            List<GameMapLayer> result = new List<GameMapLayer>();
            foreach (GameMapLayer layer in list)
            {
                result.Add(layer.Clone() as GameMapLayer);
            }

            return result;
        }
    }
}
