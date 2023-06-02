using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface ISimpleTourRequestService : IService<SimpleTourRequest>
    {
        void Create(SimpleTourRequest entity);
        void Delete(SimpleTourRequest entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        int NextId();

        List<SimpleTourRequest> RequestsForTourist(int userId);
        List<string> GetAllYears();
        List<SimpleTourRequest> AcceptedRequestsForTourist(int userId);
        List<SimpleTourRequest> GetAllForUser(int userId);

        Dictionary<string, int> CountRequestsByLanguage(int userId);
        Dictionary<string, int> CountRequestsByLocation(int userId);
        string GetDeniedRequestsCount(string SelectedYear);
        string GetAcceptedRequestsCount(string SelectedYear);

        int GetAverageMaxGuests(string SelectedYear);

        void NewTourFromStatistics(Tour tour);
        void AddIfRequestWasNeverFullfilled(SimpleTourRequest requestForAdding, List<SimpleTourRequest> notFullfilledRequests);

        List<SimpleTourRequest> PartsOfComplexTourRequest(int userId);


    }
}
