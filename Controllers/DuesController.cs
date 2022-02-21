using DAL.Dto;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_HousingAutomation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DuesController : ControllerBase
    {
        Logger logger = new Logger(); //log operation

        //db connection
        DuesDBController duesDbController = new DuesDBController();
        HouseDBController houseDBController = new HouseDBController();

        //METHODS

        [Authorize]
        //get endpoint
        [HttpGet]
        public List<DuesUserDto> GetDues()
        {
            // call function from dbContext
            return duesDbController.GetDuesUserDto();
        }

        [Authorize]
        [HttpGet("payments")]
        public List<DuesUserDto> GetDuesPaid()
        {
            return duesDbController.GetDuesUserDtoPaid();
        }

        [Authorize]
        [HttpGet("payments/debt")]
        public List<DuesUserDto> GetDuesNotPaid()
        {
            // call function from dbContext
            return duesDbController.GetDuesUserDtoNotPaid();
        }

        [Authorize]
        [HttpGet("{id}/dues")]
        public List<DuesHouseDto> GetBillHouseDto(int id)
        {
            return duesDbController.GetDuesHouseById(id);
        }
        [Authorize]
        [HttpGet("{id}/dues/payments")]
        public List<DuesHouseDto> GetBillHouseDtoPaid(int id)
        {
            return duesDbController.GetBillHouseDtoPaid(id);
        }

        [Authorize]
        //post endpoint
        [HttpPost]
        //add new dues to db
        public Result AddDues(Dues newDues)
        {
            Result _result = new Result();

            Dues? dues = duesDbController.FindDues(newDues.duesId);
            House? house = houseDBController.FindHouse(newDues.houseId);

            bool billCheck = (dues != null && house == null) ? true : false;

            if (billCheck)
            {
                _result.status = 0;
                _result.message = "dues is already exist or this house does not have a bill";
                logger.createLog(_result.message);
            }
            else
            {
                //can't exist newBill in list
                if (duesDbController.AddDuesModel(newDues))
                {
                    _result.status = 1;
                    _result.message = "Insert new dues in list";
                    _result.duesList = duesDbController.GetDues().ToList();
                }
                else
                {
                    _result.status = 0;
                    _result.message = "Don't dues new house";
                    _result.duesList = duesDbController.GetDues().ToList();
                }

            }
            return _result;
        }

        [Authorize]
        //put endpoint
        [HttpPut("{id}")]
        //update dues infromation
        public Result UpdateDues(int id, Dues updatedDues)
        {
            Result _result = new Result();

            var bill = duesDbController.FindDues(id);

            if (bill == null)
            {
                _result.status = 0;
                _result.message = "Not found this house";
                logger.createLog(_result.message);
            }
            else
            {
                if (duesDbController.DeleteDuesModel(id) && duesDbController.AddDuesModel(updatedDues))
                {
                    _result.status = 1;
                    _result.message = "Updated successfully.";
                    _result.duesList = duesDbController.GetDues().ToList();

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
        //delete dues from db
        public Result DeleteDues(int id)
        {
            Result _result = new Result();

            if (!duesDbController.DeleteDuesModel(id))
            {
                _result.status = 0;
                _result.message = "Delete failed";
            }
            else
            {
                //successfully deleted
                _result.status = 1;
                _result.message = "Deleted successfully";
                _result.duesList = duesDbController.GetDues().ToList();

            }
            return _result;
        }
    }
}
