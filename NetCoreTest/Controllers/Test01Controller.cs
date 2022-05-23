using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreTest.BusinessLogic;
using NetCoreTest.DataAccess.Context.Models;
using NetCoreTest.DataAccess.ViewModel;

namespace NetCoreTest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test01Controller : ControllerBase
    {
        private readonly ILogger<Test01Controller> _logger;
        private readonly Test01BL BusinessLogic;
        private readonly string ActionName;
        private readonly ResultViewModel Result;

        public Test01Controller(ILogger<Test01Controller> logger)
        {
            BusinessLogic = new Test01BL();
            ActionName = this.GetType().Name;
            _logger = logger;
            Result = new ResultViewModel();
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id) 
        {
            try
            {
                var Data = BusinessLogic.Read(Id);
                Result.SetDataResult(data : Data);
            }
            catch (Exception Ex)
            {
                Result.SetExceptionResult();
                _logger.LogError($"Error ActionName = {ActionName} Message = {Ex.Message}");
            }
            return Ok(Result);
        }

        [HttpGet("{CurrentPage}/{Limit}")]
        public IActionResult Get(int CurrentPage, int Limit)
        {
            try
            {
                var Data = BusinessLogic.Read(CurrentPage, Limit);
                Result.SetDataResult(data: Data);
            }
            catch (Exception Ex)
            {
                Result.SetExceptionResult();
                _logger.LogError($"Error ActionName = {ActionName} Message = {Ex.Message}");
            }
            return Ok(Result);
        }

        [HttpPost]
        public IActionResult Add(Test01 Model)
        {
            try
            {
                BusinessLogic.Create(Model);
                Result.SetWithoutDataResult(isSuccess:true, message: $"Success Add Data.");
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
                BusinessLogic.Update(Model);
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
                BusinessLogic.Delete(Id);
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