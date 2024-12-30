using System.Windows;
using System.Windows.Input;
using ConceptorUI.Models;
using ConceptorUI.ViewModels.Container;


namespace ConceptorUi.ViewModels
{
    internal class ComponentModel : Component
    {
        public readonly ContainerModel Body;

        public ComponentModel(bool allowConstraints = false)
        {
            OnInit();
            
            Name = ComponentList.Component;
            IsInComponent = true;
            ChildContentLimit = 1;
            
            Body = new ContainerModel();
            Content.Child = Body.ComponentView;
            
            if (!allowConstraints) _init();
            
            if (allowConstraints) return;
            SelfConstraints();
            OnInitialize();
            
            /*
                --> Demande de la reference du composant dans la liste des composants, grâce à un évenement
                --> La reception du composant envoyé se fait dans une autre méthode
                --> On effectue une affectation directe des propriétés originales
                --> Le composant appelant (ca) possède lui egalement une liste des proprétés qu'il a modifiées
                --> Pour les propriétés modifiées, on fait une affectation directe des props de ca (*)
                ----------------------------------------------->
                --- En réalité, le ca reçoit la ref entère de co en non seulement ses props
                    --> L'objectif est de construire le ca et ses enfants;
                    --> Les enfants du ca sont des ca => Tout ca demande à son enfant d'être un ca;
                    --> Il faut aussi appliquer les contraintes de visibilité, de sélection du co, sur les ca;
                    --> A chaque fois qu'une propriété modifiable du ca est modifiée, On envoie le signale dans le modèle des propriétés.

                --- Reussir à effectuer la copie directe des mises en page: => Crée un conflit avec (*)
                    --> Lors de la reception du co, on affecte les props et les mises en pages;
                    --> Lorsqu'un co est demandé et qu'il n'est pas encore construit, on le construit d'abord avant de l'envoyer;
                    --> Pour résoudre le problème (*), il suffit de ne pas effectuer des affectations directes:
                        - Lorsqu'il y a une modification du co: On envoie simplement la nouvelle reference;
                        - On peut utiliser simplement le mécanisme de copier-coller pour ajouter et mettre à jour les composants;
                        - Il va rester de résoudre le probleme des modifications individuelles des cas:
                            . On peut considerer que les propriétés des co peuvent avoir plusieurs valeurs; Ou bien,
                            . Lors du changement de reference, on conserve l'ancienne reference pour appliquer les modifications grace aux identifiants.

                    ==> Pratique des Composants:
                        - On crée une liste des co dans PageView
                        - On crée un evenement qui notifie lorsqu'une propriete est modifier (La methode OnUpdate va renvoyer l'id et success)
                          Lors de la notification, on précise les infos suivantes:
                          . Nom du composant;
                          . S'il s'agit d'un co ou d'un ca;
                          . Id du composant modifié;
                          . Groupe, propriété et valeur de la propriété.
                        - NB: Les ca ont les même Id que leur co;
                        - On crée une méthode pour envoyer les modification des co aux ca;
                        - Dans OnCopyOrPaste, on precise que le composant collé est composant ou pas

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
                ------------------------------------------------->
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
        }

        private void _init()
        {
            Body.SelfConstraints();
            Body.SetGroupVisibility(GroupNames.Global, false);
            Body.SetGroupVisibility(GroupNames.SelfAlignment, false);
            
            Body.SetGroupVisibility(GroupNames.Transform, false);
            Body.SetGroupOnlyVisibility(GroupNames.Transform);
            Body.SetPropertyVisibility(GroupNames.Transform, PropertyNames.Width);
            Body.SetPropertyVisibility(GroupNames.Transform, PropertyNames.Height);
            Body.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, "300");
            Body.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, "100");

            Body.SetGroupVisibility(GroupNames.Text, false);

            Body.SetGroupVisibility(GroupNames.Appearance, false);
            Body.SetGroupOnlyVisibility(GroupNames.Appearance);
            Body.SetPropertyVisibility(GroupNames.Appearance, PropertyNames.FillColor);
            Body.SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, "#ffffff");

            Body.SetGroupVisibility(GroupNames.Shadow, false);
            Body.OnInitialize();

            Children.Add(Body);
        }

        public sealed override void SelfConstraints()
        {
            /* Global */
            SetGroupVisibility(GroupNames.Global, false);
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment, false);
            /* Self Alignment */
            SetGroupVisibility(GroupNames.SelfAlignment, false);
            SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hc, "1");
            /* Transform */
            SetGroupVisibility(GroupNames.Transform, false);
            SetGroupOnlyVisibility(GroupNames.Transform);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.X);
            SetPropertyVisibility(GroupNames.Transform, PropertyNames.Y);
            /* Text */
            SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            SetGroupVisibility(GroupNames.Appearance, false);
            SetPropertyValue(GroupNames.Appearance, PropertyNames.FillColor, ColorValue.Transparent.ToString());
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow, false);
        }

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            return false;
        }

        protected override object GetPropertyGroups()
        {
            return PropertyGroups!;
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
        }

        protected override void ContinueToInitialize(string groupName, string propertyName, string value)
        {
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

        public override void WhenTextChanged(string propertyName, string value, bool isInitialize = false)
        {
        }

        protected override void CallBack(GroupNames groupName, PropertyNames propertyName, string value)
        {
        }

        protected override void InitChildContent()
        {
        }

        protected override void AddIntoChildContent(FrameworkElement child, int k = -1)
        {
            Content.Child = child;
        }

        public override bool AllowExpanded(bool isWidth = true)
        {
            return false;
        }

        public override bool AllowAuto(bool isWidth = true)
        {
            return false;
        }

        protected override void Delete(int k = -1)
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

        protected override bool CanSetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            return true;
        }

        protected override bool CanChildSetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            return true;
        }

        protected override void RestoreProperties()
        {
        }

        protected override void CheckVisibilities()
        {
        }
    }
}