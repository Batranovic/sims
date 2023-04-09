using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.Domain.ServiceInterfaces
{
    public interface IImageService : IService<Image>
    {
        public void Create(Image entity);
        public void Delete(Image entity);
        public List<Image> GetAccommodations();
        public List<string> GetTour();
    }
}
