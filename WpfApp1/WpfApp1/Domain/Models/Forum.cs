﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Domain.Models
{
    public class Forum : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private Location _location;
        private ForumComments _firstComment;
        private List<ForumComments> _comments;
        private bool _isOpen;
        private Guest _guest;
        public bool IsUsefull { get; set; }
      
        public int Id
        {
            get => _id;
            set
            {
                if(value != null)
                {
                    _id = value;
                }
            }
        }

        public Location Location
        {
            get => _location;
            set
            {
                if (value != null)
                {
                    _location = value;
                }
            }
        }

        public ForumComments FirstComment
        {
            get => _firstComment;
            set
            {
                if(value != null)
                {
                    _firstComment = value;
                }
            }
        }

        public List<ForumComments> Comments
        {
            get => _comments;
            set
            {
                if(value != null)
                {
                    _comments = value;
                }
            }
        }

        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                if(value != null)
                {
                    _isOpen = value;
                }
            }
        }

        public Guest Guest
        {
            get => _guest;
            set
            {
                if(value != _guest)
                {
                    _guest = value;
                }
            }
        }

        public Forum(int id, Location location, ForumComments firstComment, List<ForumComments> comments, bool isOpen, Guest guest)
        {
            Id = id;
            Location = location;
            FirstComment = firstComment;
            Comments = comments;
            IsOpen = isOpen;
            Guest = guest;
            IsUsefull = false;
        }

        public Forum()
        {
            Comments = new();
            Location = new();
            Guest = new();
            IsUsefull = false;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Location.Id.ToString(),
                IsOpen.ToString(),
                Guest.Id.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Location.Id = Convert.ToInt32(values[1]);
            IsOpen = Convert.ToBoolean(values[2]);
            Guest.Id = Convert.ToInt32(values[3]);
        }
    }
}
