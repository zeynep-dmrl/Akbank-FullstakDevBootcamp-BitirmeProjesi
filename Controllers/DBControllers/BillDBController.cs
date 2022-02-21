using DAL.Dto;
using DAL.Model;
using Entities;

namespace E_HousingAutomation.Controllers
{
    public class BillDBController
    {
        private DBContext _billContext = new DBContext();

        HouseDBController houseDBController = new HouseDBController();

        Logger _logger = new Logger();

        //for HTTPPost operation
        public bool AddBillModel(Bill _bill)
        {
            try
            {
                _billContext.Bill.Add(_bill);
                _billContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.createLog("Error: " + ex.Message);
                return false;
            }
        }
        //find bill from Bill table by id
        public Bill? FindBill(int _id = 0)
        {
            Bill? bill = new Bill();
            bill = _billContext.Bill.FirstOrDefault(b => b.billId == _id);
            return bill;
        }

        //for HTTPGet operations

        //fetch all bills from database
        public List<Bill> GetBills()
        {
            List<Bill> bills = new List<Bill>();
            bills = _billContext.Bill.OrderBy(b => b.monthly).ToList();
            _logger.createLog("All bills that are in list fetched sorted by their ids.");
            return bills;

        }
        public List<Bill> GetBillsPaid()
        {
            List<Bill> bills = new List<Bill>();
            bills = _billContext.Bill.Where(b => b.isPaid == true).ToList();
            _logger.createLog("All bills that are in list fetched.");
            return bills;

        }
        public List<Bill> GetBillsNotPaid()
        {
            List<Bill> bills = new List<Bill>();
            bills = _billContext.Bill.Where(b => b.isPaid == false).ToList();
            _logger.createLog("All bills that are in list fetched.");
            return bills;

        }
        //fetch one house from db
        public Bill? GetBillById(int _id)
        {
            _logger.createLog("Bill (" + _id + ")  was fetched from the db");
            return _billContext.Bill.Where(b => b.billId == _id).SingleOrDefault();
        }

        //for HTTPDelete operation

        //delete the bill from db
        public bool DeleteBillModel(int _id)
        {
            try
            {
                _billContext.Bill.Remove(FindBill(_id));
                _billContext.SaveChanges();
                _logger.createLog("Delete bill from database");
                return true;
            }
            catch (Exception ex)
            {
                _logger.createLog("Error: " + ex.Message);
                return false;
            }
        }

        #region Dto Func..

        public List<BillUserDto> GetBillUserDto()
        {
            List<BillUserDto> bills = new List<BillUserDto>();
            bills = _billContext.Bill.Join(_billContext.House,
                                              b => b.houseId,
                                              h => h.HouseID,
                                              (bill, house) => new { bill, house }).Join
                                              (_billContext.User,
                                              hb => hb.house.userId,
                                              u => u.userId,
                                              (dto, user) =>
                                              new BillUserDto
                                              {
                                                  user = (user.firstName + " " + user.lastName),
                                                  price = dto.bill.price,
                                                  monthly = dto.bill.monthly,
                                                  billDesc = dto.bill.billDesc,
                                                  isPaid = dto.bill.isPaid
                                              }
                                              ).ToList();
            _logger.createLog("All bills that are in list fetched.");
            return bills;
        }


        public List<BillHouseDto> GetBillHouseById(int _houseid)
        {
            if (houseDBController.FindHouse(_houseid) == null)
            {
                _logger.createLog("House (" + _houseid + ") is not found");
                return null;
            }
            else
            {
                _logger.createLog("Bills was fetched from the db");
                return _billContext.Bill.Where(b => b.houseId == _houseid).Join(_billContext.House,
                                              b => b.houseId,
                                              h => h.HouseID,
                                              (bill, house) => new BillHouseDto
                                              {
                                                  price = bill.price,
                                                  monthly = bill.monthly,
                                                  billDesc = bill.billDesc,
                                                  isPaid = bill.isPaid
                                              }
                                              ).ToList();
            }
        }
        public List<BillHouseDto> GetBillHouseDtoPaid(int _houseId)
        {
            List<BillHouseDto> bills = new List<BillHouseDto>();
            bills = _billContext.Bill.Where(b => b.houseId == _houseId).Join(_billContext.House,
                                              b => b.houseId,
                                              h => h.HouseID,
                                              (bill, house) => new BillHouseDto
                                              {
                                                  price = bill.price,
                                                  monthly = bill.monthly,
                                                  billDesc = bill.billDesc,
                                                  isPaid = bill.isPaid
                                              }).Where(b => b.isPaid == true).ToList();
            _logger.createLog("All bills that are in list fetched.");
            return bills;
        }
        public List<BillUserDto> GetBillUserDtoPaid()
        {
            List<BillUserDto> bills = new List<BillUserDto>();
            bills = _billContext.Bill.Join(_billContext.House,
                                              b => b.houseId,
                                              h => h.HouseID,
                                              (bill, house) => new {bill , house}).Join
                                              (_billContext.User,
                                              hb => hb.house.userId,
                                              u => u.userId,
                                              (dto, user) =>
                                              new BillUserDto
                                              {
                                                  user = (user.firstName + " " + user.lastName),
                                                  price = dto.bill.price,
                                                  monthly = dto.bill.monthly,
                                                  billDesc = dto.bill.billDesc,
                                                  isPaid = dto.bill.isPaid
                                              }
                                              ).Where(b => b.isPaid == true).ToList();
            _logger.createLog("All bills that are in list fetched.");
            return bills;
        }
        public List<BillHouseDto> GetBillHouseDtoNotPaid()
        {
            List<BillHouseDto> bills = new List<BillHouseDto>();
            bills = _billContext.Bill.Where(b => b.isPaid == true).Join(_billContext.House,
                                              b => b.houseId,
                                              h => h.HouseID,
                                              (bill, house) => new BillHouseDto
                                              {
                                                  price = bill.price,
                                                  monthly = bill.monthly,
                                                  billDesc = bill.billDesc,
                                                  isPaid = bill.isPaid
                                              }
                                              ).ToList();
            _logger.createLog("All paid bills that are in list fetched.");
            return bills;
        }
        public List<BillUserDto> GetBillUserDtoNotPaid()
        {
            List<BillUserDto> bills = new List<BillUserDto>();
            bills = _billContext.Bill.Join(_billContext.House,
                                              b => b.houseId,
                                              h => h.HouseID,
                                              (bill, house) => new { bill, house }).Join
                                              (_billContext.User,
                                              hb => hb.house.userId,
                                              u => u.userId,
                                              (dto, user) =>
                                              new BillUserDto
                                              {
                                                  user = (user.firstName + " " + user.lastName),
                                                  price = dto.bill.price,
                                                  monthly = dto.bill.monthly,
                                                  billDesc = dto.bill.billDesc,
                                                  isPaid = dto.bill.isPaid
                                              }
                                              ).Where(b => b.isPaid == false).ToList();
            _logger.createLog("All debts bills that are in list fetched.");
            return bills;
        }

        #endregion
    }
}
