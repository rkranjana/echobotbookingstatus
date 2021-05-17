// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.13.1

using EchoBotDBbookingstatus.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace EchoBotDBbookingstatus.Bots
{
    public class EchoBot : ActivityHandler
    {
        BookingstatusDBContext context;
        public BookingstatusDBContext Context { get { return context; } }
        public EchoBot()
        {
            context = new BookingstatusDBContext();
        }

        public Bookingdetail Fetchstatus(string no)
        {
            Bookingdetail statusof;
            try
            {
                statusof = (from e in Context.Bookingdetails
                            where e.Bookingid == no
                            select e).FirstOrDefault();//Query for booking details with id
            }
            catch (Exception)
            {
                statusof = null;
            }
            return statusof;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var bookNumber = turnContext.Activity.Text;
            Bookingdetail statusof = Fetchstatus(bookNumber);

            if (statusof == null)
            {
                var reply = "Sorry!! You entered an invalid booking number";
                await turnContext.SendActivityAsync(MessageFactory.Text(reply, reply), cancellationToken);
                var promptmessage = "Enter a valid booking ID ???";
                await turnContext.SendActivityAsync(MessageFactory.Text(promptmessage, promptmessage), cancellationToken);

            }

            else
            { 
                var replyid = "Your booking ID  :"+ statusof.Bookingid;
                await turnContext.SendActivityAsync(MessageFactory.Text(replyid, replyid), cancellationToken);
                var replydate = "Your Travel Date  :" + statusof.Date; 
                await turnContext.SendActivityAsync(MessageFactory.Text(replydate, replydate), cancellationToken);
                var replyfrom = "You are travelling from  :  " + statusof.From;
                await turnContext.SendActivityAsync(MessageFactory.Text(replyfrom, replyfrom), cancellationToken);
                var replyto = "You are travelling to  :  " + statusof.Venue;
                await turnContext.SendActivityAsync(MessageFactory.Text(replyto, replyto), cancellationToken);
                var replystatus = "Your booking status  :  " + statusof.Status;
                await turnContext.SendActivityAsync(MessageFactory.Text(replystatus, replystatus), cancellationToken);
                var promptmessage = "Enter booking ID to check another status???";
                await turnContext.SendActivityAsync(MessageFactory.Text(promptmessage, promptmessage), cancellationToken);



            }
        }
        
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Welcome to Indian Airlines!!!";
            var welcome = "Kindly enter your booking ID to check your booking status";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcome, welcome), cancellationToken);
                }
            }
        }
    }
}
