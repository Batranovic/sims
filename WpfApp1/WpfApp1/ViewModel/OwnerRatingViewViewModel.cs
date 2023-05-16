using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public class OwnerRatingViewViewModel
    {
        public ObservableCollection<OwnerRating> RatingOwners { get; set; }
        private readonly IOwnerRatingService _ownerRatingService;

        public Owner LoggedUser { get; set; }
        public OwnerRatingViewViewModel(Owner owner)
        {
            _ownerRatingService = InjectorService.CreateInstance<IOwnerRatingService>();

            RatingOwners = new ObservableCollection<OwnerRating>(_ownerRatingService.GetAllOwnerRewies(owner.Id));
            LoggedUser = owner;
        }
    }
}
