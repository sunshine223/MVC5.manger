using IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceA : IServiceA
    {
        public int C()
        {
            return 2;
        }
    }
}
