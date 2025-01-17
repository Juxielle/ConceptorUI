using System.Collections.Generic;
using ConceptorUI.ViewModels.ReusableComponent;


namespace ConceptorUI.Models
{
    class Properties
    {
        public static ComponentList ComponentName = ComponentList.Window;
        public static string CopiedComponent = null!;
        public int SelectedLeftOnglet;

        public List<ComponentModel> ComponentMNS;
        public int SelectedComponent;

        public int SelectedSpace;
        public int SelectedReport;

        private static Properties? _obj;

        public List<Reference> References;

        public Properties()
        {
            _obj = this;
            SelectedLeftOnglet = 1;

            ComponentMNS = new List<ComponentModel>();
            SelectedComponent = 0;

            SelectedSpace = 0;
            SelectedReport = 0;

            References = new List<Reference>();
        }

        public static Properties Instance => _obj == null! ? new Properties() : _obj;
    }
}
