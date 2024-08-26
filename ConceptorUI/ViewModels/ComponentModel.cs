using System.Windows;
using System.Windows.Input;
using ConceptorUI.Models;


namespace ConceptorUi.ViewModels
{
    internal class ComponentModel : Component
    {
        public string Address { get; set; }

        public ComponentModel()
        {
            Name = ComponentList.Component;
            Address = string.Empty;
            IsInComponent = true;
            /*
                --> Demande de la reference du composant dans la liste des composants, grâce à un évenement
                --> La reception du composant envoyé se fait dans une autre méthode
                --> On effectue une affectation directe des propriétés originales
                --> Le composant appelant (ca) possède lui egalement une liste des proprétés qu'il a modifiées
                --> Pour les propriétés modifiées, on fait une affectation directe des props de ca (*)
                -----------------------------------------------
                --- En réalité, le ca reçoit la ref entère de co en non seulement ses props
                    --> L'objectif est de construire le ca et ses enfants;
                    --> Les enfants du ca sont des ca => Tout ca demande à son enfant d'être un ca;
                    --> Il faut aussi appliquer les contraintes de visibilité, de sélection du co, sur les ca;
                    --> A chaque fois qu'une propriété modifiable du ca est modifiée, On envoie le signale dans le modèle des propriétés.
                    
                --- Reussir à effectuer la copie directe des mises en page: => Crée un conflit avec (*)
                    --> Lors de la reception du co, on effecte les props et les mises en pages.
                    --> Lorsqu'un co est demandé et qu'il n'est pas encore construit, on le construit d'abord avant de l'envoyer
                    
                --- Application des contraintes de visibilité et de sélection
                    --> On ne peut pas ajouter ou supprimer un composant dans un ca;
                    --> On ne peut pas modifier la mise en page d'un ca, même si elle est visible.
                    
                --- Création des 'cos'
                    --> Dans le panel des propriétés, ajouter un bouton de configuration pour chaque groupe;
                    --> Lorsque ce bouton est cliqué, un modal s'ouvre et affiche un tableau ayant des champs suivants:
                        - Nom de la propriétés;
                        - Champ indiquant si la propriété peut être sélectionner dans les ca;
                        - Champ indiquant si la propriété peut être visible dans le ca;
                    --> Remarque: On constate qu'on a maintenant deux types de visibilités:
                        - Visibilité naturelle du composant: c'est la visibilité qu'on impose lors de la construction
                          du composant. Même les ca ne peuvent pas la modifier;
                        - Visibilité des ca: cette visibilité permet aux 'cas' de décider de la visibilité des propriétés
                          naturellement visibles.
                -------------------------------------------------
                --- Un autre aspect à mettre en place:
                    --> Mettre en place les modifications collectives;
                    --> Exemple: Dans un bouton, la mise en forme du texte se fait à partir du bouton;
                    --> On ajoute simplement certaines propriétés du texte au bouton;
                    --> Généralisation:
                        - Soit un composant ayant n enfants de même type, on veut que la modification
                          de la mise en forme des enfants se font à partir du parent;
                        - On veut aussi pouvoir effectuer les modifications sur les enfants de même type
                          simultanement;
             */
            OnInitialize();
        }

        public override void SelfConstraints()
        {
            /* Global */
            SetGroupVisibility(GroupNames.Global);
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment);
            /* Self Alignment */
            SetGroupVisibility(GroupNames.SelfAlignment, false);
            /* Transform */
            SetGroupVisibility(GroupNames.Transform);
            SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "300");
            SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "100");
            /* Text */
            SetGroupVisibility(GroupNames.Text);
            /* Appearance */
            SetGroupVisibility(GroupNames.Appearance);
            SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFF");
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow);
        }

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            return false;
        }
        
        protected override void WhenFileLoaded(string value)
        {
            
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
        {
            
        }

        protected override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
            
        }

        protected override void WhenTextChanged(string propertyName, string value)
        {
            
        }

        protected override void InitChildContent()
        {
            
        }
        
        protected override void AddIntoChildContent(FrameworkElement child)
        {
            
        }

        protected override bool AllowExpanded(bool isWidth = true)
        {
            return false;
        }

        protected override void Delete()
        {
            
        }
        
        protected override void WhenWidthChanged(string value)
        {
            
        }
        
        protected override void WhenHeightChanged(string value)
        {
            
        }
        
        protected override void OnMoveLeft()
        {
            
        }
        
        protected override void OnMoveRight()
        {
            
        }
        
        protected override void OnMoveTop()
        {
            
        }
        
        protected override void OnMoveBottom()
        {
            
        }
    }
}
