using UiDesigner.Models;

namespace ConceptorUI.Classes;

public class UndoRedo
{
    public GroupNames GroupName { get; init; }
    public PropertyNames PropertyName { get; init; }
    public string Value { get; init; }
}