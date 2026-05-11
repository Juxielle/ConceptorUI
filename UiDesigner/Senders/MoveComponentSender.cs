using ConceptorUI.Models;

namespace ConceptorUI.Senders;

public class MoveComponentSender
{
    public string Id { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
    public bool IsMove { get; init; }
    public object PropertyGroups { get; init; }
    public ComponentList ComponentName { get; init; }
}