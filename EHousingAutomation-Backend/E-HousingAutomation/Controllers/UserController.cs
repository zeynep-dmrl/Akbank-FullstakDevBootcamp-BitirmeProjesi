using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_HousingAutomation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        Logger logger = new Logger(); //log operation

        UserDBController userDBController = new UserDBController(); //db connection


        //METHODS

        [Authorize]
        //get endpoint
        [HttpGet]
        public List<User> GetUsers()
        {
            // call function from dbContext
            return userDBController.GetUsers();
        }

        [Authorize]
        //get user by id
        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            if (userDBController.FindUser(id) == null)
            {
                logger.createLog("User (" + id + ") is not found");
                return null;
            }
            else
            {
                User? user = userDBController.GetUserById(id);
                return user;
            }
        }

        //[Authorize]
        //post endpoint
        [HttpPost]
        //add new User to db
        public Result AddUser(User newUser)
        {
            Result _result = new Result();

            User? user = userDBController.FindUser(newUser.userId, newUser.tcNo);
            bool userCheck = (user != null) ? true : false;

            if (userCheck)
            {
                _result.status = 0;
                _result.message = "User is already exist.";
                logger.createLog(_result.message);
            }
            else
            {
                //can't exist newUser in list
                if (userDBController.AddUserModel(newUser))
                {
                    _result.status = 1;
                    _result.message = "Insert new User in list";
                    _result.userList = userDBController.GetUsers().ToList();
                }
                else
                {
                    _result.status = 0;
                    _result.message = "Don't insert new User";
                    _result.userList = userDBController.GetUsers().ToList();
                }

            }
            return _result;
        }

        [Authorize]
        //put endpoint
        [HttpPut("{id}")]
        //update User infromation
        public Result UpdateUser(int id, User updatedUser)
        {
            Result _result = new Result();

            var user = userDBController.FindUser(id, updatedUser.tcNo);

            if (user == null)
            {
                _result.status = 0;
                _result.message = "Not found this User";
                logger.createLog(_result.message);
            }
            else
            {
                if (userDBController.DeleteUserModel(id) && userDBController.AddUserModel(updatedUser))
                {
                    _result.status = 1;
                    _result.message = "Updated successfully.";
                    _result.userList = userDBController.GetUsers().ToList();

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
        //delete user from db
        public Result DeleteUser(int id)
        {
            Result _result = new Result();

            if (!userDBController.DeleteUserModel(id))
            {
                _result.status = 0;
                _result.message = "Delete failed";
            }
            else
            {
                //successfully deleted
                _result.status = 1;
                _result.message = "Deleted successfully";
                _result.userList = userDBController.GetUsers().ToList();

            }
            return _result;
        }
    }
}
