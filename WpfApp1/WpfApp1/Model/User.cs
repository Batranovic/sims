using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.Enums;
using WpfApp1.Serializer;


namespace WpfApp1.Model
{
    public  class User : WpfApp1.Serializer.ISerializable
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _username;
        private string _password;
        private string _email;
        private UserKind _userKind;

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

        public string Name
        {
            get => _name;
            set
            {
                if(value != null)
                {
                    _name = value;
                }
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (value != null)
                {
                    _surname = value;
                }
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (value != null)
                {
                    _username = value;
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (value != null)
                {
                    _password = value;
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if(value != null)
                {
                    _email = value;
                }
            }
        }

        public UserKind UserKind
        {
            get => _userKind;
            set
            {
                if (value != null)
                {
                    _userKind = value;
                }
            }
        }

        protected User(int id, string name, string surname, string username, string password, string email, UserKind userKind)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Username = username;
            Password = password;
            Email = email;
            UserKind = userKind;
        }

        protected User()
        {

        }

        public virtual string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Surname,
                Username,
                Password,
                Email,
                UserKind.ToString()
            };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            Username = values[3];
            Password = values[4];
            Email = values[5];
            UserKind = Enum.Parse<UserKind>(values[6]);
        }




    }
}
