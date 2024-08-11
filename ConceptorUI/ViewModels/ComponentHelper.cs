﻿using ConceptorUI.Models;

namespace ConceptorUi.ViewModels;

internal class ComponentHelper
{
    public static string? ProjectPath;
    
    public static Component GetComponent(string name)
    {
        if (name == ComponentList.TextSingle.ToString()) 
            return new TextSingleModel();
        if (name == ComponentList.Text.ToString()) 
            return new TextSingleModel();
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
            return new ListVModel();
        return name == ComponentList.ListH.ToString() ? new ListVModel(isVertical: false) : null!;
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
    
}