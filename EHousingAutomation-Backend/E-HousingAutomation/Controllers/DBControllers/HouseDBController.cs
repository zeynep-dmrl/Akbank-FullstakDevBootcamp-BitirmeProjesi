using Entities;
using DAL.Model;
using DAL.Dto;

namespace E_HousingAutomation.Controllers
{
    public class HouseDBController
    {
        private DBContext _houseContext = new DBContext();

        Logger _logger = new Logger();

        //for HTTPPost operation
        public bool AddHouseModel(House _house)
        {
            try
            {
                _houseContext.House.Add(_house);
                _houseContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.createLog("Error: " + ex.Message);
                return false;
            }
        }
        //find house from House table by id
        public House? FindHouse(int _id = 0)
        {
            House? house = new House();
            house = _houseContext.House.FirstOrDefault(h => h.HouseID == _id);
            return house;
        }

        public int FindHouseFromBill(string _block ="", int _floor = 0, int _aptNo = 0)
        {
            House? house = new House();
            house = _houseContext.House.Where(h=> h.block == _block && h.floor == _floor && h.aptNo == _aptNo).SingleOrDefault();
            if(house == null)
            {
                return 0;
            }
            else 
            {
                return house.HouseID;
            }
            
        }
            
        //for HTTPGet operations

        //fetch all houses from database
        public List<House> GetHouses()
        {
            List<House> houses = new List<House>();
            houses = _houseContext.House.OrderBy(h => h.HouseID).ToList();
            _logger.createLog("All houses that are in list fetched sorted by their ids.");
            return houses;
        }

        //fetch one house from db
        public House? GetHouseById(int _id)
        {
            _logger.createLog("House ("+ _id + ")  was fetched from the db");
            return _houseContext.House.Where(h=> h.HouseID==_id).SingleOrDefault();
        }

        //for HTTPDelete operation

        //delete the house from db
        public bool DeleteHouseModel(int _id)
        {
            try
            {
                _houseContext.House.Remove(FindHouse(_id));
                _houseContext.SaveChanges();
                _logger.createLog("Delete house from database");
                return true;
            }
            catch (Exception ex)
            {
                _logger.createLog("Error: "+ ex.Message);
                return false;
            }
        }

        #region DTO FUNC..

        public List<HouseUserDto> GetHouseUser()
        {
            List<HouseUserDto> houses = new List<HouseUserDto>();
            houses = _houseContext.House.Join(_houseContext.User,
                                              h => h.userId,
                                              u => u.userId,
                                              (house, user) => new HouseUserDto 
                                              {
                                                  Id = house.HouseID,
                                                  block = house.block,
                                                  floor = house.floor,
                                                  aptNo = house.aptNo,
                                                  isFull = house.isFull, 
                                                  ownerOrtenant = (user.firstName + user.lastName)
                                              }
                                              ).ToList();
            return houses;
        }

        public HouseUserDto? GetHouseUserById(int _houseid)
        {
            _logger.createLog("House (" + _houseid + ")  was fetched from the db");
            return _houseContext.House.Where(h => h.HouseID == _houseid).Join(_houseContext.User,
                                              h => h.userId,
                                              u => u.userId,
                                              (house, user) => new HouseUserDto
                                              {
                                                  block = house.block,
                                                  floor = house.floor,
                                                  aptNo = house.aptNo,
                                                  isFull = house.isFull,
                                                  ownerOrtenant = (user.firstName+" "+ user.lastName)
                                              }
                                              ).SingleOrDefault();
        }
        #endregion
    }
}
