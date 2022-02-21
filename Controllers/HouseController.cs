using DAL.Dto;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_HousingAutomation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HouseController : ControllerBase
    {
        Logger logger = new Logger(); //log operation

        HouseDBController houseDBController = new HouseDBController(); //db connection


        //METHODS

        //get endpoint
        [HttpGet]
        public List<HouseUserDto> GetHouseUsers()
        {
            return houseDBController.GetHouseUser();
        }

        [HttpGet("find")]
        public int FindHouseFormBill(string block, int floor, int aptNo)
        {
            return houseDBController.FindHouseFromBill(block, floor, aptNo);
        }

        [Authorize]
        //get house by id
        [HttpGet("{id}")]
        public House GetHouse(int id)
        {
            if(houseDBController.FindHouse(id) == null)
            {
                logger.createLog("House (" + id + ") is not found");
                return null;
            }
            else
            {
                House? house = houseDBController.GetHouseById(id);
                return house;
            }
        }

        [Authorize]
        //post endpoint
        [HttpPost]
        //add new house to db
        public Result AddHouse(House newHouse)
        {
            Result _result = new Result();

            House? house = houseDBController.FindHouse(newHouse.HouseID);
            bool houseCheck = (house != null) ? true : false;

            if (houseCheck)
            {
                _result.status = 0;
                _result.message = "House is already exist.";
                logger.createLog(_result.message);
            }
            else
            {
                //can't exist newHouse in list
                if (houseDBController.AddHouseModel(newHouse))
                {
                    _result.status = 1;
                    _result.message = "Insert new house in list";
                    _result.houseList = houseDBController.GetHouses().ToList();
                }
                else
                {
                    _result.status = 0;
                    _result.message = "Don't insert new house";
                    _result.houseList = houseDBController.GetHouses().ToList();
                }

            }
            return _result;
        }

        [Authorize]
        //put endpoint
        [HttpPut("{id}")]
        //update house infromation
        public Result UpdateHouse(int id, House updatedHouse)
        {
            Result _result = new Result();

            var house = houseDBController.FindHouse(id);

            if(house == null)
            {
                _result.status = 0;
                _result.message = "Not found this house";
                logger.createLog(_result.message);
            }
            else
            {
                if(houseDBController.DeleteHouseModel(id) && houseDBController.AddHouseModel(updatedHouse))
                {
                    _result.status = 1;
                    _result.message = "Updated successfully.";
                    _result.houseList = houseDBController.GetHouses().ToList();

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
        //delete house from db
        public Result DeleteHouse(int id)
        {
            Result _result = new Result();

            if (!houseDBController.DeleteHouseModel(id))
            {
                _result.status = 0;
                _result.message = "Delete failed";
            }
            else
            {
                //successfully deleted
                _result.status = 1;
                _result.message = "Deleted successfully";
                _result.houseList = houseDBController.GetHouses().ToList();

            }
            return _result;
        }

        


    }
}
