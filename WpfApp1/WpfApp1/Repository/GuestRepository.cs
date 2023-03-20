using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class GuestRepository : IRepository<Guest>
    {
        private const string _filePath = "../../../Resources/Data/guests.csv";

        private readonly Serializer<Guest> _serializer;

        private List<Guest> _guests;
        private static GuestRepository _instance = null;

        public static GuestRepository GetInsatnce()
        {
            if(_instance == null)
            {
                _instance = new GuestRepository();
            }
            return _instance;
        }

        private GuestRepository()
        {
            _serializer = new Serializer<Guest>();
            _guests = new List<Guest>();
            _guests = _serializer.FromCSV(_filePath);
        }

        public Guest Get(int id)
        {
            return _guests.Find(o => o.Id == id);
        }

        public List<Guest> GetAll()
        {
            return _guests;
        }
    }
}
