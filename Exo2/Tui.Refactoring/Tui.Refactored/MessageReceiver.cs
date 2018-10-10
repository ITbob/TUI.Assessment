using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tui.Refactored
{
    class MessageReceiver
    {
        public void Receive(IMessage message)
        {
            message.Do();
        }
    }
}
