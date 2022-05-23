using NetCoreTest.DataAccess.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTest.DataAccess.ViewModel
{
    public class WorkerServiceModel
    {
        public string Command { get; set; }
        public Test01 Data { get; set; }
    }
}
