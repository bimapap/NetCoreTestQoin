using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTest.DataAccess.ViewModel
{
    public class ResultViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public void SetResult(bool isSuccess, object data, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
        public void SetExceptionResult(string message = "")
        {
            IsSuccess = false;
            Message = message == "" ? "Something Wrong." : message;
            Data = new object();
        }
        public void SetDataResult(object data)
        {
            IsSuccess = true;
            Message = String.Empty;
            Data = data;
        }
        public void SetWithoutDataResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

    }
}
