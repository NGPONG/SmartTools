using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Service.Data
{
    public static class DbContainer
    {
        public static SM_ModuleContainer GetDbContext()
        {
            var dbContext = CallContext.GetData("DbContext");
            if (dbContext == null)
            {
                dbContext = new SM_ModuleContainer();
                CallContext.SetData("DbContext", dbContext);
            }

            return dbContext as SM_ModuleContainer;
        }
    }
}
