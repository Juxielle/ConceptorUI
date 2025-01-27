using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UiDesigner.Constants;
using UiDesigner.Interfaces;
using UiDesigner.Models;
using UiDesigner.Views.Widgets;
using TextAlignment = UiDesigner.Views.Widgets.TextAlignment;
using Widgets_TextAlignment = UiDesigner.Views.Widgets.TextAlignment;

namespace UiDesigner.Views.PropertyPanel;

public partial class DynamicPropertyPanel : IValue
{
    private GroupProperties _properties;
    private readonly Dictionary<string, List<Property>> _subGroups;
    private readonly List<int[]> _spaceIndex;
    private readonly List<int[]> _totalSpaceIndex;

    private readonly int _subGroupCount;
    private readonly int _propertiesWithoutSubGroupCount;
    private const int MaxColumns = 3;

    public event EventHandler? OnValueChangedEvent;
    private readonly object _valueChangedLock = new();

    public DynamicPropertyPanel()
    {
        InitializeComponent();

        _properties = new GroupProperties();
        _subGroups = new Dictionary<string, List<Property>>();
        _spaceIndex = new List<int[]>();
        _totalSpaceIndex = new List<int[]>();

        _subGroupCount = 0;
        _propertiesWithoutSubGroupCount = 0;
    }

    event EventHandler IValue.OnValueChanged
    {
        add
        {
            lock (_valueChangedLock)
            {
                OnValueChangedEvent += value;
            }
        }
        remove
        {
            lock (_valueChangedLock)
            {
                OnValueChangedEvent -= value;
            }
        }
    }

    public void BuildFields(object sender)
    {
        _properties = (sender as GroupProperties)!;
        Title.Text = _properties.Name.ToUpper();

        foreach (var property in _properties.Properties)
        {
            if (property.SubGroup != PropertyType.None.ToString()) continue;
            
            var field = new FrameworkElement();

            if (property.Type == PropertyType.String.ToString() || property.Type == PropertyType.Number.ToString())
            {
                field = new CustomTextBox();
            }
            else if (property.Type == PropertyType.List.ToString())
            {
                field = new CustomComboBox();
            }
            else if (property.Type == PropertyType.Color.ToString())
            {
                field = new ColorBox();
            }
            
            AddField(field, property.SpaceCount);
        }

        BuildSubGroup();
        foreach (var key in _subGroups.Keys)
        {
            var field = new FrameworkElement();
            
            if (key == PropertyType.TextAlignment.ToString())
            {
                field = new Widgets_TextAlignment();
            }
            else if (key == PropertyType.TextAlignment.ToString())
            {
                field = new TextFormat();
            }
            else if (key == PropertyType.Expanded.ToString())
            {
                field = new ExpandComponent();
            }
            
            AddField(field, _subGroups[key][0].SpaceCount);
        }
    }

    private void AddField(FrameworkElement field, int spaceCount)
    {
        var space = FindVoidSpace(spaceCount);
        var row = space[0];
        var column = space[1];
            
        if (row >= Content.RowDefinitions.Count)
            Content.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });

        Grid.SetRow(field, row);
        Grid.SetColumn(field, column);
        Grid.SetColumnSpan(field, spaceCount);

        Content.Children.Add(field);
    }

    private void BuildSubGroup()
    {
        foreach (var property in _properties.Properties.Where(property =>
                     property.SubGroup != PropertyType.None.ToString()))
        {
            if (_subGroups.ContainsKey(property.SubGroup))
                _subGroups[property.SubGroup].Add(property);
            else
            {
                _subGroups.Add(property.SubGroup, new List<Property> { property });
            }
        }
    }

    private int PropertiesWithoutSubGroup()
    {
        return _properties.Properties.Count(property => property.SubGroup == PropertyType.None.ToString());
    }

    private int[] FindVoidSpace(int columnSpan)
    {
        // var total = _subGroupCount + _propertiesWithoutSubGroupCount;
        // var idealRowCount = total / MaxColumns;
        // var diff = total - idealRowCount * MaxColumns;
        //
        // var rowCount = idealRowCount + (diff != 0 ? 1 : 0);
        // var lastRowFieldCount = diff;

        var i = 0;
        var j = 0;

        while (true)
        {
            var isExist = false;
            var remainingSpace = RemainingSpace(i);
            
            foreach (var space in _spaceIndex)
            {
                if ((space[0] == i && space[1] == j) || (space[0] == i && space[1] + space[2] > j) ||
                    (space[0] == i && space[1] + space[2] > j && columnSpan > remainingSpace) ||
                    (space[0] == i && !IsColumnExist(i, j) && columnSpan > remainingSpace))
                {
                    isExist = true;
                    break;
                }
            }

            if (isExist)
            {
                i = j == MaxColumns - 1 ? i + 1 : i;
                j = j == MaxColumns - 1 ? 0 : j + 1;
            }
            else break;
        }

        return new[] { i, j, columnSpan };
    }

    private int RemainingSpace(int row)
    {
        var columnCount = 0;
        
        foreach (var space in _spaceIndex)
        {
            if(space[0] != row) continue;
            columnCount += space[2];
        }
        
        return MaxColumns - columnCount;
    }

    private bool IsColumnExist(int row, int column)
    {
        var found = false;
        
        foreach (var space in _spaceIndex)
        {
            if ((space[0] != row || space[1] != column) &&
                (space[0] != row || space[1] + space[2] <= column)) continue;
            found = true;
            break;
        }
        
        return found;
    }
}