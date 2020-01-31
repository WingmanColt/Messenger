using HireMe.Data.Repository.Interfaces;
using HireMe.ExeptionHelper;
using HireMe.Services.Interfaces;
using HireMe.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using HireMe.ViewModels.Message;
using HireMe.Mapping;
using Microsoft.Extensions.Logging;
using System;
using HireMe.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HireMe.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> messageRepository;
        private readonly ILogger _logger;
        public MessageService(IRepository<Message> messageRepository, ILogger<Message> logger)
        {
            this.messageRepository = messageRepository;
            _logger = logger;
        }
        public async Task<OperationResult> Create(string title, string description, string senderid, string receiverid)
        {
            var message = new Message
            {
                Title = title,
                Description = description,
                dateTime = DateTime.Now,
                SenderId = senderid,
                ReceiverId = receiverid
            };

            await this.messageRepository.AddAsync(message);

            var success = await this.messageRepository.SaveChangesAsync() > 0;
            return success ? OperationResult.SuccessResult() : OperationResult.FailureResult("Message can't send, try again later or contact with support !");
        }
        public OperationResult Delete(int iD, string userid)
        {
            var msg = this.messageRepository.GetById(iD); //.All().FirstOrDefault(j => j.Id == iD);

            if (msg.ReceiverId == userid && !msg.deletedFromReceiver)
            {
                msg.deletedFromReceiver = true;
                this.messageRepository.SaveChanges();
            }

            if (msg.SenderId == userid && !msg.deletedFromSender)
            {
                msg.deletedFromSender = true;
                this.messageRepository.SaveChanges();
            }

            if(msg.deletedFromReceiver && msg.deletedFromSender)
            {
                this.messageRepository.Delete(msg);
                this.messageRepository.SaveChanges();
            }
            var success = this.messageRepository.SaveChanges() > 0;
            return success ? OperationResult.SuccessResult() : OperationResult.FailureResult("Message can't delete, try again later or contact with support !");
        }
       
        public IEnumerable<MessageViewModel> GetAllMessagesBy_Sender(string senderId, int count)
        {
            var messages = this.messageRepository
                    .All()
                    .Where(j => j.SenderId == senderId)
                    .To<MessageViewModel>()
                    .Take(count)
                    .ToList();

            return messages;
        }
        
        public IEnumerable<MessageViewModel> GetAllMessagesBy_Receiver(string receiverId, int count)
        {
            var messages = this.messageRepository
                    .All()
                    .Where(j => j.ReceiverId == receiverId)
                    .To<MessageViewModel>()
                    .Take(count)
                    .ToList();

            return messages;
        }

        /* public IEnumerable<MessageViewModel> GetAllMessagesBy_Receiver2(string receiverId, int currentPage, int Skip, int Take)
         {
             CurrentPage = currentPage == 0 ? 1 : currentPage;

             Count = this.messageRepository.All()
               .Where(j => j.ReceiverId == receiverId)
               .Count();

             if (CurrentPage > TotalPages)
                 CurrentPage = TotalPages;

                 var messageList = this.messageRepository.All()
                 .Where(j => j.ReceiverId == receiverId)
                 .Skip((CurrentPage - 1) * PageSize)
                 .Take(PageSize)
                 .To<MessageViewModel>()
                 .ToList();


             return messageList;
         }*/
        public Message GetByIdMessage(int id)
        {
            return this.messageRepository.GetById(id);
        }

        public OperationResult Add_MessageState(int msgId, MessageStates msgState, bool type)
        {
            var msg = this.messageRepository.All().FirstOrDefault(j => j.Id == msgId);

                switch (msgState)
                {
                    case MessageStates.Read:
                        msg.isRead = type;
                        break;
                    case MessageStates.Stared:
                        msg.isStared = type;
                        break;
                    case MessageStates.Important:
                        msg.isImportant = type;
                        break;
                    default:
                        this.messageRepository.SaveChangesAsync();
                        break;
                }
            
            var success = this.messageRepository.SaveChanges() > 0;
            return success ? OperationResult.SuccessResult() : OperationResult.FailureResult("Message can't stared, try again later or contact with support !");
        }
        public TViewModel GetMessageById<TViewModel>(int id)
        {
            var msg = this.messageRepository.All().Where(x => x.Id == id)
                .To<TViewModel>().FirstOrDefault();
            return msg;
        }
        public int GetMessagesCountBy_Sender(string Id)
        {
            int count = this.messageRepository.All()
                        .Where(j => j.SenderId == Id).Count();
            return count;
        }

        public int GetMessagesCountBy_Receiver(string Id)
        {
            int count = this.messageRepository.All()
                        .Where(j => j.ReceiverId == Id).Count();
            return count;
        }

        public int GetMessagesUnreadBy_Receiver(string Id)
        {
            int count = this.messageRepository.All()
                        .Where(j => j.ReceiverId == Id && !j.isRead)
                        .Count();
            return count;
        }
    }
}