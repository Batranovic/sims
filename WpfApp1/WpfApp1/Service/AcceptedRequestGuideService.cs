using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Repository;

namespace WpfApp1.Service
{

    public class AcceptedRequestGuideService : IAcceptedRequestService
    {

        private readonly IAcceptedRequestGuideRepositry _acceptedRequestGuide;

        public AcceptedRequestGuideService()
        {
            _acceptedRequestGuide = InjectorRepository.CreateInstance<IAcceptedRequestGuideRepositry>();
            BindAcceptedRequestGuideUser();
        }
        public void BindAcceptedRequestGuideUser()
        {
            foreach (AcceptedRequestGuide acceptedRequestGuide in _acceptedRequestGuide.GetAll())
            {
                int userId = acceptedRequestGuide.Guide.Id;
                Tourist tourist = TouristRepository.GetInstance().Get(userId);
                if (tourist != null)
                {
                    acceptedRequestGuide.Guide = tourist;
                }
                else
                {
                    Console.WriteLine("Error in acceptedRequestGuideUser binding");
                }
            }
        }

        public List<AcceptedRequestGuide> GetAll()
        {
            return _acceptedRequestGuide.GetAll();
        }

        public AcceptedRequestGuide Get(int id)
        {
            return _acceptedRequestGuide.Get(id);
        }

        public void Save()
        {

            _acceptedRequestGuide.Save();
        }

        public void Delete(AcceptedRequestGuide tourEvent)
        {

            _acceptedRequestGuide.Delete(tourEvent);

        }

        public AcceptedRequestGuide Update(AcceptedRequestGuide tourEvent)
        {
            return _acceptedRequestGuide.Update(tourEvent);
        }



        public void Subscribe(IObserver observer)
        {
            _acceptedRequestGuide.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _acceptedRequestGuide.Unsubscribe(observer);
        }

    }
}
