using UiDesigner.Models;

namespace ConceptorUI.Classes;

public class UndoRedoAction
{
    public UndoRedo CurrentAction { get; init; }
    public UndoRedo PreviousAction { get; init; }
}