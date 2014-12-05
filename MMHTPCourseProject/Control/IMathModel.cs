using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MMHTPCourseProject.Chart;
using MMHTPCourseProject.Model;

namespace MMHTPCourseProject.Control
{
    interface IMathModel
    {
        Task<double> Process(InputData inputData);
        Task<ObservableCollection<DataSeriesInfo>> ProcessPressure(InputData inputData);
        Task<ObservableCollection<DataSeriesInfo>> ProcessDensity(InputData inputData);
    }
}
