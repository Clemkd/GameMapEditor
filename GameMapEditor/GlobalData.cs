
using System.Drawing;

namespace GameMapEditor
{
    class GlobalData
    {
        private static Size tileSize = new Size(32, 32);
        private static Size mapSize = new Size(20, 14);

        public static Size TileSize
        {
            get { return tileSize; }
        }

        public static Size MapSize
        {
            get { return mapSize; }
        }
    }
}
