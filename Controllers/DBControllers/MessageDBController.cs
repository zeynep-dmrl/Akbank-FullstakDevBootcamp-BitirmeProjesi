using DAL.Dto;
using DAL.Model;
using Entities;

namespace E_HousingAutomation.Controllers.DBControllers
{
    public class MessageDBController
    {
        private DBContext _messageContext = new DBContext();


        Logger _logger = new Logger();

        #region Message Func..
        //for HTTPPost operation
        public bool AddMessageModel(Message _message)
        {
            try
            {
                _messageContext.Message.Add(_message);
                _messageContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.createLog("Error: " + ex.Message);
                return false;
            }
        }
        //find message from Message table by id
        public Message? FindMessage(int _id = 0, int userId = 0)
        {
            Message? message = new Message();
            if (userId > 0)
            {
                message = _messageContext.Message.FirstOrDefault(m => m.userId == userId);
            }
            else if (_id > 0)
            {
                message = _messageContext.Message.FirstOrDefault(m => m.messageId == _id);
            }
            return message;
        }

        //for HTTPGet operations

        //fetch all message from database
        public List<Message> GetMessages()
        {
            List<Message> messages = new List<Message>();
            messages = _messageContext.Message.OrderBy(m => m.messageId).ToList();
            _logger.createLog("All bills that are in list fetched sorted by their ids.");
            return messages;
        }

        //fetch all unread message from database
        public List<Message> GetUnreadMessages()
        {
            List<Message> messages = new List<Message>();
            messages = _messageContext.Message.Where(m => m.isRead == false).ToList();
            _logger.createLog("All bills that are in list fetched sorted by their ids.");
            return messages;
        }

        //fetch all message that is reading from database
        public List<Message> GetReadMessages()
        {
            List<Message> messages = new List<Message>();
            messages = _messageContext.Message.Where(m => m.isRead == true).ToList();
            _logger.createLog("All bills that are in list fetched sorted by their ids.");
            return messages;
        }

        //fetch all message from database
        public List<Message> GetMessageById(int _id)
        {
            List<Message> messages = new List<Message>();
            messages = _messageContext.Message.Where(m => m.messageId == _id).ToList();
            _logger.createLog("All bills that are in list fetched sorted by their ids.");
            return messages;
        }

        //for HTTPDelete operation

        //delete the message from db
        public bool DeleteMessageModel(int _id)
        {
            try
            {
                _messageContext.Message.Remove(FindMessage(_id));
                _messageContext.SaveChanges();
                _logger.createLog("Delete message from database");
                return true;
            }
            catch (Exception ex)
            {
                _logger.createLog("Error: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region Dto FUNC...

        public List<MessageUserDto> GetMessageUser()
        {
            List<MessageUserDto> messages = new List<MessageUserDto>();
            messages = _messageContext.Message.OrderBy(m => m.timestamp).Join(_messageContext.User,
                                              m => m.userId,
                                              u => u.userId,
                                              (message, user) => new MessageUserDto
                                              {
                                                  message = message.message,
                                                  isRead = message.isRead,
                                                  user = (user.firstName+" "+ user.lastName)
                                              }
                                              ).ToList();
            return messages;
        }

        public List<MessageUserDto> GetMessageUserById(int _userId)
        {
            List<MessageUserDto> messages = new List<MessageUserDto>();
            messages = _messageContext.Message.Where(m => m.userId == _userId).Join(_messageContext.User,
                                              m => m.userId,
                                              u => u.userId,
                                              (message, user) => new MessageUserDto
                                              {
                                                  message = message.message,
                                                  isRead = message.isRead,
                                                  user = (user.firstName + " " + user.lastName)
                                              }
                                              ).ToList();
            return messages;
        }
        #endregion

    }
}
