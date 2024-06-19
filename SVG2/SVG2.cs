using Cosmos.Core.Memory;
using Cosmos.System.Graphics;
using cosmosVectorGraphics;
using HtmlAgilityPack;
using HTMLRENDERER3.GrapeGL.Hrender3.DrawingUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSVG.SVG2
{
    public class SVG : Image
    {
        public string SRC = "";
        internal Bitmap Canvas = new Bitmap(10,10,ColorDepth.ColorDepth32);
        HtmlDocument dom;
        public SVG(string SVG2, float ZoomFactor = 1) : base(10, 10, ColorDepth.ColorDepth32)
        {
            SRC = "<body>"+SVG2+"</body>";
            ReRender(ZoomFactor);
        }

        public void ReRender(float ZoomFactor = 1)
        {
            dom = new HtmlDocument();
            dom.LoadHtml(SRC);
            foreach (var nodes in dom.DocumentNode.ChildNodes)
            {
                RecursiveRender(nodes, ZoomFactor);
            }
            Width = Canvas.Width;
            Height = Canvas.Height;
            RawData = Canvas.RawData;
        }
        private void RecursiveRender(HtmlNode node,float ZoomFactor)
        {

            Dictionary<string, string> attribs = new Dictionary<string, string>();

            foreach (var attribute in node.Attributes)
            {
                attribs.Add(attribute.Name, attribute.Value);
            }

            switch (node.Name)
            {
                case "svg":
                    Canvas = new Bitmap((uint)(int.Parse(attribs["width"]) * ZoomFactor) + 5, (uint)(int.Parse(attribs["height"]) * ZoomFactor) + 5, ColorDepth.ColorDepth32);
                    break;
                case "circle":
                    Canvas.DrawCircle(int.Parse(attribs["cx"]), int.Parse(attribs["cy"]), int.Parse(attribs["r"]), ProcessColor(attribs["fill"]));
                    break;
                case "ellipse":
                    Canvas.DrawFilledEllipse(int.Parse(attribs["cx"]), int.Parse(attribs["cy"]), int.Parse(attribs["rx"]), int.Parse(attribs["ry"]), ProcessColor(attribs["fill"]));
                    break;
                case "path":
                    Point Prev = new Point();
                    Point First = new Point();
                    string[] path = attribs["d"].Split(" ",StringSplitOptions.RemoveEmptyEntries);
                    bool first = true;
                    foreach (var item in path)
                    {
                        string[] ParamsRaw = item.Split(",", StringSplitOptions.RemoveEmptyEntries);
                        ParamsRaw[0] = ParamsRaw[0].Remove(0,1);
                        int[] Params = new int[ParamsRaw.Length];
                        for (int i = 0; i < ParamsRaw.Length; i++)
                        {
                            Params[i] = (int)(int.Parse(ParamsRaw[i]) * ZoomFactor);
                        }
                        char type = item[0];
                        if (item == "Z")
                        {
                            type = 'Z';
                        }
                        switch (type)
                        {
                            //absolutes
                            case 'M':
                                Prev.X = Params[0];
                                Prev.Y = Params[1];
                                break;
                            case 'L':
                                Canvas.DrawLine2(Color.Transparent, Prev.X, Prev.Y, Params[0], Params[1]);
                                Prev.X = Params[0];
                                Prev.Y = Params[1];
                                break;
                            case 'H':
                                Canvas.DrawLine2(Color.Transparent, Prev.X, Prev.Y, Params[0], Prev.Y);
                                Prev.X = Params[0];
                                break;
                            case 'V':
                                Canvas.DrawLine2(Color.Transparent, Prev.X, Prev.Y, Prev.X, Params[0]);
                                Prev.Y = Params[0];
                                break;
                            case 'C':
                                Canvas.DrawBezierCurve(Prev, Params[0], Params[1], Params[2], Params[3], Params[4], Params[5], Color.Transparent);
                                Prev.X = Params[4];
                                Prev.Y = Params[5];
                                break;
                            case 'Q':
                                Canvas.DrawBezierCurve(Prev, Params[0], Params[1], Params[2], Params[3], Color.Transparent);
                                Prev.X = Params[2];
                                Prev.Y = Params[3];
                                break;
                            case 'Z':
                                Canvas.DrawLine(Color.Transparent, Prev.X, Prev.Y, First.X, First.Y);
                                break;


                            //relatives
                            case 'm':
                                Prev.X += Params[0];
                                Prev.Y += Params[1];
                                break;
                            case 'l':
                                Canvas.DrawLine2(ProcessColor(attribs["fill"]), Prev.X, Prev.Y, Prev.X+Params[0], Prev.Y+Params[1]);
                                Prev.X += Params[0];
                                Prev.Y += Params[1];
                                break;
                            case 'c':
                                Canvas.DrawBezierCurve(Prev, Prev.X+Params[0], Prev.Y + Params[1], Prev.X + Params[2], Prev.Y + Params[3], Prev.X + Params[4], Prev.Y + Params[5], ProcessColor(attribs["fill"]));
                                Prev.X += Params[4];
                                Prev.Y += Params[5];
                                break;
                            case 'q':
                                Canvas.DrawBezierCurve(Prev, Prev.X + Params[0], Prev.Y + Params[1], Prev.X + Params[2], Prev.Y + Params[3], ProcessColor(attribs["fill"]));
                                Prev.X += Params[2];
                                Prev.Y += Params[3];
                                break;
                            case 'h':
                                Canvas.DrawLine2(ProcessColor(attribs["fill"]), Prev.X, Prev.Y, Prev.X+Params[0], Prev.Y);
                                Prev.X += Params[0];
                                break;
                            case 'v':
                                Canvas.DrawLine2(ProcessColor(attribs["fill"]), Prev.X, Prev.Y, Prev.X, Prev.Y+Params[0]);
                                Prev.Y += Params[0];
                                break;
                            case 'z':
                                Canvas.DrawLine(ProcessColor(attribs["fill"]), Prev.X, Prev.Y, First.X, First.Y);
                                break;
                            default:
                                break;
                        }

                        if (first)
                        {
                            first = false;
                            First.X = Prev.X;
                            First.Y = Prev.Y;
                        }
                    }
                    break;
            }

            foreach (var nodes in node.ChildNodes)
            {
                RecursiveRender(nodes, ZoomFactor);
            }
        }

        Color ProcessColor(string col)
        {
            try
            {
                return Color.FromName(col);
            }
            catch
            {
                try
                {
                    return HexToColor(col);
                }
                catch
                {
                    return Color.Black;
                }
                
            }
        }
        Color HexToColor(string hexColor)
        {
            if (hexColor.StartsWith("#"))
                hexColor = hexColor.Substring(1);

            if (hexColor.Length == 6)
                hexColor = "FF" + hexColor; // Add alpha if not present

            if (hexColor.Length != 8)
                throw new ArgumentException("Invalid hex color. Hex color must be in ARGB format (8 characters).");

            int a = Convert.ToInt32(hexColor.Substring(0, 2), 16);
            int r = Convert.ToInt32(hexColor.Substring(2, 2), 16);
            int g = Convert.ToInt32(hexColor.Substring(4, 2), 16);
            int b = Convert.ToInt32(hexColor.Substring(6, 2), 16);

            return Color.FromArgb(a, r, g, b);
        }

    }
}
