using Quran.Model;
using System.Collections.ObjectModel;

namespace Quran.ViewModel
{
    internal class ViewModelAzkar
    {
        public ObservableCollection<Azkar> AllAzkars { get; set; }
        public ObservableCollection<ZakrContent> AllAzkarsSub { get; set; }
        public ObservableCollection<Tasbih> AllTasbihs { get; set; }
        public ObservableCollection<Mshaiekh> AllMshaiekh { get; set; }

        private readonly AzkarDB azkarDB = new AzkarDB();
        private readonly TasbihDB TasbihDB = new TasbihDB();
        private readonly ZakrContentDB azkaSubrDB = new ZakrContentDB();
        private readonly shak shakDB = new shak();
        public ViewModelAzkar()
        {
            AllAzkars = new ObservableCollection<Azkar>(azkarDB.GetAzkars());
            AllAzkarsSub = new ObservableCollection<ZakrContent>(azkaSubrDB.GetZakrsContent());
            AllTasbihs = new ObservableCollection<Tasbih>(TasbihDB.getAllItem());
            AllMshaiekh = new ObservableCollection<Mshaiekh>(shakDB.getAll());

        }
    }
}
