using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace ConceptorUI.Models
{
    class TextElement
    {
        public TextBlock componentElement;
        public List<GroupProperties> groupProps;
        public TextElement()
        {
            configureProps();
            initialize();
        }

        public TextElement(List<GroupProperties> groupProps)
        {
            CopyConfigureProps(groupProps);
            initialize();
        }

        private void initialize()
        {
            componentElement = new TextBlock();
            componentElement.Text = "My texte";
        }

        public void Update(int idG, int idP, string value)
        {

        }

        private void configureProps()
        {

        }

        private void CopyConfigureProps(List<GroupProperties> groupProps)
        {

        }
    }
}
