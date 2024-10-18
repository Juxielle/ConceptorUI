using System.Collections.Generic;
using System.Drawing;

namespace ConceptorUI.Classes;

public class CustomColor
{
    public string ColorType { get; set; } = Enums.ColorType.SolidColor.ToString();
    public string GradientColorType { get; set; } = Enums.GradientColorType.LinearGradient.ToString();
    public string GradientDirection { get; set; } = Enums.GradientDirection.LeftToRight.ToString();
    public List<string> Colors { get; set; } = [];
    public Point StartPoint { get; set; }
    public Point EndPoint { get; set; }
    public Point CenterPoint { get; set; }
    public Point BlurRadiusPoint { get; set; }
}