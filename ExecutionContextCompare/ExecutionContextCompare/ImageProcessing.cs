using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;

namespace MultithreadingImageProcessing
{
    public static class ImageProcessing
    {
        private static readonly byte[,] structuringElement = {
            {0, 1, 0},
            {1, 1, 1},
            {0, 1, 0}
        };

        public static Bitmap Dilation(Bitmap sourceBitmap, int dimension)
        {
            int sourceBitmapWidth = sourceBitmap.Width;
            int sourceBitmapHeight = sourceBitmap.Height;

            BitmapData sourceData =
                sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmapWidth, sourceBitmapHeight),
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int step = sourceData.Stride;
            int bytes = step * sourceData.Height;

            var processArray = new byte[bytes];
            var resultArray = new byte[bytes];

            Marshal.Copy(sourceData.Scan0, processArray, 0, bytes);
            sourceBitmap.UnlockBits(sourceData);

            ConvertToGrayScale(bytes, processArray);
            ExecuteDilatation(dimension, sourceBitmapHeight, sourceBitmapWidth, step, processArray, resultArray);

            return SaveResult(sourceBitmapWidth, sourceBitmapHeight, resultArray, bytes);
        }

        private static void ExecuteDilatation(int dimension, int sourceBitmapHeight, int sourceBitmapWidth, int step,
            byte[] processArray,
            byte[] resultArray)
        {
            int kernelDimension = dimension;

            int offset = (kernelDimension - 1) / 2;
            int currentOffset = 0;
            int currentByteOffset = 0;
            byte dilatationValue = 0;

            for (int y = offset; y < sourceBitmapHeight - offset; y++)
            {
                for (int x = offset; x < sourceBitmapWidth - offset; x++)
                {
                    dilatationValue = 0;
                    currentByteOffset = y * step + x * 4;

                    for (int ykernel = -offset; ykernel <= offset; ykernel++)
                    {
                        for (int xkernel = -offset; xkernel <= offset; xkernel++)
                        {
                            if (structuringElement[ykernel + offset, xkernel + offset] == 1)
                            {
                                currentOffset = currentByteOffset + ykernel * step + xkernel * 4;
                                dilatationValue = Math.Max(dilatationValue, processArray[currentOffset]);

                                SetDilatationValue(resultArray, currentByteOffset, dilatationValue);
                            }
                        }
                    }
                }
            }
        }

        private static void ConvertToGrayScale(int bytes, byte[] processBuffer)
        {
            for (int i = 0; i < bytes; i += 4)
            {
                float value = processBuffer[i] * .071f;
                value += processBuffer[i + 1] * .71f;
                value += processBuffer[i + 2] * .21f;

                processBuffer[i] = (byte) value;
                processBuffer[i + 1] = processBuffer[i];
                processBuffer[i + 2] = processBuffer[i];
                processBuffer[i + 3] = 255;
            }
        }

        private static void SetDilatationValue(byte[] resultArray, int byteOffset, byte dilatationValue)
        {
            resultArray[byteOffset] = dilatationValue;
            resultArray[byteOffset + 1] = dilatationValue;
            resultArray[byteOffset + 2] = dilatationValue;
            resultArray[byteOffset + 3] = 255;
        }

        private static Bitmap SaveResult(int sourceBitmapWidth, int sourceBitmapHeight, byte[] resultArray, int bytes)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmapWidth, sourceBitmapHeight);
            BitmapData resultBitmapData =
                resultBitmap.LockBits(new Rectangle(0, 0, sourceBitmapWidth, sourceBitmapHeight),
                    ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(resultArray, 0, resultBitmapData.Scan0, bytes);
            resultBitmap.UnlockBits(resultBitmapData);
            return resultBitmap;
        }
    }
}