using ConceptorUi.ViewModels;
using UiDesigner.Constants;

namespace UiDesigner.Models;

class Reference
{
    public string Address { get; set; }
    public ReferenceType Type { get; set; }
    public string File { get; set; }
    public Reference ReferenceValue { get; set; }
    /*
     * Créer une liste de références Component indépendante
     * Créer une liste de références Couleurs indépendante
     * Créer une liste de références Textes indépendante
     * Créer une liste de références Nombres indépendante
     * Créer une liste de références Styles indépendante
     * ==> Chaque type de référence possède sa propre structure de données (Car elles n'ont toutes les mêmes valeurs).
     */
}