using DAL.Dto;
using DAL.Model;
using Entities;

namespace E_HousingAutomation.Controllers
{
    public class DuesDBController
    {
        private DBContext _duesContext = new DBContext();

        HouseDBController houseDBController = new HouseDBController();

        Logger _logger = new Logger();

        #region Dues Func..
        //for HTTPPost operation
        public bool AddDuesModel(Dues _dues)
        {
            try
            {
                _duesContext.Dues.Add(_dues);
                _duesContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.createLog("Error: " + ex.Message);
                return false;
            }
        }
        //find dues from Dues table by id
        public Dues? FindDues(int _id = 0)
        {
            Dues? dues = new Dues();
            dues = _duesContext.Dues.FirstOrDefault(d => d.duesId == _id);
            return dues;
        }

        //for HTTPGet operations

        //fetch all dues from database
        public List<Dues> GetDues()
        {
            List<Dues> dues = new List<Dues>();
            dues = _duesContext.Dues.OrderBy(d => d.monthly).ToList();
            _logger.createLog("All dues that are in list fetched sorted by their ids.");
            return dues;

        }
        public List<Dues> GetDuesPaid()
        {
            List<Dues> dues = new List<Dues>();
            dues = _duesContext.Dues.Where(d => d.isPaid == true).ToList();
            _logger.createLog("All dues that are in list fetched.");
            return dues;

        }
        public List<Dues> GetDuesNotPaid()
        {
            List<Dues> dues = new List<Dues>();
            dues = _duesContext.Dues.Where(d => d.isPaid == false).ToList();
            _logger.createLog("All dues that are in list fetched");
            return dues;

        }
        //fetch one dues from db
        public Dues? GetDuesById(int _id)
        {
            _logger.createLog("Dues (" + _id + ")  was fetched from the db");
            return _duesContext.Dues.Where(d => d.duesId == _id).SingleOrDefault();
        }

        //for HTTPDelete operation

        //delete the dues from db
        public bool DeleteDuesModel(int _id)
        {
            try
            {
                _duesContext.Dues.Remove(FindDues(_id));
                _duesContext.SaveChanges();
                _logger.createLog("Delete dues from database");
                return true;
            }
            catch (Exception ex)
            {
                _logger.createLog("Error: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region Dto Func...

        public List<DuesUserDto> GetDuesUserDto()
        {
            List<DuesUserDto> dues = new List<DuesUserDto>();
            dues = _duesContext.Dues.Join(_duesContext.House,
                                              d => d.houseId,
                                              h => h.HouseID,
                                              (dues, house) => new { dues, house }).Join
                                              (_duesContext.User,
                                              dh => dh.house.userId,
                                              u => u.userId,
                                              (dto, user) =>
                                              new DuesUserDto
                                              {
                                                  user = (user.firstName + " " + user.lastName),
                                                  price = dto.dues.price,
                                                  monthly = dto.dues.monthly,
                                                  duesDesc = dto.dues.duesDesc,
                                                  isPaid = dto.dues.isPaid
                                              }
                                              ).ToList();
            _logger.createLog("All duess that are in list fetched.");
            return dues;
        }

        public List<DuesHouseDto> GetDuesHouseById(int _houseid)
        {
            if (houseDBController.FindHouse(_houseid) == null)
            {
                _logger.createLog("House (" + _houseid + ") is not found");
                return null;
            }
            else
            {
                _logger.createLog("Dues was fetched from the db");
                return _duesContext.Dues.Where(d => d.houseId == _houseid).Join(_duesContext.House,
                                              d => d.houseId,
                                              h => h.HouseID,
                                              (dues, house) => new DuesHouseDto
                                              {
                                                  price = dues.price,
                                                  monthly = dues.monthly,
                                                  duesDesc = dues.duesDesc,
                                                  isPaid = dues.isPaid
                                              }
                                              ).ToList();
            }
        }
        public List<DuesHouseDto> GetBillHouseDtoPaid(int _houseId)
        {
            List<DuesHouseDto> dues = new List<DuesHouseDto>();
            dues = _duesContext.Dues.Where(d => d.houseId == _houseId).Join(_duesContext.House,
                                              d => d.houseId,
                                              h => h.HouseID,
                                              (dues, house) => new DuesHouseDto
                                              {
                                                  price = dues.price,
                                                  monthly = dues.monthly,
                                                  duesDesc = dues.duesDesc,
                                                  isPaid = dues.isPaid
                                              }
                                              ).Where(d => d.isPaid == true).ToList();
            _logger.createLog("All duess that are in list fetched.");
            return dues;
        }

        public List<DuesUserDto> GetDuesUserDtoPaid()
        {
            List<DuesUserDto> dues = new List<DuesUserDto>();
            dues= _duesContext.Dues.Join(_duesContext.House,
                                              d => d.houseId,
                                              h => h.HouseID,
                                              (dues, house) => new { dues, house }).Join
                                              (_duesContext.User,
                                              dh => dh.house.userId,
                                              u => u.userId,
                                              (dto, user) =>
                                              new DuesUserDto
                                              {
                                                  user = (user.firstName + " " + user.lastName),
                                                  price = dto.dues.price,
                                                  monthly = dto.dues.monthly,
                                                  duesDesc = dto.dues.duesDesc,
                                                  isPaid = dto.dues.isPaid
                                              }
                                              ).Where(d => d.isPaid == true).ToList();
            _logger.createLog("All duess that are in list fetched.");
            return dues;
        }
        public List<DuesUserDto> GetDuesUserDtoNotPaid()
        {
            List<DuesUserDto> dues = new List<DuesUserDto>();
            dues = _duesContext.Dues.Join(_duesContext.House,
                                              d => d.houseId,
                                              h => h.HouseID,
                                              (dues, house) => new { dues, house }).Join
                                              (_duesContext.User,
                                              dh => dh.house.userId,
                                              u => u.userId,
                                              (dto, user) =>
                                              new DuesUserDto
                                              {
                                                  user = (user.firstName + " " + user.lastName),
                                                  price = dto.dues.price,
                                                  monthly = dto.dues.monthly,
                                                  duesDesc = dto.dues.duesDesc,
                                                  isPaid = dto.dues.isPaid
                                              }
                                              ).Where(d => d.isPaid == false).ToList();
            _logger.createLog("All duess that are in list fetched.");
            return dues;
        }

        #endregion
    }
}
