using System.Windows;

namespace MMHTPCourseProject.Chart
{
    public class SeriesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SeriesTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            if (item is DataSeriesInfo)
            {
                    return SeriesTemplate;
            }

            // default
            return null;

        }
    }
}
