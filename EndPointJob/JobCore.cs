using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPointJob
{
    public class JobCore
    {
        public Queue<QueueType> DeclearQueue { get; }
        public Queue<QueueType> FinancialQueue { get; }
        public Queue<QueueType> ClaimQueue { get; }
        public JobCore()
        {
            DeclearQueue = new Queue<QueueType>();
            FinancialQueue = new Queue<QueueType>();
            ClaimQueue = new Queue<QueueType>();
        }
    }
    public class QueueType
    {
        public long FormId { get; set; }
        public DateTime Deadline { get; set; }
    }
}
