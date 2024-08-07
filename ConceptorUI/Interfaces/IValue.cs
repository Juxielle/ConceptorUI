using System;

namespace ConceptorUI.Interfaces;

public interface IValue
{
    event EventHandler OnValueChanged;
}