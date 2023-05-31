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
    public  class ForumOverviewViewModel : ViewModelBase
    {
        private IForumService _forumService;
        private IForumCommentsService _forumCommentService;

        public Owner LoggedOwner { get; set; }

        private ObservableCollection<ForumComments> _forumComments;
        public ObservableCollection<ForumComments> ForumComments
        {
            get => _forumComments;
            set
            {
                _forumComments = value;
                OnPropertyChanged(nameof(ForumComments));   
            }
        }

        private ObservableCollection<Forum> _forums;
        public ObservableCollection<Forum> Forums
        {
            get => _forums;
            set
            {
                _forums = value;
                OnPropertyChanged(nameof(Forums));  
            }
        }

        public ForumOverviewViewModel(Owner owner)
        {
            _forumService = InjectorService.CreateInstance<IForumService>();
            _forumCommentService = InjectorService.CreateInstance<IForumCommentsService>();

            init(owner);
        }

        private void init(Owner owner)
        {
            LoggedOwner = owner;
            Forums = new(_forumService.GetAll());
            ForumComments = new();
        }

    }
}
