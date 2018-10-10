using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tui.Refactored
{
    public class MessageC:IMessage
    {
        public void Do()
        {
            this.MyCustomMethodOnC();
        }

        public void MyCustomMethodOnC()
        {
            //todo
        }
    }
}
