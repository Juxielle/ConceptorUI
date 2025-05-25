using System.Collections.Generic;
using ConceptorUI.Enums;
using ConceptorUI.Models;
using UiDesigner.Models;

namespace ConceptorUI.Senders;

public class SelectComponentSender
{
    public string Id { get; init; }
    public SelectComponentActions SelectComponentAction { get; init; }
    public object PropertyGroups { get; init; }
    public ComponentList ComponentName { get; init; }
}