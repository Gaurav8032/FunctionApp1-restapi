using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1_restapi
{
    public class machinedata
    {
        public int MessageID { get; set; }
        public string DeviceId { get; set; }
        public decimal temperature { get; set; }
        public decimal humidity { get; set; }
        public DateTime EventProcessedUtcTime { get; set; }
        public DateTime EventEnqueuedUtcTime { get; set; }
        public DateTime Insertedtime { get; set; }

    }
}
