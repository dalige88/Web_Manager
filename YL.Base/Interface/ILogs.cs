using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YL.Base.Interface
{
    public interface ILogs
    {
         void Add(long uid, string name, string ip, int type, string typename, string mapmethod, string logcontent);
    }
}
