using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class ActionLog
    {
        public List<string> Actions { get; set; }
        public List<DateTime> TimeStamp { get; set; }
        public ActionLog()
        {
            Actions = new List<string>();
            TimeStamp = new List<DateTime>();
        }
    }
}
