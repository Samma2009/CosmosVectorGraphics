using Cosmos.Debug.Kernel;
using Cosmos.System.Graphics;
using CSVG.SVG2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Debugger = Cosmos.Debug.Kernel.Debugger;
using Sys = Cosmos.System;

namespace cosmosVectorGraphics
{
    public class Kernel : Sys.Kernel
    {
        Canvas canv;
        SVG svg;

        string SVGCode = "<svg width=\"100\" height=\"100\" xmlns=\"http://www.w3.org/2000/svg\">\r\n  <path fill=\"Blue\" d=\"m45,0 l23,30 c-18,20,-13,20,2,45 l-3,3 c-17,-8,-27,2,-14,19 l-3,3 c-20,-25,-20,-40,7,-32 l-22,-28 q25,-15,3,-40 z\"></path>\r\n</svg>";

        protected override void BeforeRun()
        {
            canv = FullScreenCanvas.GetFullScreenCanvas(new Mode(1280,720,ColorDepth.ColorDepth32));
            svg = new(SVGCode,2f);
        }

        protected override void Run()
        {
            canv.Clear(Color.White);
            canv.DrawImage(svg, 10, 10);
            canv.Display();
        }
    }
}
