using System;
using UiDesigner.Interfaces;
using UiDesigner.Models;

namespace UiDesigner.Views.Component
{
    public partial class ParentProperties : IValue
    {
        private static ParentProperties? _obj;
        private GroupProperties _properties;
        
        public event EventHandler? OnValueChangedEvent;
        private readonly object _valueChangedLock = new();

        public ParentProperties()
        {
            InitializeComponent();
            
            _obj = this;
            _properties = new GroupProperties();
        }

        public static ParentProperties Instance => _obj == null! ? new ParentProperties() : _obj;
        
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

        public void FeedProps(object value)
        {
            _properties = (value as GroupProperties)!;
            
            foreach (var prop in _properties.Properties)
            {
                _properties = (value as GroupProperties)!;

                if (prop.Visibility != VisibilityValue.Visible.ToString()) continue;
                if (prop.Name == GroupNames.Alignment.ToString())
                {

                }
                else if (prop.Name == GroupNames.Transform.ToString())
                {

                }
            }
        }
    }
}
