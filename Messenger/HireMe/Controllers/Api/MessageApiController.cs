using HireMe.Data;
using HireMe.Models.Enums;
using HireMe.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace HireMe.Controllers.Api
{
    public class MessageApiController : BaseController
    {
        private readonly BaseDbContext _contextBase;
        private readonly FeaturesDbContext _contextFeatures;
        private readonly IMessageService _messageService;
        private readonly IConfiguration _config;

        public MessageApiController(
            BaseDbContext contextBase, 
            FeaturesDbContext contextFeatures, 
            IMessageService messageService,
            IConfiguration config)
        {
            this._messageService = messageService;
            this._config = config;

            this._contextBase = contextBase ?? throw new ArgumentNullException(nameof(contextBase));
            this._contextFeatures = contextFeatures ?? throw new ArgumentNullException(nameof(contextFeatures));
        }

       

        [Produces("application/json")]
        public JsonResult Search()
        {
                string term = HttpContext.Request.Query["term"].ToString();
                var url = _config.GetSection("MySettings").GetSection("SiteUrl").Value;

                var nameData = _contextBase.Users
                     .Where(X => X.FirstName.Contains(term))
                     .Select(x => new { 
                         id = x.Id, 
                         firstname = x.FirstName,
                         lastname = x.LastName, 
                         picture = x.PictureName,
                         siteurl = url
                     })
                     .ToArray();

                return Json(nameData);
        }

        [Produces("application/json")]
        public JsonResult SearchMessage()
        {
            string term = HttpContext.Request.Query["term"].ToString();
            var nameData = _contextFeatures.Message
                 .Where(X => X.Title.Contains(term))
                 .Select(x => new { id = x.Id, title = x.Title })
                 .ToArray();

            return Json(nameData);
        }


        public IActionResult AddToStared(int id)
        {
            var msg = this._messageService.GetByIdMessage(id);

            if (this.ModelState.IsValid)
            {
                var result = this._messageService.Add_MessageState(id, MessageStates.Stared, msg.isStared ? false : true);

                if (result.Success)
                {
                    return Redirect($"/identity/messenger/index");
                }
            }
            return Redirect($"/identity/messenger/index");
        }
        public IActionResult AddToImportant(int id)
        {
            var msg = this._messageService.GetByIdMessage(id);

            if (this.ModelState.IsValid)
            {
                var result = this._messageService.Add_MessageState(id, MessageStates.Important, msg.isImportant ? false : true);

                if (result.Success)
                {
                    return Redirect($"/identity/messenger/index");
                }
            }
            return Redirect($"/identity/messenger/index");
        }

        public IActionResult AddToReaded(int id)
        {
            var msg = this._messageService.GetByIdMessage(id);

            if (this.ModelState.IsValid)
            {
                var result = this._messageService.Add_MessageState(id, MessageStates.Read, true);

                if (result.Success)
                {
                    return Redirect($"/identity/messenger/messageview/{id}");
                }
            }
            return Redirect($"/identity/messenger/messageview/{id}");
        }

    }
}
