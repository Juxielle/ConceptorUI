using System;

namespace ConceptorUI.Interfaces;

public interface IAppearance
{
    event EventHandler OnMouseDown;
    event EventHandler OnTextChange;
    event EventHandler OnCheck;
}