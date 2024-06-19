using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HTMLRENDERER3.GrapeGL.Hrender3.DrawingUtils
{
    public static class BitmapDraws
    {

        static string ASC16Base64 = "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AAB4zMzM2MzGxsbMAAAAAAAA/sbGwMDAwMDAwAAAAAAAAAAA/mxsbGxsbGwAAAAAAAAA/sZgMBgwYMb+AAAAAAAAAAAAftjY2NjYcAAAAAAAAAAAZmZmZmZ8YGDAAAAAAAAAAHbcGBgYGBgYAAAAAAAAAH4YPGZmZjwYfgAAAAAAAAA4bMbG/sbGbDgAAAAAAAA4bMbGxmxsbGzuAAAAAAAAHjAYDD5mZmZmPAAAAAAAAAAAAH7b29t+AAAAAAAAAAAAAwZ+29vzfmDAAAAAAAAAHDBgYHxgYGAwHAAAAAAAAAB8xsbGxsbGxsYAAAAAAAAAAP4AAP4AAP4AAAAAAAAAAAAYGH4YGAAA/wAAAAAAAAAwGAwGDBgwAH4AAAAAAAAADBgwYDAYDAB+AAAAAAAADhsbGBgYGBgYGBgYGBgYGBgYGBgYGNjY2HAAAAAAAAAAABgYAH4AGBgAAAAAAAAAAAAAdtwAdtwAAAAAAAAAOGxsOAAAAAAAAAAAAAAAAAAAAAAAABgYAAAAAAAAAAAAAAAAAAAAGAAAAAAAAAAADwwMDAwM7GxsPBwAAAAAANhsbGxsbAAAAAAAAAAAAABw2DBgyPgAAAAAAAAAAAAAAAAAfHx8fHx8fAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==";
        static MemoryStream ASC16FontMS = new MemoryStream(Convert.FromBase64String(ASC16Base64));

        public static void DrawString(this Bitmap bmp, string s, int x, int y, Color col)
        {

            string[] lines = s.Split('\n');
            for (int l = 0; l < lines.Length; l++)
            {
                for (int c = 0; c < lines[l].Length; c++)
                {
                    int offset = (Encoding.ASCII.GetBytes(lines[l][c].ToString())[0] & 0xFF) * 16;
                    ASC16FontMS.Seek(offset, SeekOrigin.Begin);
                    byte[] fontbuf = new byte[16];
                    ASC16FontMS.Read(fontbuf, 0, fontbuf.Length);

                    for (int i = 0; i < 16; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if ((fontbuf[i] & (0x80 >> j)) != 0)
                            {
                                if (x + c * 8 > bmp.Width)
                                {

                                }
                                else
                                {
                                    DrawPixel(bmp, (int)((x + j) + (c * 8)), (int)(y + i + (l * 16)), col);
                                }
                            }
                        }
                    }
                }
            }

        }
        public static Color GetPointColor(this Bitmap image, int X, int Y)
        {
            return Color.FromArgb(image.RawData[X + Y * image.Width]);
        }
        public static void SetPixel(this Bitmap bmp,Color color, int X, int Y)
        {
            if (!(X > bmp.Width | X < 0 | Y > bmp.Height | Y < 0))
            {
                bmp.RawData[X + Y * bmp.Width] = color.ToArgb();
            }
        }
        public static Bitmap FromBMPRegion(Bitmap canvas, int X, int Y, ushort W, ushort H)
        {
            Bitmap bitmap = new Bitmap(W, H, canvas.Depth);
            for (int i = X; i < W + X; i++)
            {
                for (int j = Y; j < H + Y; j++)
                {
                    bitmap.SetPixel(canvas.GetPointColor(i, j), i - X, j - Y);
                }
            }

            return bitmap;
        }
        public static void DrawPixel(this Bitmap bmp, int X, int Y, Color color)
        {
            if (color.A <255)
            {
                color = AlphaBlend(color, Color.FromArgb(bmp.RawData[X + Y * bmp.Width]),color.A);
            }
            bmp.RawData[X + Y * bmp.Width] = color.ToArgb();
            
        }
        public static void DrawFilledRoundedRectangle(this Bitmap bmp, int left, int top, int width, int height, int r, Color color)
        {

            // Draw the straight lines of the rectangle
            for (int i = left + r; i < left + width - r; i++)
            {
                for (int j = top; j < top + height; j++)
                {
                    DrawPixel(bmp,i, j, color);
                }
            }
            for (int i = left; i < left + width; i++)
            {
                for (int j = top + r; j < top + height - r; j++)
                {
                    DrawPixel(bmp,i, j, color);
                }
            }

            // Drawidththe corners
            DrawCircle(bmp,left + r, top + r, r, color);  // Top-left corner
            DrawCircle(bmp,left + width - r, top + r, r, color);  // Top-right corner
            DrawCircle(bmp,left + r, top + height - r, r, color);  // Bottom-left corner
            DrawCircle(bmp,left + width - r, top + height - r, r, color);  // Bottom-right corner
        }

        public static void DrawRectangle(this Bitmap bmp, int left, int top, int width, int height, Color color)
        {

            bmp.DrawLine(color,left,top,width,top);
            bmp.DrawLine(color,width,top,width,height);
            bmp.DrawLine(color,left,height,width,height);
            bmp.DrawLine(color,left,top,left,height);
        }

        public static void DrawLine(this Bitmap bmp,Color color, int x1, int y1, int x2, int y2)
        {

            // Bresenham's line algorithm
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {

                DrawPixel(bmp, x1, y1, color);

                if (x1 == x2 && y1 == y2)
                    break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }
        /// <summary>
        /// cubic curve
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="P0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="col"></param>
        public static void DrawBezierCurve(this Bitmap bmp, Point P0,int x1,int y1,int x2,int y2,int x,int y,Color col)
        {
            DrawBezierCurvePoint(bmp,P0,new(x1,y1), new(x2, y2), new(x, y),col);
        }
        public static void DrawBezierCurvePoint(this Bitmap bmp, Point P0, Point P1, Point P2, Point P3, Color color)
        {
            for (double t = 0.0; t <= 1.0; t += 0.01)
            {
                double x = Math.Pow(1 - t, 3) * P0.X +
                            3 * Math.Pow(1 - t, 2) * t * P1.X +
                            3 * (1 - t) * Math.Pow(t, 2) * P2.X +
                            Math.Pow(t, 3) * P3.X;

                double y = Math.Pow(1 - t, 3) * P0.Y +
                            3 * Math.Pow(1 - t, 2) * t * P1.Y +
                            3 * (1 - t) * Math.Pow(t, 2) * P2.Y +
                            Math.Pow(t, 3) * P3.Y;

                DrawPixel(bmp,(int)x, (int)y, color);
            }
        }

        /// <summary>
        /// quadratic curve
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="P0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="col"></param>
        public static void DrawBezierCurve(this Bitmap bmp, Point P0, int x1, int y1, int x, int y, Color col)
        {
            DrawBezierCurvePoint(bmp, P0, new(x1, y1), new(x, y), col);
        }
        public static void DrawBezierCurvePoint(this Bitmap bmp, Point P0, Point P1, Point P2, Color color)
        {
            for (double t = 0.0; t <= 1.0; t += 0.01)
            {
                double x = Math.Pow(1 - t, 2) * P0.X +
                            2 * (1 - t) * t * P1.X +
                            Math.Pow(t, 2) * P2.X;

                double y = Math.Pow(1 - t, 2) * P0.Y +
                            2 * (1 - t) * t * P1.Y +
                            Math.Pow(t, 2) * P2.Y;

                DrawPixel(bmp,(int)x, (int)y, color);
            }
        }

        public static void FloodFill(this Bitmap bmp, int sx,int sy, Color fillColor)
        {
            FloodFill(bmp, new(sx, sy), fillColor);
        }

        public static void FloodFill(this Bitmap bmp,Point seed, Color fillColor)
        {
            Color startColor = GetPointColor(bmp,seed.X, seed.Y);
            if (startColor == fillColor) return;
            FillPixel(bmp,seed,fillColor,startColor);
        }
        private static void FillPixel(this Bitmap bmp, Point seed, Color fillColor,Color basecolor)
        {
            if (GetPointColor(bmp, seed.X, seed.Y) == basecolor)
            {
                SetPixel(bmp, fillColor, seed.X, seed.Y);
                FillPixel(bmp,new(seed.X + 1,seed.Y),fillColor,basecolor);
                FillPixel(bmp,new(seed.X - 1,seed.Y),fillColor,basecolor);
                FillPixel(bmp,new(seed.X,seed.Y + 1),fillColor,basecolor);
                FillPixel(bmp,new(seed.X,seed.Y - 1),fillColor,basecolor);
            }
        }

        public static void DrawImageAlpha(this Bitmap bmp, Bitmap image, int x, int y)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color color = Color.FromArgb(image.RawData[i + j * image.Width]);
                    if (color.A > 0)
                    {
                        if(color.A < 255)
                        {
                            color = AlphaBlend(color, Color.FromArgb(bmp.RawData[(x + i) + (y + j) * bmp.Width]), color.A);
                        }
                        bmp.RawData[(x + i) + (y + j) * bmp.Width] = color.ToArgb();
                    }
                        
                }
            }
        }
        public static void DrawImagePart(this Bitmap bmp, Bitmap image, int x, int y,int w,int h,int ox = 0,int oy = 0)
        {
            for (int i = ox; i < w; i++)
            {
                for (int j = oy; j < h; j++)
                {
                    Color color = Color.FromArgb(image.RawData[i + j * image.Width]);
                    if (color.A > 0)
                    {
                        if (color.A < 255)
                        {
                            color = AlphaBlend(color, Color.FromArgb(bmp.RawData[(x + i) + (y + j) * bmp.Width]), color.A);
                        }
                        bmp.RawData[(x + i) + (y + j) * bmp.Width] = color.ToArgb();
                    }

                }
            }
        }

        public static void DrawFilledEllipse(this Bitmap bmp, int centerX, int centerY, int radiusX, int radiusY, Color color)
        {
            for (int y = -radiusY; y <= radiusY; y++)
            {
                for (int x = -radiusX; x <= radiusX; x++)
                {
                    if (x * x * radiusY * radiusY + y * y * radiusX * radiusX <= radiusX * radiusX * radiusY * radiusY)
                    {
                        DrawPixel(bmp,centerX + x, centerY + y, color);
                    }
                }
            }
        }


        public static void DrawCircle(this Bitmap bmp, int cx, int cy, int r, Color color)
        {
            // Midpoint circle algorithm
            int x = r;
            int y = 0;
            int err = 0;

            while (x >= y)
            {
                // Draw horizontal lines from left to right
                for (int i = cx - x; i <= cx + x; i++)
                {
                    DrawPixel(bmp,i, cy + y, color);
                    DrawPixel(bmp,i, cy - y, color);
                }
                for (int i = cx - y; i <= cx + y; i++)
                {
                    DrawPixel(bmp,i, cy + x, color);
                    DrawPixel(bmp,i, cy - x, color);
                }

                if (err <= 0)
                {
                    y += 1;
                    err += 2 * y + 1;
                }
                if (err > 0)
                {
                    x -= 1;
                    err -= 2 * x + 1;
                }
            }
        }
        public static void Clear(this Bitmap bmp, Color col)
        {

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    DrawPixel(bmp,x,y,col);
                }
            }

        }

        public static void DrawLine2(this Bitmap bmp, Color color, int x1, int y1, int x2, int y2)
        {
            TrimLine(bmp,ref x1, ref y1, ref x2, ref y2);
            int num = x2 - x1;
            int num2 = y2 - y1;
            if (num2 == 0)
            {
                DrawHorizontalLine(bmp, color, num, x1, y1);
            }
            else if (num == 0)
            {
                DrawVerticalLine(bmp, color, num2, x1, y1);
            }
            else
            {
                DrawDiagonalLine(bmp,color, num, num2, x1, y1);
            }
        }

        internal static void DrawHorizontalLine(this Bitmap bmp,Color color, int dx, int x1, int y1)
        {
            for (int i = 0; i < dx; i++)
            {
                SetPixel(bmp, color, x1 + i, y1);
            }
        }
        internal static void DrawVerticalLine(this Bitmap bmp, Color color, int dy, int x1, int y1)
        {
            for (int i = 0; i < dy; i++)
            {
                SetPixel(bmp, color, x1, y1 + i);
            }
        }

        static void TrimLine(this Bitmap Mode,ref int x1, ref int y1, ref int x2, ref int y2)
        {
            if (x1 == x2)
            {
                x1 = Math.Min((int)(Mode.Width - 1), Math.Max(0, x1));
                x2 = x1;
                y1 = Math.Min((int)(Mode.Height - 1), Math.Max(0, y1));
                y2 = Math.Min((int)(Mode.Height - 1), Math.Max(0, y2));
                return;
            }

            float num = x1;
            float num2 = y1;
            float num3 = x2;
            float num4 = y2;
            float num5 = (num4 - num2) / (num3 - num);
            float num6 = num2 - num5 * num;
            if (num < 0f)
            {
                num = 0f;
                num2 = num6;
            }
            else if (num >= (float)Mode.Width)
            {
                num = Mode.Width - 1;
                num2 = (float)(Mode.Width - 1) * num5 + num6;
            }

            if (num3 < 0f)
            {
                num3 = 0f;
                num4 = num6;
            }
            else if (num3 >= (float)Mode.Width)
            {
                num3 = Mode.Width - 1;
                num4 = (float)(Mode.Width - 1) * num5 + num6;
            }

            if (num2 < 0f)
            {
                num = (0f - num6) / num5;
                num2 = 0f;
            }
            else if (num2 >= (float)Mode.Height)
            {
                num = ((float)(Mode.Height - 1) - num6) / num5;
                num2 = Mode.Height - 1;
            }

            if (num4 < 0f)
            {
                num3 = (0f - num6) / num5;
                num4 = 0f;
            }
            else if (num4 >= (float)Mode.Height)
            {
                num3 = ((float)(Mode.Height - 1) - num6) / num5;
                num4 = Mode.Height - 1;
            }

            if (num < 0f || num >= (float)Mode.Width || num2 < 0f || num2 >= (float)Mode.Height)
            {
                num = 0f;
                num3 = 0f;
                num2 = 0f;
                num4 = 0f;
            }

            if (num3 < 0f || num3 >= (float)Mode.Width || num4 < 0f || num4 >= (float)Mode.Height)
            {
                num = 0f;
                num3 = 0f;
                num2 = 0f;
                num4 = 0f;
            }

            x1 = (int)num;
            y1 = (int)num2;
            x2 = (int)num3;
            y2 = (int)num4;
        }

        internal static void DrawDiagonalLine(this Bitmap bmp,Color color, int dx, int dy, int x1, int y1)
        {
            int num = Math.Abs(dx);
            int num2 = Math.Abs(dy);
            int num3 = Math.Sign(dx);
            int num4 = Math.Sign(dy);
            int num5 = num2 >> 1;
            int num6 = num >> 1;
            int num7 = x1;
            int num8 = y1;
            if (num >= num2)
            {
                for (int i = 0; i < num; i++)
                {
                    num6 += num2;
                    if (num6 >= num)
                    {
                        num6 -= num;
                        num8 += num4;
                    }

                    num7 += num3;
                    SetPixel(bmp,color, num7, num8);
                }

                return;
            }

            for (int i = 0; i < num2; i++)
            {
                num5 += num;
                if (num5 >= num2)
                {
                    num5 -= num2;
                    num7 += num3;
                }

                num8 += num4;
                SetPixel(bmp,color, num7, num8);
            }
        }

        public static Color AlphaBlend(Color to, Color from, byte alpha)
        {
            byte R = (byte)(((to.R * alpha) + (from.R * (255 - alpha))) >> 8);
            byte G = (byte)(((to.G * alpha) + (from.G * (255 - alpha))) >> 8);
            byte B = (byte)(((to.B * alpha) + (from.B * (255 - alpha))) >> 8);
            return Color.FromArgb(R, G, B);
        }

        public static void resize(this Bitmap bmp,uint NewW, uint NewH)
        {
            bmp.RawData = ScaleImage(bmp, (int)NewW, (int)NewH);
        }

        private static int[] ScaleImage(Image image, int newWidth, int newHeight)
        {
            int[] rawData = image.RawData;
            int width = (int)image.Width;
            int height = (int)image.Height;
            int[] array = new int[newWidth * newHeight];
            int num = (width << 16) / newWidth + 1;
            int num2 = (height << 16) / newHeight + 1;
            for (int i = 0; i < newHeight; i++)
            {
                for (int j = 0; j < newWidth; j++)
                {
                    int num3 = j * num >> 16;
                    int num4 = i * num2 >> 16;
                    array[i * newWidth + j] = rawData[num4 * width + num3];
                }
            }

            return array;
        }

        public static void ChangeTransparency(this Bitmap bmp,Color changeto)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    if (GetPointColor(bmp,i,j) == Color.Transparent)
                    {
                        DrawPixel(bmp,i,j,changeto);
                    }
                }
            }
        }

    }
}
