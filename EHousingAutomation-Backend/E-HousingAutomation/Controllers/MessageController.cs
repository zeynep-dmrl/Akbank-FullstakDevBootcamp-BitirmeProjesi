using DAL.Dto;
using DAL.Model;
using E_HousingAutomation.Controllers.DBControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_HousingAutomation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        Logger logger = new Logger(); //log operation

        MessageDBController messageDbController = new MessageDBController();


        //METHODS

        [Authorize]
        //get endpoint
        [HttpGet]
        public List<Message> GetMessages()
        {
            // call function from dbContext
            return messageDbController.GetMessages();
        }

        [HttpGet("messages")]
        public List<MessageUserDto> GetMessageUser()
        {
            return messageDbController.GetMessageUser();
        }

        [Authorize]
        [HttpGet("messages/unread")]
        public List<Message> GetUnreadMessages()
        {
            return messageDbController.GetUnreadMessages();
        }

        [Authorize]
        [HttpGet("messages/read")]
        public List<Message> GetReadMessages()
        {
            return messageDbController.GetReadMessages();
        }

        [Authorize]
        //get message by id
        [HttpGet("{id}")]
        public List<Message> GetMessagesById(int id)
        {
            if (messageDbController.FindMessage(id) == null)
            {
                logger.createLog("Messages (" + id + ") is not found");
                return null;
            }
            else
            {
                return messageDbController.GetMessageById(id);
            }
        }

        [Authorize]
        [HttpGet("{id}/messages")]
        public List<MessageUserDto> GetMessageUserById(int id)
        {
            if (messageDbController.FindMessage(id) == null)
            {
                logger.createLog("Messages (" + id + ") is not found");
                return null;
            }
            else
            {
                return messageDbController.GetMessageUserById(id);
            }
        }

        [Authorize]
        //post endpoint
        [HttpPost]
        //add new message to db
        public Result AddMessage(Message newMessage)
        {
            Result _result = new Result();

            Message? message = messageDbController.FindMessage(newMessage.messageId);
            bool messageCheck = (message != null) ? true : false;

            if (messageCheck)
            {
                _result.status = 0;
                _result.message = "Message is already exist.";
                logger.createLog(_result.message);
            }
            else
            {
                //can't exist newBill in list
                if (messageDbController.AddMessageModel(newMessage))
                {
                    _result.status = 1;
                    _result.message = "Insert new message in list";
                    _result.messageList = messageDbController.GetMessages().ToList();
                }
                else
                {
                    _result.status = 0;
                    _result.message = "Don't send message";
                    _result.messageList = messageDbController.GetMessages().ToList();
                }

            }
            return _result;
        }

        [Authorize]
        //delete endpoint
        [HttpDelete("{id}")]
        //delete message from db
        public Result DeleteMessage(int id)
        {
            Result _result = new Result();

            if (!messageDbController.DeleteMessageModel(id))
            {
                _result.status = 0;
                _result.message = "Delete failed";
            }
            else
            {
                //successfully deleted
                _result.status = 1;
                _result.message = "Deleted successfully";
                _result.messageList = messageDbController.GetMessages().ToList();

            }
            return _result;
        }
    }
}
