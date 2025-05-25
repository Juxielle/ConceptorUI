using ConceptorUI.Enums;
using ConceptorUI.Models;
using UiDesigner.Models;

namespace ConceptorUI.Senders;

public class PropertySender
{
    public SenderAction SenderAction { get; init; }
    public GroupNames GroupName { get; init; }
    public PropertyNames propertyName { get; init; }
    public string Value { get; init; }
}