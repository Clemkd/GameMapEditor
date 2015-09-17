
using System.Collections.Generic;

namespace GameMapEditor.Objects
{
    public static class Extensions
    {
        public static List<GameTile> Clone(this List<GameTile> list)
        {
            List<GameTile> result = new List<GameTile>();
            foreach (GameTile tile in list)
            {
                result.Add(tile.Clone());
            }

            return result;
        }


        public static List<GameMapLayer> Clone(this List<GameMapLayer> list)
        {
            List<GameMapLayer> result = new List<GameMapLayer>();
            foreach (GameMapLayer layer in list)
            {
                result.Add(layer.Clone());
            }

            return result;
        }
    }
}
