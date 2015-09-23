using System.Drawing.Imaging;

namespace GameMapEditor.Objects
{
    public static class ImageAttrExtension
    {
        private const int X_ALPHA = 3;
        private const int Y_ALPHA = 3;

        private static float[][] matrixItems = { new float[] { 1, 0, 0, 0, 0 }, new float[] { 0, 1, 0, 0, 0 }, new float[] { 0, 0, 1, 0, 0 }, new float[] { 0, 0, 0, 0.5f, 0 }, new float[] { 0, 0, 0, 0, 1 } };
        
        public static ImageAttributes SetOpacity(this ImageAttributes imgAttr, float opacity)
        {
            matrixItems[X_ALPHA][Y_ALPHA] = opacity;
            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
            imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            return imgAttr;
        }
    }
}
