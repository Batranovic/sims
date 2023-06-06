using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public class ForumCommentsViewModel : ViewModelBase
    {
        private IForumService _forumService;
        private IForumCommentsService _forumCommentService;

        private Guest LoggedGuest { get; set; }

        public RelayCommand ShowCommand { get; set; }

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

        public ForumCommentsViewModel(Guest guest)
        {
            _forumService = InjectorService.CreateInstance<IForumService>();
            _forumCommentService = InjectorService.CreateInstance<IForumCommentsService>();
            LoggedGuest = guest;

            ShowCommand = new(param => Execute_ShowCommand(), param => CanExecute());


            VisibilityPopUp = false;
        }

        public void Execute_ShowCommand()
        {
            VisibilityPopUp = !VisibilityPopUp;
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}
