using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ConceptorUI.Models
{
    class TextUI
    {
        public List<TextElement> Children;
        private int selectedIndex;
        public TextBlock component;

        public TextUI()
        {
            selectedIndex = 0;
        }

        public void Initialize()
        {

        }

        public void Update(int idG, int idP, string value)
        {
        }
    }
}
