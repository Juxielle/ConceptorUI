using ConceptorUI.ViewModels.Components;
using UiDesigner.Models;

namespace ConceptorUI.Classes;

class UndoRedoAction
{
    public UndoRedo CurrentAction { get; init; }
    public UndoRedo PreviousAction { get; init; }
    public Component Instance { get; init; }
}