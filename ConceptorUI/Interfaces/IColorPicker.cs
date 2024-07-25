using System;

namespace ConceptorUI.Interfaces;

public interface IColorPicker
{
    event EventHandler OnColorSelected;
    event EventHandler OnOpacityChanged;
}