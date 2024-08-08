using System;

namespace ConceptorUI.Interfaces;

public interface ISpace
{
    event EventHandler OnAddSpace;
    event EventHandler OnAddSelectSpace;
}