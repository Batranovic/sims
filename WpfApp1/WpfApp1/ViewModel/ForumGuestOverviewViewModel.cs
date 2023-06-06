using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public class ForumGuestOverviewViewModel : ViewModelBase
    {
        private IForumService _forumService;
        private IForumCommentsService _forumCommentService;
        private readonly ILocationService _locationService;
        private readonly IOwnerService _ownerService;
        private readonly IForumNotificationService _forumNotificationService;

        public ObservableCollection<Forum> Forums { get; set; }

        private Guest LoggedGuest { get; set; }
        
        private Forum _selectedForum;
        public RelayCommand CreateForumCommand { get; set; }
        public RelayCommand ShowCommand { get; set; }
        public RelayCommand ShowCommand1 { get; set; }
        public RelayCommand ShowCommand2 { get; set; }

        public RelayCommand CreateCommand { get; set; }

        public ObservableCollection<string> States { get; set; }

        public ObservableCollection<string> Cities { get; set; }

        private string _state;
        private string _city;

        private string _text;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public string State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
                ChosenState();
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public Forum SelectedForum
        {
            get => _selectedForum;
            set
            {
                _selectedForum = value;
                OnPropertyChanged(nameof(SelectedForum));
            }
        }

        private bool _visibilityPopUp;
        public bool VisibilityPopUp
        {
            get => _visibilityPopUp;
            set
            {
                _visibilityPopUp = value;
                OnPropertyChanged(nameof(VisibilityPopUp));
            }
        }
        private bool _visibilityPopUp1;
        public bool VisibilityPopUp1
        {
            get => _visibilityPopUp1;
            set
            {
                _visibilityPopUp1 = value;
                OnPropertyChanged(nameof(VisibilityPopUp1));
            }
        }

        private bool _visibilityPopUp2;
        public bool VisibilityPopUp2
        {
            get => _visibilityPopUp2;
            set
            {
                _visibilityPopUp2 = value;
                OnPropertyChanged(nameof(VisibilityPopUp2));
            }
        }

        public ObservableCollection<ForumComments> ForumComments { get; set; }

        private int _tabPosition;
        public int TabPosition
        {
            get => _tabPosition;
            set
            {
                _tabPosition = value;
                OnPropertyChanged(nameof(TabPosition));
            }
        }

        public ForumGuestOverviewViewModel(Guest guest)
        {
            _forumService = InjectorService.CreateInstance<IForumService>();
            _forumCommentService = InjectorService.CreateInstance<IForumCommentsService>();
            _locationService = InjectorService.CreateInstance<ILocationService>();
            _ownerService = InjectorService.CreateInstance<IOwnerService>();
            _forumNotificationService = InjectorService.CreateInstance<IForumNotificationService>();
            LoggedGuest = guest;

            ShowCommand = new(param => Execute_ShowCommand(), param => CanExecute());
            ShowCommand1 = new(param => Execute_ShowCommand1(), param => CanExecute());
            ShowCommand2 = new(param => Execute_ShowCommand2(), param => CanExecute());

            States = new ObservableCollection<string>(_locationService.GetStates());
            Cities = new ObservableCollection<string>();
            Forums = new ObservableCollection<Forum>(_forumService.GetAll());


            VisibilityPopUp = false;
            VisibilityPopUp1 = false;
            VisibilityPopUp2 = false;

             CreateForumCommand = new RelayCommand(param => Execute_CreateForumCommand(), param => CanExecute_CreateForum());
        }

        public void Execute_ShowCommand()
        {
            VisibilityPopUp = !VisibilityPopUp;
        }
        public void Execute_ShowCommand1()
        {
            VisibilityPopUp1 = !VisibilityPopUp1;
        }
        public void Execute_ShowCommand2()
        {
            VisibilityPopUp2 = !VisibilityPopUp2;
        }

        public bool CanExecute()
        {
            return true;
        }

        private void ChosenState()
        {
            Cities.Clear();
            foreach (string city in _locationService.GetCitiesFromStates(State))
            {
                Cities.Add(city);
            }
        }

        private void Execute_CreateForumCommand()
        {
            Forum forum = new Forum();
            ForumComments fc = new ForumComments();
            fc.Comment = Text;
            fc.Date = DateTime.Now;
            fc.Author = LoggedGuest;
            fc.Report = 0;
            fc.Forum = forum;
            _forumCommentService.Create(fc);
            forum.FirstComment = fc;
            forum.Comments.Add(fc);
            forum.Location = _locationService.GetByCityAndState(City, State);
            forum.IsOpen = true;
            forum.Guest = LoggedGuest;
            _forumService.Create(forum);
            fc.Forum = forum;
            _forumCommentService.Update(fc);
            Forums.Clear();
            foreach(var f in _forumService.GetAll())
            {
                Forums.Add(f);
            }
            VisibilityPopUp = false;
            foreach(Owner o in _ownerService.GetAll())
            {
                if(o.Accommodations.Find(a => a.Location.Id == forum.Location.Id) != null)
                {
                    _forumNotificationService.Create(new("Forum about " + forum.Location.City + " has been opened", DateTime.Now, forum, o));
                }
            }
        }

        private bool CanExecute_CreateForum()
        {
            return !(State == "" || City == "" || Text == "");
        }
    }
}
