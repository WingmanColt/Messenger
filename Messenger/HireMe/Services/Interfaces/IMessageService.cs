namespace HireMe.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HireMe.ExeptionHelper;
    using HireMe.Models;
    using HireMe.Models.Enums;
    using HireMe.ViewModels.Message;

    public interface IMessageService
    { 
        Task<OperationResult> Create(string title, string description, string senderid, string receiverid);

        OperationResult Delete(int id, string userId);

        IEnumerable<MessageViewModel> GetAllMessagesBy_Sender(string senderId, int count);

        IEnumerable<MessageViewModel> GetAllMessagesBy_Receiver(string receiverId, int count);

        TViewModel GetMessageById<TViewModel>(int id);

        Message GetByIdMessage(int id);

        OperationResult Add_MessageState(int msgId, MessageStates msgState, bool type);

        int GetMessagesCountBy_Sender(string Id);

        int GetMessagesCountBy_Receiver(string Id);

        int GetMessagesUnreadBy_Receiver(string Id);


    }
}
