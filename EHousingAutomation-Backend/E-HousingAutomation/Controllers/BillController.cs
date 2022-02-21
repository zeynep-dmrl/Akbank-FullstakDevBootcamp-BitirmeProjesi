using DAL.Dto;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_HousingAutomation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : ControllerBase
    {
        Logger logger = new Logger(); //log operation

        //db connection
        BillDBController billDbController = new BillDBController(); 
        HouseDBController houseDBController = new HouseDBController();


        //METHODS

        [Authorize]
        //get endpoint
        [HttpGet]
        public List<BillUserDto> GetBills()
        {
            // call function from dbContext
            return billDbController.GetBillUserDto();
        }


        //[Authorize]
        [HttpGet("payments")]
        public List<BillUserDto> GetBillsPaid()
        {
            return billDbController.GetBillUserDtoPaid();
        }

        [Authorize]
        [HttpGet("payments/debt")]
        public List<BillUserDto> GetBillsNotPaid()
        {
            return billDbController.GetBillUserDtoNotPaid();
        }

        [Authorize]
        [HttpGet("{id}/bills")]
        public List<BillHouseDto> GetBillHouseDto(int id)
        {
           return billDbController.GetBillHouseById(id);
        }
        [Authorize]
        [HttpGet("{id}/bills/payments")]
        public List<BillHouseDto> GetBillHouseDtoPaid(int id)
        {
            return billDbController.GetBillHouseDtoPaid(id);
        }
        [Authorize]
        //post endpoint
        [HttpPost]
        //add new bill to db
        public Result AddBill(Bill newBill)
        {
            Result _result = new Result();

            Bill? bill = billDbController.FindBill(newBill.billId);
            House? house = houseDBController.FindHouse(newBill.houseId);

            bool billCheck = (bill != null && house == null) ? true : false;

            if (billCheck)
            {
                _result.status = 0;
                _result.message = "Bill is already exist or this house does not have a bill";
                logger.createLog(_result.message);
            }
            else
            {
                //can't exist newBill in list
                if (billDbController.AddBillModel(newBill))
                {
                    _result.status = 1;
                    _result.message = "Insert new bill in list";
                    _result.billList = billDbController.GetBills().ToList();
                }
                else
                {
                    _result.status = 0;
                    _result.message = "Don't bill new house";
                    _result.billList = billDbController.GetBills().ToList();
                }

            }
            return _result;
        }

        [Authorize]
        //put endpoint
        [HttpPut("{id}")]
        //update bill infromation
        public Result UpdateBill(int id, Bill updatedBill)
        {
            Result _result = new Result();

            var bill = billDbController.FindBill(id);

            if (bill == null)
            {
                _result.status = 0;
                _result.message = "Not found this house";
                logger.createLog(_result.message);
            }
            else
            {
                if (billDbController.DeleteBillModel(id) && billDbController.AddBillModel(updatedBill))
                {
                    _result.status = 1;
                    _result.message = "Updated successfully.";
                    _result.billList = billDbController.GetBills().ToList();

                }
                else
                {
                    _result.status = 0;
                    _result.message = "Update Failed";
                }
            }
            return _result;

        }

        [Authorize]
        //delete endpoint
        [HttpDelete("{id}")]
        //delete bill from db
        public Result DeleteBill(int id)
        {
            Result _result = new Result();

            if (!billDbController.DeleteBillModel(id))
            {
                _result.status = 0;
                _result.message = "Delete failed";
            }
            else
            {
                //successfully deleted
                _result.status = 1;
                _result.message = "Deleted successfully";
                _result.billList = billDbController.GetBills().ToList();

            }
            return _result;
        }
    }
}
