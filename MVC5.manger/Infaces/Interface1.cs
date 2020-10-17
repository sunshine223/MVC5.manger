using MVC5.manger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC5.manger.Infaces
{
   public interface Interface1
    {
        Task<List<department>> GetAll();
    }
}
