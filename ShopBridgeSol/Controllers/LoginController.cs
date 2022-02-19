using Microsoft.AspNetCore.Mvc;
using ShopBridgeSol.Application_start;
using ShopBridgeSol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopBridgeSol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private Repo.LoginRepo objItemLoginRepo;
        private const string StrAPIName = "Login";
        dynamic objLogger;

        public LoginController()
        {
            objItemLoginRepo = new Repo.LoginRepo();
            objLogger = Logger.Instance;
        }



        [HttpPost]
        public async Task<IActionResult> Post(Login objLogin)
        {
            ResultOut obj = new ResultOut();
            try
            {
                if (objLogin == null)
                {
                    objLogger.LongAPIError(StrAPIName + "AddData(Post)", "Json Format is Null");
                    obj.IsSuccess = false;
                    obj.Msg = "Json Format is Null";
                    return Ok(obj);
                }
                var Data = await objItemLoginRepo.GetuserDetails(objLogin);
                
                return Ok(Data);
            }
            catch (Exception ex)
            {
                objLogger.LogAPIError(StrAPIName + "Login(Post)", ex.ToString());
                obj.IsSuccess = false;
                obj.Msg = ex.ToString();
                return Ok(obj);
            }
        }

         
    }
}
