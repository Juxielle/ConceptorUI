using System;
using System.Collections.Generic;
using System.Text.Json;
using ConceptorUI.Classes;
using ConceptorUI.Models;
using ConceptorUi.ViewModels;
using ConceptorUI.ViewModels.Container;
using ConceptorUI.ViewModels.Icon;
using ConceptorUI.ViewModels.Image;
using ConceptorUI.ViewModels.ListView;
using ConceptorUI.ViewModels.Row;
using ConceptorUI.ViewModels.Stack;
using ConceptorUI.ViewModels.Text;
using ConceptorUI.ViewModels.Window;
using UiDesigner.Models;

namespace ConceptorUI.ViewModels.Components;

internal class ComponentHelper
{
    public static string? ProjectPath;
    public static string? ProjectName;
    public static string? ProjectTempId;
    public static string? FilePath;

    public static bool IsMultiSelectionEnable = false;
    
    public static readonly List<UndoRedoAction> UndoActions = [];
    public static readonly List<UndoRedoAction> RedoActions = [];

    private static List<string>? _ids;

    public static Component GetComponent(string name)
    {
        if (name == ComponentList.TextSingle.ToString())
            return new TextSingleModel();
        if (name == ComponentList.Text.ToString())
            return new TextModel();
        if (name == ComponentList.Image.ToString())
            return new ImageModel();
        if (name == ComponentList.Column.ToString())
            return new RowModel(isVertical: false);
        if (name == ComponentList.Row.ToString())
            return new RowModel();
        if (name == ComponentList.Container.ToString())
            return new ContainerModel();
        if (name == ComponentList.Icon.ToString())
            return new IconModel();
        if (name == ComponentList.Grid.ToString())
            return new GridModel();
        if (name == ComponentList.Window.ToString())
            return new WindowModel();
        if (name == ComponentList.Stack.ToString())
            return new StackModel();
        if (name == ComponentList.ListV.ToString())
            return new ListViewModel();
        return name == ComponentList.ListH.ToString() ? new ListViewModel(isVertical: false) : null!;
    }

    public static bool IsComponent(string name)
    {
        return name == ComponentList.TextSingle.ToString() ||
               name == ComponentList.Text.ToString() ||
               name == ComponentList.Image.ToString() ||
               name == ComponentList.Column.ToString() ||
               name == ComponentList.Row.ToString() ||
               name == ComponentList.Container.ToString() ||
               name == ComponentList.Icon.ToString() ||
               name == ComponentList.Grid.ToString() ||
               name == ComponentList.Window.ToString() ||
               name == ComponentList.Stack.ToString() ||
               name == ComponentList.ListV.ToString() ||
               name == ComponentList.ListH.ToString();
    }

    public static string GenerateId()
    {
        if (_ids == null!)
            _ids = new List<string>();

        var i = 0;
        var time = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
        var generatedId = $"{i}{time}";

        while (_ids.Count > 0)
        {
            var found = false;
            foreach (var id in _ids)
            {
                if (id != generatedId) continue;
                found = true;
                break;
            }

            if (!found) break;
            i++;
            generatedId = $"{i}{time}";
        }

        _ids.Add(generatedId);
        return generatedId;
    }

    public static void SaveId(string id)
    {
        _ids?.Add(id);
    }

    public static void DeleteId(string id)
    {
        _ids?.Remove(id);
    }

    public static void SaveUndoRedoAction(Component instance, GroupNames groupName, PropertyNames propertyName,
        string oldValue, string newValue)
    {
        UndoActions.Add(new UndoRedoAction
        {
            CurrentAction = new UndoRedo
            {
                GroupName = groupName,
                PropertyName = propertyName,
                Value = newValue
            },
            PreviousAction = new UndoRedo
            {
                GroupName = groupName,
                PropertyName = propertyName,
                Value = oldValue
            },
            Instance = instance
        });

        if (UndoActions.Count <= 50) return;
        UndoActions.RemoveAt(0);
    }

    public static CompSerializer? CopyComponent(Component component)
    {
        var json = JsonSerializer.Serialize(component);
        return JsonSerializer.Deserialize<CompSerializer>(json);
    }
}