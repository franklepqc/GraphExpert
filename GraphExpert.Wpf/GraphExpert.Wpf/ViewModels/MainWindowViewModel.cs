using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphExpert.Wpf.ViewModels
{
    public class MainWindowViewModel : Prism.Mvvm.BindableBase
    {
        public ObservableCollection<StopVM> Formes { get; private set; } = new ObservableCollection<StopVM>();

        public void AjouterArret(double x, double y, string etiquette)
        {
            var arret = new StopVM(x, y, (true == Formes?.Any() ? (byte)(Formes.Max(k => k.Id) + 1) : (byte)1) , "");

            Formes.Add(arret);
        }

        public bool AjouterLiaison(StopVM arret1, StopVM arret2)
        {
            return true;
        }
    }
}
