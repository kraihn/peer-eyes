/**
 * This file is part of Peer Eyes.
 * 
 * Peer Eyes is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Peer Eyes is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Peer Eyes.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PeerEyesLibrary.Images
{
    public static class Screenshot
    {
        public static Bitmap TakeScreenshot()
        {
            Point startLocation = new Point(0, 0);
            Point endLocation = new Point(0, 0);

            System.Windows.Forms.Screen[] screens = System.Windows.Forms.Screen.AllScreens;

            foreach (System.Windows.Forms.Screen tempScreen in screens)
            {
                if (tempScreen.Bounds.X < startLocation.X)
                    startLocation.X = tempScreen.Bounds.X;

                if (tempScreen.Bounds.Y < startLocation.Y)
                    startLocation.Y = tempScreen.Bounds.Y;

                if ((tempScreen.Bounds.X + tempScreen.Bounds.Width) > endLocation.X)
                    endLocation.X = (tempScreen.Bounds.X + tempScreen.Bounds.Width);

                if ((tempScreen.Bounds.Y + tempScreen.Bounds.Height) > endLocation.Y)
                    endLocation.Y = (tempScreen.Bounds.Y + tempScreen.Bounds.Height);
            }

            Bitmap bitmap = new Bitmap(endLocation.X + Math.Abs(startLocation.X), endLocation.Y + Math.Abs(startLocation.Y));
            Graphics g = Graphics.FromImage(bitmap);

            g.CopyFromScreen(startLocation.X, startLocation.Y, 0, 0, new System.Drawing.Size(endLocation.X + Math.Abs(startLocation.X), endLocation.Y + Math.Abs(startLocation.Y)));

            return bitmap;
        }
        public static byte[] GetScreenshot()
        {
            MemoryStream ms = new MemoryStream();
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameter parm = new EncoderParameter(encoder, 25L);
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = parm;

            Bitmap bitmap = TakeScreenshot();
            bitmap = Resize(bitmap, 640, 480);
            try
            {
                //string name = "";
                //DateTime t = DateTime.Now;
                //name = "C:\\peereyes " + t.Hour + " " + t.Minute + " " + t.Second + ".jpg";

                bitmap.Save(ms, GetEncoderInfo("image/jpeg"), parameters);
            }
            catch (Exception ex)
            {
            }

            return ms.ToArray();
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo encoder in encoders)
            {
                if (encoder.MimeType == mimeType)
                {
                    return encoder;
                }
            }
            return null;
        }

        public static Bitmap Resize(Bitmap bitmap, double scale = -0.50)
        {
            Bitmap result = new Bitmap((int)(bitmap.Width + bitmap.Width * scale), (int)(bitmap.Height + bitmap.Height * scale));

            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawImage(bitmap, 0, 0, result.Width, result.Height);
            }

            return result;
        }

        public static Bitmap Resize(Bitmap bitmap, int width, int height)
        {
            int finalWidth, finalHeight;

            if (bitmap.Width > bitmap.Height)
            {
                double ratio = (double)bitmap.Width / (double)bitmap.Height;
                finalWidth = width;
                finalHeight = (int)(width / ratio);
            }
            else
            {
                double ratio = (double)bitmap.Height / (double)bitmap.Width;
                finalHeight = height;
                finalWidth = (int)(height / ratio);
            }

            Bitmap result = new Bitmap(finalWidth, finalHeight);

            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawImage(bitmap, 0, 0, result.Width, result.Height);
            }

            return result;
        }

    }
}
