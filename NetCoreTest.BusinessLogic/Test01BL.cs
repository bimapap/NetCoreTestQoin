using NetCoreTest.DataAccess.Context;
using NetCoreTest.DataAccess.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTest.BusinessLogic
{
    public interface ITest01BL : IBaseBL<Test01>
    { 
    
    }

    public class Test01BL : BaseBL, ITest01BL
    {
        public void Create(Test01 Model)
        {
            Context.Test01.Add(Model);
            Context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var Data = Read(Id);
            if (Data != null)
            {
                Context.Remove(Data);
                Context.SaveChanges();
            }
        }

        public List<Test01> Read(int CurrentPage, int Limit)
        {
            return Context.Test01.GetPaged(CurrentPage, Limit).Results.ToList();
        }

        public Test01 Read(int Id)
        {
            return Context.Test01.FirstOrDefault(x => x.Id == Id);
        }

        public void Update(Test01 Model)
        {
            var Data = Read(Model.Id);
            if (Data != null)
            {
                Data.Nama = Model.Nama;
                Data.Status = Model.Status;
                Data.Updated = DateTime.Now;
                Context.SaveChanges();
            }
        }
    }
}
