using SmartTools.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Utils.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable ConvertByJagged<T>(this IEnumerable<T> enumerable)
            where T : CustomAction
        {
            string[][] data = new string[enumerable.Count()][];

            //init
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new string[5];
            }

            int index = 0;
            foreach (var item in enumerable)
            {
                data[index][0] = item.ActionIndex.ToString();
                data[index][1] = item.GetBetString();
                data[index][2] = item.Delay.ToString();
                data[index][3] = item.Money.ToString();
                data[index][4] = "点击删除";
                index ++;
            }

            return data;
        }
    }
}
