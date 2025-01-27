using System;

namespace UiDesigner.Interfaces;

public interface ISpace
{
    event EventHandler OnAddSpace;
    event EventHandler OnAddSelectSpace;
}