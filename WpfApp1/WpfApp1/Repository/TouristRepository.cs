using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class TouristRepository : IRepository<Tourist>
    {
        private const string _filePath = "../../../Resources/Data/tourists.csv";

        private readonly Serializer<Tourist> _serializer;

        private List<Tourist> _tourists;

        public TouristRepository()
        {
            _serializer = new Serializer<Tourist>();
            _tourists = new List<Tourist>();
            _tourists = _serializer.FromCSV(_filePath);
        }
        public Tourist Get(int id)
        {
            return _tourists.Find(t => t.Id == id);
        }

        public List<Tourist> GetAll()
        {
            return _tourists;
        }
    }
}
