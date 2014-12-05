using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MMHTPCourseProject.Model
{
    public class InputData
    {
        private ObservableCollection<InputItem> _items;
        private ObservableCollection<Substance> _substances = new ObservableCollection<Substance>();

        public double D { get; set; }

        public double L { get; set; }

        public double N { get; set; }

        public double Ro { get; set; }

        public double Mu { get; set; }

        public double Delta { get; set; }

        public double Nu { get; set; }
        public double WStart { get; set; }
        public double WEnd { get; set; }
        public double H { get; set; }

        public ObservableCollection<InputItem> Items
        {
            get { return _items ?? (_items = new ObservableCollection<InputItem>()); }
            set { _items = value; }
        }

        public ObservableCollection<Substance> Substances
        {
            get
            {
                if (_substances == null)
                {
                    _substances = new ObservableCollection<Substance>();
                }
                _substances = new ObservableCollection<Substance>(_substances.OrderBy(x => x.Density));
                return _substances;
            }
            set { _substances = value; }
        }


    }
}
