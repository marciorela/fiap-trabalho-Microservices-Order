using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Service
{
    public class MessageNewOrder : Message
    {

        protected override void Configure()
        {
            _topicName = "NewOrder";
        }
    }
}
