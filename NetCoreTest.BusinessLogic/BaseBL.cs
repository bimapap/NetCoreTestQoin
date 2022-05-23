using NetCoreTest.DataAccess.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTest.BusinessLogic
{
    public class BaseBL
    {
        protected DatabaseContext Context { get; }
        public BaseBL()
        {
            Context = new DatabaseContext();
        }
    }
}
