using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Model
{
    public class ActionPoint
    {
        public Point One { get; set; } = new Point(602, 360);
        public Point Two { get; set; } = new Point(632, 360);
        public Point Three { get; set; } = new Point(662, 360);
        public Point Four { get; set; } = new Point(692, 360);
        public Point Five { get; set; } = new Point(732, 360);

        public Point BetPoint(Bet bet)
        {
            Point point;
            switch (bet)
            {
                case Bet.庄:
                    point = new Point(509, 320);
                    break;
                case Bet.闲:
                    point = new Point(509, 300);
                    break;
                case Bet.和:
                    point = new Point(509, 280);
                    break;
                default:
                    point = Point.Empty;
                    break;
            }
            return point;
        }
    }
}
