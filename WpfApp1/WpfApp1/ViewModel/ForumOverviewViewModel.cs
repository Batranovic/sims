using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public class ForumOverviewViewModel : ViewModelBase
    {
        private IForumService _forumService;
       
        public ObservableCollection<Forum> Forums { get; set; }
        public Owner LoggedOwner { get; set; }

        private Forum _selectedForum;
        public Forum SelectedForum
        {
            get => _selectedForum;
            set
            {
                _selectedForum = value;
                OnPropertyChanged(nameof(SelectedForum));
            }
        }

        public  ForumOverviewViewModel(Owner owner)
        {
            _forumService = InjectorService.CreateInstance<IForumService>();

            Init(owner);
            InitCommand();
        }

        private void Init(Owner owner)
        {
            LoggedOwner = owner;
            Forums = new(_forumService.GetAll());
            SelectedForum = Forums[0];
        }

        private void InitCommand()
        {

        }

    }
}
