using System;

namespace ConceptorUI.Interfaces;

public interface IRefreshPropertyView
{
    event EventHandler OnSelected;
    event EventHandler OnRefreshPropertyPanel;
    event EventHandler OnRefreshStructuralView;
}