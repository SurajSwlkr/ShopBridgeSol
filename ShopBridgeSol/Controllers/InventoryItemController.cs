using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShopBridgeSol.Application_start;
using ShopBridgeSol.Models;
using ShopBridgeSol.Repo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopBridgeSol.Controllers
{
     
    [ApiController]
    [Authorize]
    public class InventoryItemController : ControllerBase
    {
        private ItemRepo objItemRepo;
        private const string StrAPIName = "InventoryItem";
        dynamic objLogger;
        private readonly IWebHostEnvironment webHostEnvironment;
        public InventoryItemController(IWebHostEnvironment _webHostEnvironment)
        {
            objItemRepo = new ItemRepo();
            objLogger = Logger.Instance;
            webHostEnvironment = _webHostEnvironment;
        }
        
        [HttpGet]
        [Route("api/[controller]/Get")]

        public async Task<IActionResult> Get()
        {
            return Ok(await objItemRepo.GetData());
        }
        [HttpGet]
        [Route("api/[controller]/GetByID/{ItemID}")]
        public async Task<ActionResult> GetByID(int ItemID)
        {
            try
            {
                return Ok(await objItemRepo.GetDatabyID(ItemID));
            }
            catch (Exception ex)
            {
                ResultOut myflag = new ResultOut();
                objLogger.LogAPIError(StrAPIName + "GetByID" + "(Get)", ex.ToString());
                myflag.IsSuccess = false;
                myflag.Msg = ex.ToString();
                return Ok(myflag);
            }
        }
        [Route("api/[controller]/AddData")]
        [HttpPost]
        public async Task<IActionResult> AddData(Inventory objInventory)
        {
           ResultOut obj=new ResultOut();
            try
            {
                if (objInventory == null)
                {
                    objLogger.LongAPIError(StrAPIName + "AddData(Post)", "Json Format is Null");
                    obj.IsSuccess = false;
                    obj.Msg = "Json Format is Null";                     
                    return Ok(obj);
                }              

                var Data = await objItemRepo.AddData(objInventory);
                if (Data.IsSuccess)
                {
                    if (objInventory.ImageBase64Code != "")
                    {
                        objLogger.SaveImage(objInventory.ImageBase64Code, objInventory.ItemImageName);
                    }
                }
                
                
                return Ok(Data);
            }
            catch (Exception ex)
            {
                objLogger.LogAPIError(StrAPIName + "AddData(Post)", ex.ToString());
                obj.IsSuccess = false;
                obj.Msg =ex.ToString();
                return Ok(obj);
            }
        }
        
        [Route("api/[controller]/UpdateData")]
        [HttpPost]
        public async Task<IActionResult> UpdateData(Inventory objInventory)
        {
            ResultOut obj = new ResultOut();
            try
            {
                if (objInventory == null)
                {
                    objLogger.LongAPIError(StrAPIName + "UpdateData(Post)", "Json Format is Null");
                    obj.IsSuccess = false;
                    obj.Msg = "Json Format is Null";
                    return Ok(obj);
                }
                var Data = await objItemRepo.UpdateData(objInventory);
                if (Data.IsSuccess)
                {
                    objLogger.SaveImage(objInventory.ImageBase64Code, objInventory.ItemImageName);
                }
                return Ok(Data);
            }
            catch (Exception ex)
            {
                objLogger.LogAPIError(StrAPIName + "UpdateData(Post)", ex.ToString());
                obj.IsSuccess = false;
                obj.Msg = ex.ToString();
                return Ok(obj);
            }
        }
        [HttpPut]
        [Route("api/[controller]/DeleteComplaint")]
        public async Task<ActionResult> DeleteData(Inventory objInventory)
        {
            ResultOut myflag = new ResultOut();
            if (objInventory == null)
            {
               
                objLogger.LogAPIError(StrAPIName + "DeleteData_(Post)", "Json Format is Null");
                myflag.IsSuccess = false;
                myflag.Msg = "Json Format is Null";                
                return Ok(myflag);
            }
             try
            {
                 int Res = await objItemRepo.Delete(objInventory.ItemID);
                if (Res > 0)
                {
                    myflag.IsSuccess = false;
                    myflag.Msg = "Success";
                }
                else
                {
                    myflag.IsSuccess = false;
                    myflag.Msg = "Error";
                }
                
                return Ok(myflag);
            }
            catch (Exception ex)
            {
                 
                objLogger.LogAPIError(StrAPIName + "DeleteData(Post)", ex.ToString());
                myflag.IsSuccess = false;
                myflag.Msg = ex.ToString();
                return Ok(myflag);
            }
        }

    }
}
