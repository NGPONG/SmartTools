using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Model
{
    public class CustomAction
    {
        public int ActionIndex { get; set; }
        public Bet BetType { get; set; }
        public int Delay { get; set; }
        public double Money { get; set; }
        public double StopMoney { get; set; }
        public bool IsCycle { get; set; }
    }

    public enum Bet
    {
        庄 = 0,
        闲 = 1,
        和 = 2,
        停 = 3
    }
}
