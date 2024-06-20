CosmosVectorGraphics is a library for Vector based image formats like SVG or GCODE

## Currently supported
### SVG version 2
- local mode
- global mode

# this library is currently a BETA


# Example code

```csharp
protected override void BeforeRun()
{
    Canvas canv = FullScreenCanvas.GetFullScreenCanvas(new Mode(1280,720,ColorDepth.ColorDepth32));
    SVG svg = new(SVGCode,ZoomFactor:1f);
    canv.Clear(Color.White);
    canv.DrawImage(svg, 10, 10);
    canv.Display();
}
```
