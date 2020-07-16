using AIDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YL.Base;

namespace AIServer
{
    public class PlatformList : AIServiceBase
    {
        public PlatformList(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public List<Platforminfo> ss() 
        {
            return db.Platforminfo.Where(w => w.Id > 0).ToList();
        }
    }
}
