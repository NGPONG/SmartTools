using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test_11
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Person> dic = new Dictionary<string, Person>();
            dic.Add("wupeng", new Person() { Name = "wupeng", Age = 22, Address = "shenzhenlonghua" });
            dic.Add("wupeng1", new Person() { Name = "wupeng1", Age = 21, Address = "shenzhenlonghua" });
            dic.Add("wupeng2", new Person() { Name = "wupeng2", Age = 20, Address = "shenzhenlonghua" });
            dic.Add("wupeng3", new Person() { Name = "wupeng3", Age = 19, Address = "shenzhenlonghua" });


            string str =  JsonConvert.SerializeObject(dic);
        }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        [JsonIgnore]
        public string Address { get; set; }
    }
}
