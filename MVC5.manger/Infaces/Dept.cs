using MVC5.manger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC5.manger.Infaces
{
    public class Dept : Interface1
    {

        public List<department> dt = new List<department>();

        public Task<List<department>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}