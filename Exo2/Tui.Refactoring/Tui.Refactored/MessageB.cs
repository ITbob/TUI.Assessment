using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tui.Refactored
{
    public class MessageB : IMessage
    {
        public void Do()
        {
            this.MyCustomMethodOnB();
            this.SomeAdditionalMethodOnB();
        }

        public void MyCustomMethodOnB()
        {
            //todo
        }

        public void SomeAdditionalMethodOnB()
        {
            //todo
        }
    }
}
