using System;

namespace UiDesigner.Interfaces;

public interface IRefreshPropertyView
{
    event EventHandler OnSelected;
    event EventHandler OnRefreshPropertyPanel;
    event EventHandler OnRefreshStructuralView;
}