using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTest.BusinessLogic
{
    public interface IBaseBL<T>
    {
        List<T> Read(int CurrentPage, int Limit);
        T Read(int Id);
        void Create(T Model);
        void Update(T Model);
        void Delete(int Id);
    }
}
