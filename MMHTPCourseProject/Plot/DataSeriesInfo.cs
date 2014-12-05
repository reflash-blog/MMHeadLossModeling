using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMLab2.View.Plot
{
    public class DataSeriesInfo
    {
        /// <summary>
        /// Класс DataSeriesInfo
        /// 
        /// Класс, отвечающий за отображение графиков. 
        /// 
        /// DataSeriesName - имя графика,
        /// DataSeriesItems - набор его точек
        /// </summary>
        public string DataSeriesName { get; set; }
        public List<DataSeriesItem> DataSeriesItems { get; set; }
    }
}
