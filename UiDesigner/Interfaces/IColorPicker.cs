using System;

namespace UiDesigner.Interfaces;

public interface IColorPicker
{
    event EventHandler OnColorSelected;
    event EventHandler OnOpacityChanged;
}