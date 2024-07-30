using System.Collections.Generic;
using System.Windows.Controls;


namespace ConceptorUI.Models
{
    class TextElement
    {
        public TextBlock ComponentElement;
        public List<GroupProperties> GroupProps;
        public TextElement()
        {
            configureProps();
            Initialize();
        }

        public TextElement(List<GroupProperties> groupProps)
        {
            CopyConfigureProps(groupProps);
            Initialize();
        }

        private void Initialize()
        {
            ComponentElement = new TextBlock();
            ComponentElement.Text = "My text";
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
