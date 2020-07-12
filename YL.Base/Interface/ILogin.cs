using YL.Base.dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace YL.Base.Interface
{
    public interface ILogin
    {
        CurrentUserDto GetCurrentUser();
    }
}
