using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Model
{
    public class CustomAction
    {
        public string ActionIndex { get; set; }
        public Bet BetType { get; set; }
        public string Delay { get; set; }
        public string Money { get; set; }

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

        public static Bet ToBet(string source)
        {
            Bet bet = new Bet();
            switch (source)
            {
                case "庄":
                    bet = Bet.庄;
                    break;
                case "闲":
                    bet = Bet.闲;
                    break;
                case "和":
                    bet = Bet.和;
                    break;
                case "停":
                    bet = Bet.停;
                    break;
            }

            return bet;
        }

        public string[] ConvertToArrary()
        {
            return new string[4] { this.ActionIndex.ToString(), this.GetBetString(), this.Delay.ToString(), this.Money.ToString() };
        }

        public static CustomAction GetDefaultCustomAction()
        {
            return new CustomAction()
            {
                ActionIndex = "0",
                BetType = Bet.停,
                Delay = "0",
                Money = "0"
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
