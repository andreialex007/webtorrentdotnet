﻿using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Common.Utils
{
    public static class ImageUtils
    {
        public static Image ResizeImage(Image imgToResize, Size size)
        {
            var sourceWidth = imgToResize.Width;
            var sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = (size.Width / (float)sourceWidth);
            nPercentH = (size.Height / (float)sourceHeight);

            nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var bitmap = new Bitmap(destWidth, destHeight);
            var graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            graphics.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            graphics.Dispose();

            return bitmap;
        }

        private static Image CropImage(Image img, Rectangle cropArea)
        {
            var bmpImage = new Bitmap(img);
            var bmpCrop = bmpImage.Clone(cropArea,
                bmpImage.PixelFormat);
            return bmpCrop;
        }

        public static byte[] ToByteArray(this Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        public static Bitmap ToBitmap(this byte[] array)
        {
            using (var image = Image.FromStream(new MemoryStream(array)))
            {
                return new Bitmap(image);
            }
        }

        public static byte[] ConvertToJpg(byte[] bytes)
        {
            using (var bitmap = bytes.ToBitmap())
            {
                return bitmap.ToByteArray(ImageFormat.Jpeg);
            }
        }

    }
}