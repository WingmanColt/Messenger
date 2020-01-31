namespace HireMe.ViewModels.Message
{
using HireMe.Models;
using HireMe.Mapping;
using System;

    public class MessageViewModel : IMapFrom<Message>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime dateTime { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public bool isRead { get; set; }
        public bool isImportant { get; set; }
        public bool isStared { get; set; }

        public bool deletedFromSender { get; set; }
        public bool deletedFromReceiver { get; set; }


    }
    
}