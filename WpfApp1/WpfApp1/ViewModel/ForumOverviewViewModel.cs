using System;
using System.Collections.ObjectModel;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public class ForumOverviewViewModel : ViewModelBase
    {
        private IForumService _forumService;
        private IForumCommentsService _forumCommentService;

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

        private string _selectedState;
        public string SelectedState
        {
            get => _selectedState;
            set
            {
                _selectedState = value;
                OnPropertyChanged(nameof(SelectedState));
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

        private bool _visibilityCreateComment;
        public bool VisibilityCreateComment
        {
            get => _visibilityCreateComment;
            set
            {
                _visibilityCreateComment = value;
                OnPropertyChanged(nameof(VisibilityCreateComment));
            }
        }

        private string _newComment;
        public string NewComment
        {
            get => _newComment;
            set
            {
                _newComment = value;
                OnPropertyChanged(nameof(NewComment));
                ConfirmNewCommentCommand.RaiseCanExecuteChanged();
            }
        }

        private ForumComments _selectedComment;
        public ForumComments SelectedComment
        {
            get => _selectedComment;
            set
            {
                _selectedComment = value;
                OnPropertyChanged(nameof(SelectedComment));
            }
        }

        public RelayCommand ShowCommentCommand { get; set; }
        public RelayCommand NewCommentCommand { get; set; }
        public RelayCommand ConfirmNewCommentCommand { get; set; }
        public RelayCommand CancelNewCommentCommand { get; set; }
        public RelayCommand ReportCommand { get; set; }
        public ForumOverviewViewModel(Owner owner)
        {
            _forumService = InjectorService.CreateInstance<IForumService>();
            _forumCommentService = InjectorService.CreateInstance<IForumCommentsService>();

            Init(owner);
            InitCommand();
        }

        private void Init(Owner owner)
        {
            TabPosition = 0;
            LoggedOwner = owner;
            Forums = new(_forumService.GetAll());
            foreach(var f in  Forums)
            {
                bool tmp1 = f.Comments.FindAll(fc => fc.Author.UserKind == Domain.Models.Enums.UserKind.Guest).Count >= 20;
                bool tmp2 = f.Comments.FindAll(fc => fc.Author.UserKind == Domain.Models.Enums.UserKind.Owner).Count >= 20;
                f.IsUsefull = tmp1 && tmp2 ? true : false;
            }
            SelectedForum = Forums[0];
            ForumComments = new(SelectedForum.Comments);
        }

        private void InitCommand()
        {
            ShowCommentCommand = new(param => Execute_ShowCommentCommand(), param => CanExecute());
            NewCommentCommand = new(param => Execute_NewCommentCommand(), param => CanExecute());
            ConfirmNewCommentCommand = new(param => Execute_ConfirmNewCommentCommand(), param => CanExecute_ConfirmNewCommentCommand());
            CancelNewCommentCommand = new(param => Execute_CancelNewCommentCommand(), param => CanExecute());
            ReportCommand = new(Execute_ReportCommand, param => CanExecute());
        }

        private void Execute_ShowCommentCommand()
        {
            TabPosition = 1;
            ForumComments.Clear();
            SelectedState = SelectedForum.Location.City;
            foreach(var fc in SelectedForum.Comments)
            {
                ForumComments.Add(fc);
            }
            OnPropertyChanged(nameof(SelectedState));
            OnPropertyChanged(nameof(SelectedForum));
        }

        private void Execute_NewCommentCommand()
        {
            VisibilityCreateComment = true;
            ForumComments.Clear();
            foreach (var item in _forumCommentService.GetAll().FindAll(f => f.Forum.Id == SelectedForum.Id))
            {
                ForumComments.Add(item);
            }
            OnPropertyChanged(nameof(ForumComments));
        }
        
        private void Execute_ReportCommand(object sender)
        {
            if (sender != null  && sender is ForumComments forumComments)
            {
                if (forumComments.ForumReports.Find(fc => fc.Author.Equals(LoggedOwner)) != null)
                {
                    MessageBox.Show("You have been reported comment", "Warning");
                    return;
                }
                SelectedComment = forumComments;
                
                ReportForum rf = new();
                rf.ForumComment = SelectedComment;
                rf.Author = LoggedOwner;
                InjectorService.CreateInstance<IReportForumService>().Create(rf);

                SelectedComment.ForumReports.Add(rf);
                SelectedComment.Report= SelectedComment.ForumReports.Count;
                _forumCommentService.Update(SelectedComment);
                ForumComments.Clear();
                foreach (var item in _forumCommentService.GetAll().FindAll(f => f.Forum.Id == SelectedForum.Id))
                {
                    ForumComments.Add(item);
                }
                OnPropertyChanged(nameof(ForumComments));

            }
        }

        private void Execute_CancelNewCommentCommand()
        {
            VisibilityCreateComment = false;
        }

        private void Execute_ConfirmNewCommentCommand()
        {
            ForumComments fc = new();
            fc.Comment = NewComment;
            fc.Date = DateTime.Now;
            fc.Author = LoggedOwner;
            fc.Report = 0;
            _forumCommentService.Create(fc);
            VisibilityCreateComment = false;
            ForumComments.Clear();
            foreach (var item in _forumCommentService.GetAll().FindAll(f => f.Forum.Id == SelectedForum.Id))
            {
                ForumComments.Add(item);
            }
            OnPropertyChanged(nameof(ForumComments));
        }

        private bool CanExecute_ConfirmNewCommentCommand()
        {
            return !String.IsNullOrEmpty(NewComment);
        }

        private bool CanExecute()
        {
            return true;
        }

    }
}
