using System;

namespace UiDesigner.Interfaces;

public interface IValue
{
    event EventHandler OnValueChanged;
}