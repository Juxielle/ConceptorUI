using ConceptorUI.Enums;
using UiDesigner.Models;

namespace ConceptorUI.Senders;

public class PropertySender
{
    public SenderAction SenderAction { get; init; }
    public GroupNames GroupName { get; init; }
    public PropertyNames propertyName { get; init; }
    public string Value { get; init; }
}