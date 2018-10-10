using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tui.Refactored
{
    public class MessageA : IMessage
    {
        public void Do()
        {
            this.MyCustomMethodOnA();
        }

        public void MyCustomMethodOnA()
        {
            //todo
        }
    }
}
