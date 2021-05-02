using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Test.SignalR.Hubs;

namespace Test.SignalR.Controllers
{
    [ApiController]
    [Route("Api/Message")]
    public class WebsController : Controller
    {
        protected readonly IHubContext<MessageHub> _messageHub;
        public WebsController([NotNull]IHubContext<MessageHub> messageHub)
        {
            _messageHub = messageHub;
        }

        [HttpPost]
        public async Task<ResponseModel> Create(MessagePost messagePost)
        {
            ResponseModel ret = new ResponseModel
            {
                Data = new MessagePost
                {
                    User = messagePost.User,
                    Message = messagePost.Message
                }
            };
            await _messageHub.Clients.All.SendAsync("sendToReact", ret.Data);

            return ret;
        }
    }

    public class MessagePost
    {
        public virtual string User { get; set; }
        public virtual string Message { get; set; }
    }
    public class ResponseModel
    {
        public virtual MessagePost Data { get; set; }
    }
}
