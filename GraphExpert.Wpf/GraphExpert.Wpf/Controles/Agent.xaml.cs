using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphExpert.Wpf.Controles
{
    /// <summary>
    /// Logique d'interaction pour Agent.xaml
    /// </summary>
    public partial class Agent : UserControl
    {
        /// <summary>
        /// Propriété "Top"
        /// </summary>
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register(
                "Top",
                typeof(double),
                typeof(Agent),
                new PropertyMetadata(new PropertyChangedCallback(SurChangementProprieteTop)));

        /// <summary>
        /// Propriété "Left"
        /// </summary>
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register(
                "Left",
                typeof(double),
                typeof(Agent),
                new PropertyMetadata(new PropertyChangedCallback(SurChangementProprieteLeft)));

        static Agent()
        {
            _couleurProperty = DependencyProperty.Register(
                "Couleur",
                typeof(Brush),
                typeof(Agent),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));
        }

        public Agent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Propriété.
        /// </summary>
        public static DependencyProperty _couleurProperty;

        /// <summary>
        /// Obtient ou assigne la couleur par défaut.
        /// </summary>
        public Brush Couleur
        {
            get { return GetValue(_couleurProperty) as Brush; }
            set { SetValue(_couleurProperty, value); }
        }

        /// <summary>
        /// Coordonnée Y.
        /// </summary>
        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        /// <summary>
        /// Coordonnée X.
        /// </summary>
        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        /// <summary>
        /// Sur changement de la propriété.
        /// </summary>
        /// <param name="sender">Objet appelant.</param>
        /// <param name="args">Arguments.</param>
        protected static void SurChangementProprieteTop(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((Agent)sender).SurChangementProprieteTop((double)args.NewValue);
        }

        /// <summary>
        /// Sur changement de la propriété.
        /// </summary>
        /// <param name="sender">Objet appelant.</param>
        /// <param name="args">Arguments.</param>
        protected static void SurChangementProprieteLeft(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((Agent)sender).SurChangementProprieteLeft((double)args.NewValue);
        }

        /// <summary>
        /// Modifier la propriété.
        /// </summary>
        /// <param name="valeur">Valeur.</param>
        protected virtual void SurChangementProprieteTop(double valeur)
        {
            var conteneur = TrouverPresentateur(this);

            conteneur?.SetValue(Canvas.TopProperty, valeur);
        }

        /// <summary>
        /// Modifier la propriété.
        /// </summary>
        /// <param name="valeur">Valeur.</param>
        protected virtual void SurChangementProprieteLeft(double valeur)
        {
            var conteneur = TrouverPresentateur(this);

            conteneur?.SetValue(Canvas.LeftProperty, valeur);
        }

        /// <summary>
        /// Récupérer l'item qui contient ce canvas.
        /// </summary>
        /// <param name="enfant">Enfant.</param>
        /// <returns>Retourne le parent qui le contient.</returns>
        private DependencyObject TrouverPresentateur(DependencyObject enfant)
        {
            var parent = VisualTreeHelper.GetParent(enfant);

            if (null == parent) return null;

            if (parent.GetType() != typeof(ContentPresenter))
            {
                return TrouverPresentateur(parent);
            }

            return parent;
        }
    }
}
