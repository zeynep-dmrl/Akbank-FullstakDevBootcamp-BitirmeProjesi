using DAL.Model;
using Entities;

namespace E_HousingAutomation.Controllers
{
    public class UserDBController
    {
        private DBContext _userContext = new DBContext();

        Logger _logger = new Logger();

        #region USER FUNC...
        //for HTTPPost operation
        public bool AddUserModel(User _user)
        {
            try
            {
                _userContext.User.Add(_user);
                _userContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.createLog("Error: " + ex.Message);
                return false;
            }
        }
        //find user from User table by id or tcNo
        public User? FindUser(int _id = 0, string _tcNo="")
        {
            User? user = new User();
            if (!string.IsNullOrEmpty(_tcNo))
            {
                user = _userContext.User.FirstOrDefault(u => u.tcNo == _tcNo);
            }
            else if (_id > 0)
            {
                user = _userContext.User.FirstOrDefault(u => u.userId  == _id);
            }
            return user;
        }

        public User? FindUserByEmail(int _id = 0, string _email = "")
        {
            User? user = new User();
            if (!string.IsNullOrEmpty(_email))
            {
                user = _userContext.User.FirstOrDefault(u => u.email == _email);
            }
            else if (_id > 0)
            {
                user = _userContext.User.FirstOrDefault(u => u.userId == _id);
            }
            return user;
        }

        //for HTTPGet operations

        //fetch all users from database
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            users = _userContext.User.OrderBy(u => u.userId).ToList();
            _logger.createLog("All users that are in list fetched sorted by their ids.");
            return users;

        }

        //fetch one user from db
        public User? GetUserById(int _id)
        {
            _logger.createLog("User (" + _id + ")  was fetched from the db");
            return _userContext.User.Where(u => u.userId == _id).SingleOrDefault();
        }

        //for HTTPDelete operation

        //delete the user from db
        public bool DeleteUserModel(int _id)
        {
            try
            {
                _userContext.User.Remove(FindUser(_id));
                _userContext.SaveChanges();
                _logger.createLog("Delete user from database");
                return true;
            }
            catch (Exception ex)
            {
                _logger.createLog("Error: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region TOKEN FUNC...

        public void CreateLogin(APIAuthority loginUser)
        {
            _userContext.APIAuthority.Add(loginUser);
            _userContext.SaveChanges();
        }

        public APIAuthority GetLogin(APIAuthority loginUser)
        {
            APIAuthority? user = new APIAuthority();
            if (!string.IsNullOrEmpty(loginUser.Email) && !string.IsNullOrEmpty(loginUser.Password))
            {
                user = _userContext.APIAuthority.FirstOrDefault(m => m.Email == loginUser.Email && m.Password == loginUser.Password);
            }

            return user;
        }


        #endregion
    }
}
