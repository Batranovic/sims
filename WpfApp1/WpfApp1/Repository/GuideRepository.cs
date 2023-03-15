using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Repository
{
    public class GuideRepository : IRepository<Guide>
    {
        private const string _filePath = "../../../Resources/Data/tourists.csv";

        private readonly Serializer<Guide> _serializer;

        private List<Guide> _guides;

        public GuideRepository()
        {
            _serializer = new Serializer<Guide>();
            _guides = new List<Guide>();
            _guides = _serializer.FromCSV(_filePath);
        }
        public Guide Get(int id)
        {
            return _guides.Find(t => t.Id == id);
        }

        public List<Guide> GetAll()
        {
            return _guides;
        }
    }
}
