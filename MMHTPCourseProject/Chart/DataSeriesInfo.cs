using System.Collections.Generic;

namespace MMHTPCourseProject.Chart
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
