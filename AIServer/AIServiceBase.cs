using AIDB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using YL.Base;

namespace AIServer
{
    public abstract class AIServiceBase : ServiceBase<ai_platformContext>
    {
        protected AIServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }
}
