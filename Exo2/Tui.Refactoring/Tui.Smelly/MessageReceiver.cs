using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tui.Smelly
{
    class MessageReceiver
    {
        public void Receive(IMessage message)
        {
            //the code is smelly because Message receiver is handling each message type's logic...
            //this message's logic is none of the business of MessageReceiver
            //consequence: the more message's kind there are, the more complex MessageReceiver will be
            if (message is MessageA)
            {
                var messageA = message as MessageA;
                messageA?.MyCustomMethodOnA();
            }
            else if (message is MessageB)
            {
                var messageB = message as MessageB;
                messageB?.MyCustomMethodOnB();
                messageB?.SomeAdditionalMethodOnB();
            }
            else if (message is MessageC)
            {
                var messageC = message as MessageC;
                messageC?.MyCustomMethodOnC();
            }
        }
    }
}
