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
        public Point One { get; set; } = new Point(603, 357);
        public Point Two { get; set; } = new Point(635, 357);
        public Point Three { get; set; } = new Point(667, 357);
        public Point Four { get; set; } = new Point(699, 357);
        public Point Five { get; set; } = new Point(731, 357);
        public Point Confirm { get; set; } = new Point(413, 355);

        public Point BetPoint(Bet bet)
        {
            Point point;
            switch (bet)
            {
                case Bet.和:
                    point = new Point(415, 277);
                    break;
                case Bet.庄:
                    point = new Point(415, 297);
                    break;
                case Bet.闲:
                    point = new Point(415, 317);
                    break;
                default:
                    point = Point.Empty;
                    break;
            }
            return point;
        }
    }
}
