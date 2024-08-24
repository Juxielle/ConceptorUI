using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ConceptorUI.Constants;
using ConceptorUI.Interfaces;
using ConceptorUI.Models;
using ConceptorUI.Views.Widgets;

namespace ConceptorUI.Views.PropertyPanel;

public partial class DynamicPropertyPanel : IValue
{
    private GroupProperties _properties;
    private readonly Dictionary<string, List<int>> _subGroupIndex;
    private readonly List<int[]> _spaceIndex;
    private readonly List<int[]> _totalSpaceIndex;

    private int _subGroupCount;
    private int _propertiesWithoutSubGroupCount;
    private const int MaxColumns = 3;

    public event EventHandler? OnValueChangedEvent;
    private readonly object _valueChangedLock = new();

    public DynamicPropertyPanel()
    {
        InitializeComponent();

        _properties = new GroupProperties();
        _subGroupIndex = new Dictionary<string, List<int>>();
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

        var row = 0;
        var column = 0;

        foreach (var property in _properties.Properties)
        {
            if (property.SubGroup != PropertyType.None.ToString())
            {
                if (_subGroupIndex.ContainsKey(property.SubGroup))
                    _subGroupIndex[property.SubGroup].Add(_properties.Properties.IndexOf(property));
                else
                    _subGroupIndex.Add(property.SubGroup, new List<int> { _properties.Properties.IndexOf(property) });
                continue;
            }

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

            Grid.SetRow(field, row);
            Grid.SetColumn(field, column);
            Grid.SetColumnSpan(field, property.SpaceCount);

            Content.Children.Add(field);

            if (column == 2)
                Content.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
            row = column == 2 ? row + 1 : row;
            column = column == 2 ? 0 : column + property.SpaceCount;
            /*
             * Si SpaceCount
             */
        }
    }

    private int BuildSubGroup()
    {
        var count = 0;

        foreach (var property in _properties.Properties.Where(property =>
                     property.SubGroup != PropertyType.None.ToString()))
        {
            if (_subGroupIndex.ContainsKey(property.SubGroup))
                _subGroupIndex[property.SubGroup].Add(_properties.Properties.IndexOf(property));
            else
            {
                _subGroupIndex.Add(property.SubGroup, new List<int> { _properties.Properties.IndexOf(property) });
                count++;
            }
        }

        return count;
    }

    private int PropertiesWithoutSubGroup()
    {
        return _properties.Properties.Count(property => property.SubGroup == PropertyType.None.ToString());
    }

    private int[] FindVoidSpace(int columnSpan)
    {
        var total = _subGroupCount + _propertiesWithoutSubGroupCount;
        var idealRowCount = total / MaxColumns;
        var diff = total - idealRowCount * MaxColumns;
        
        var rowCount = idealRowCount + (diff != 0 ? 1 : 0);
        var lastRowFieldCount = diff;

        var i = 0;
        var j = 0;
        var isFound = false;

        while (!isFound)
        {
            var isExist = false;
            foreach (var space in _spaceIndex)
            {
                if (space[0] == i && space[1] == j)
                {
                    
                }
            }
        }

        return new[] { i, j, columnSpan };
    }
}