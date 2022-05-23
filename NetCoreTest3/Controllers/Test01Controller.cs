using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreTest.DataAccess.Context.Models;
using NetCoreTest.DataAccess.ViewModel;
using NetCoreTest.Helper;

namespace NetCoreTest3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test01Controller : ControllerBase
    {
        private readonly ILogger<Test01Controller> _logger;
        private readonly string ActionName;
        private readonly RabbitMqHelper Helper;
        private readonly ResultViewModel Result;

        public Test01Controller(ILogger<Test01Controller> logger)
        {
            _logger = logger;
            ActionName = this.GetType().Name;
            Helper = new RabbitMqHelper();
            Result = new ResultViewModel();
        }

        [HttpPost]
        public IActionResult Add(Test01 Model)
        {
            try
            {
                Helper.SendMessage(new WorkerServiceModel() { Command = "add", Data = Model});
                Result.SetWithoutDataResult(isSuccess: true, message: $"Success Add Data.");
            }
            catch (Exception Ex)
            {
                Result.SetExceptionResult();
                _logger.LogError($"Error ActionName = {ActionName} Message = {Ex.Message}");
            }
            return Ok(Result);
        }

        [HttpPut]
        public IActionResult Update(Test01 Model)
        {
            try
            {
                Helper.SendMessage(new WorkerServiceModel() { Command = "update", Data = Model });
                Result.SetWithoutDataResult(isSuccess: true, message: $"Success Update Data.");
            }
            catch (Exception Ex)
            {
                Result.SetExceptionResult();
                _logger.LogError($"Error ActionName = {ActionName} Message = {Ex.Message}");
            }
            return Ok(Result);
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                var data = new Test01() { Id = Id };
                Helper.SendMessage(new WorkerServiceModel() { Command = "delete", Data = data });
                Result.SetWithoutDataResult(isSuccess: true, message: $"Success Delete Data.");
            }
            catch (Exception Ex)
            {
                Result.SetExceptionResult();
                _logger.LogError($"Error ActionName = {ActionName} Message = {Ex.Message}");
            }
            return Ok(Result);
        }

    }
}
