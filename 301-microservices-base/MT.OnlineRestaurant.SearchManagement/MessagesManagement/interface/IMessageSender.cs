using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessagesManagement
{
 public interface IMessageSender
    {
     Task SendMessagesAsync(Object obj, string TopicName);
}

}
