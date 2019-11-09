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

        public string GetBetString()
        {
            string strReturn = string.Empty;
            switch (this.BetType)
            {
                case Bet.庄:
                    strReturn = "庄";
                    break;
                case Bet.闲:
                    strReturn = "闲";
                    break;
                case  Bet.和:
                    strReturn = "和";
                    break;
                case Bet.停:
                    strReturn = "停";
                    break;
            }

            return strReturn;
        }

        public string[] ConvertToArrary()
        {
            return new string[4] { this.ActionIndex.ToString(), this.GetBetString(), this.Delay.ToString(), this.Money.ToString() };
        }

        public static CustomAction GetDefaultCustomAction()
        {
            return new CustomAction()
            {
                ActionIndex = 0,
                BetType = Bet.停,
                Delay = 0,
                Money = 0
            };
        }
    }

    public enum Bet
    {
        庄 = 0,
        闲 = 1,
        和 = 2,
        停 = 3
    }
}
