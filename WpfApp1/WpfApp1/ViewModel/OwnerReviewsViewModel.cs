using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public class OwnerReviewsViewModel : ViewModelBase
    {

        public Guest LoggedGuest { get; set; }
        public ObservableCollection<GuestRating> Reviews { get; set; }

        private readonly IGuestRatingService _guestRatingService;
        public OwnerReviewsViewModel(Guest guest)
        {
            _guestRatingService = InjectorService.CreateInstance<IGuestRatingService>();

            Reviews = new(_guestRatingService.GetAllGuestReviews(guest.Id));
        }
    }
}
