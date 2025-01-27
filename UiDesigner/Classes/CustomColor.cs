using System.Collections.Generic;
using System.Windows;

namespace UiDesigner.Classes;

public class CustomColor
{
    public string ColorType { get; set; } = Enums.ColorType.SolidColor.ToString();
    public string GradientColorType { get; set; } = Enums.GradientColorType.LinearGradient.ToString();
    public string GradientDirection { get; set; } = Enums.GradientDirection.LeftToRight.ToString();
    public List<SingleColor> Colors { get; set; } = [];
    public Point StartPoint { get; set; } = new(0, 0);
    public Point EndPoint { get; set; } = new(0, 0);
    public Point CenterPoint { get; set; } = new(0, 0);
    public Point BlurRadiusPoint { get; set; } = new(0, 0);
}

public class SingleColor
{
    public int Id { get; set; }
    public string Color { get; set; }
    public double Offset { get; set; }
    public bool IsSelected { get; set; }
}