using Web.Manager.WebManager.Entity;
using Web.Manager.WebManager.Models;
using YL.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using YL.Base.Manager.Entity;
using YL.Base.Common;

namespace Web.Manager.WebManager.Business
{
    public class WebSYSUserManager : Web.Manager.WebManager.Business.ServiceBase
    {
        public WebSYSUserManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public AjaxResult<Object> UpdatePasswordDo(long managerid, string PassWord, string NewPassWord, string RePassWord)
        {
            if (string.IsNullOrEmpty(PassWord))
                return new AjaxResult<Object>("请输入原密码");
            if (string.IsNullOrEmpty(NewPassWord))
                return new AjaxResult<Object>("请输入新密码");
            if (string.IsNullOrEmpty(RePassWord))
                return new AjaxResult<Object>("请输入确认密码");
            if (NewPassWord != RePassWord)
                return new AjaxResult<Object>("新密码与确认密码不一致");
            var user = db.WebSysManager.FirstOrDefault(m => m.ManagerId == managerid);
            if (user == null)
                return new AjaxResult<Object>("账户不存在");
            if (user.ManagerPwd != Encrypt.MD5Encrypt(PassWord + user.ManagerScal))
                return new AjaxResult<Object>("原密码错误");

            user.UpdateTime = DateTime.Now;
            Random rd = new Random();
            string scal = rd.Next(100000, 999999).ToString();
            user.ManagerPwd = Encrypt.MD5Encrypt(NewPassWord + scal);
            user.ManagerScal = scal;
            int r = db.SaveChanges();
            if (r <= 0)
                return new AjaxResult<Object>("更新失败");
            return new AjaxResult<Object>("更新成功", 0);
        }

        /// <summary>
        /// 根据用户ID获取
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public WebSysManager GetManagerById(long ManagerId)
        {
            return db.WebSysManager.FirstOrDefault(m => m.ManagerId == ManagerId);
        }

        /// <summary>
        /// 根据用户名获取
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public WebSysManager GetManagerByName(string UserName)
        {
            return db.WebSysManager.FirstOrDefault(m => m.ManagerName == UserName);
        }

        public AjaxResult<Object> MyAccountSave(MyAccountSaveModel model)
        {
            if (string.IsNullOrEmpty(model.ManagerRealname))
                return new AjaxResult<Object>("请输入真实姓名");
            if (string.IsNullOrEmpty(model.ManagerTel))
                return new AjaxResult<Object>("请输入联系电话");
            if (!string.IsNullOrEmpty(model.ManagerPwd))
            {
                if (model.ManagerPwd != model.RePassword)
                {
                    return new AjaxResult<Object>("两次输入的密码不一致");
                }
            }
            var user = db.WebSysManager.FirstOrDefault(m => m.ManagerId == model.ManagerId);
            if (user == null)
                return new AjaxResult<Object>("账户不存在！");
            user.ManagerRealname = model.ManagerRealname;
            user.ManagerEmail = model.ManagerEmail;
            user.UpdateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(model.ManagerPwd))
            {
                Random rd = new Random();
                string scal = rd.Next(100000, 999999).ToString();
                user.ManagerPwd = Encrypt.MD5Encrypt(model.ManagerPwd + scal);
                user.ManagerScal = scal;
            }
            int r = db.SaveChanges();
            if (r <= 0)
                return new AjaxResult<Object>("更新失败");
            return new AjaxResult<Object>("更新成功", 0);
        }



        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="ManagerID"></param>
        /// <param name="oldPass"></param>
        /// <param name="newPass"></param>
        /// <param name="newpayPwd2"></param>
        /// <returns></returns>
        public AjaxResult<Object> ChangePassword(long ManagerID, string oldPass, string newPass, string newpayPwd2)
        {
            if (string.IsNullOrEmpty(oldPass))
                return new AjaxResult<Object>("请输入原密码！");
            if (string.IsNullOrEmpty(newPass))
                return new AjaxResult<Object>("请输入新密码！");
            if (string.IsNullOrEmpty(newpayPwd2))
                return new AjaxResult<Object>("请输入确认密码！");
            if (newPass != newpayPwd2)
                return new AjaxResult<Object>("新密码与确认密码不一致！");
            var user = db.WebSysManager.FirstOrDefault(m => m.ManagerId == ManagerID);
            if (user == null)
                return new AjaxResult<Object>("账户不存在！");

            string oldEnPwd = Encrypt.MD5Encrypt(oldPass + user.ManagerScal);


            if (oldEnPwd != user.ManagerPwd)
                return new AjaxResult<Object>("原密码输入错误！");
            Random rd = new Random();

            user.ManagerScal = rd.Next(10000, 99999).ToString();
            user.ManagerPwd = Encrypt.MD5Encrypt(newPass + user.ManagerScal);
            user.UpdateTime = DateTime.Now;
            int r = db.SaveChanges();
            if (r <= 0)
                return new AjaxResult<Object>("密码修改失败");
            return new AjaxResult<Object>("密码修改成功", 0);
        }



        public AjaxResult<Pagination<WebSysManager>> AccountLoadList(SysUserListModel model)
        {
            Pagination<WebSysManager> page = new Pagination<WebSysManager>();

            var query = from t in db.WebSysManager
                        select t;
            if (!string.IsNullOrEmpty(model.UserName))
                query = query.Where(m => m.ManagerName.Contains(model.UserName) || m.ManagerRealname.Contains(model.UserName) || m.ManagerEmail.Contains(model.UserName) || m.ManagerTel.Contains(model.UserName));
            page.TotalCount = query.Count();
            page.dataList = query.OrderByDescending(m => m.ManagerId).Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).ToList();
            return new AjaxResult<Pagination<WebSysManager>>(page);
        }



        public AjaxResult<SysAccountInfo> GetSysAccountInfo(int managerid)
        {
            var user = db.WebSysManager.FirstOrDefault(m => m.ManagerId == managerid);
            if (user == null)
                user = new WebSysManager();
            SysAccountInfo info = new SysAccountInfo()
            {
                IsSupper = user.IsSupper ?? 0,
                ManagerEmail = user.ManagerEmail,
                ManagerId = user.ManagerId,
                ManagerRealname = user.ManagerRealname,
                ManagerStatus = user.ManagerStatus ?? 0,
                ManagerTel = user.ManagerTel,
                ManagerName = user.ManagerName,

            };
            var roles = db.WebSysRole.ToList();
            var userRoles = db.WebSysManagerRole.Where(m => m.ManagerId == user.ManagerId);
            info.Roles = new List<RoleItemInfo>();
            foreach (var role in roles)
            {
                RoleItemInfo addItem = new RoleItemInfo()
                {
                    RoleID = role.RoleId,
                    RoleName = role.RoleName,
                };
                var userRole = userRoles.FirstOrDefault(m => m.RoleId == role.RoleId);
                if (userRole != null)
                    addItem.Checked = 1;
                info.Roles.Add(addItem);
            }
            return new AjaxResult<SysAccountInfo>(info);
        }



        public AjaxResult<Object> SaveAccountInfo(SysAccountInfo model)
        {
            if (string.IsNullOrEmpty(model.ManagerName))
                return new AjaxResult<Object>("请输入登录名");
            if (model.ManagerId == 0)
            {
                var userN = db.WebSysManager.FirstOrDefault(m => m.ManagerName == model.ManagerName);
                if (userN != null)
                    return new AjaxResult<Object>("该登录名已存在");
            }
            using (var scope = new TransactionScope())
            {
                var user = db.WebSysManager.FirstOrDefault(m => m.ManagerId == model.ManagerId);
                if (user == null)
                {
                    Random rd = new Random();
                    string scal = rd.Next(10000, 99999).ToString();
                    user = new WebSysManager()
                    {
                        ManagerName = model.ManagerName,
                        IsSupper = model.IsSupper,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        ManagerEmail = model.ManagerEmail,
                        ManagerIsdel = 0,
                        ManagerPwd = Encrypt.MD5Encrypt(Encrypt.MD5Encrypt("123456") + scal),
                        ManagerScal = scal,
                        ManagerRealname = model.ManagerRealname,
                        ManagerStatus = model.ManagerStatus,
                        ManagerTel = model.ManagerTel
                    };
                    db.WebSysManager.Add(user);
                    db.SaveChanges();
                }
                else
                {
                    user.IsSupper = model.IsSupper;
                    user.UpdateTime = DateTime.Now;
                    user.ManagerEmail = model.ManagerEmail;
                    user.ManagerRealname = model.ManagerRealname;
                    user.ManagerStatus = model.ManagerStatus;
                    user.IsSupper = model.IsSupper;
                    user.ManagerTel = model.ManagerTel;
                    db.SaveChanges();
                }
                //删除原先分配的角色
                var dels = db.WebSysManagerRole.Where(m => m.ManagerId == user.ManagerId).ToList();
                if (dels.Count > 0)
                {
                    db.WebSysManagerRole.RemoveRange(dels);
                }
                if (model.Roles != null)
                {
                    List<WebSysManagerRole> InSertRoles = new List<WebSysManagerRole>();
                    foreach (var item in model.Roles)
                    {
                        WebSysManagerRole addItem = new WebSysManagerRole()
                        {
                            ManagerId = user.ManagerId,
                            RoleId = item.RoleID,
                        };
                        InSertRoles.Add(addItem);
                    }
                    if (InSertRoles.Count > 0)
                    {
                        db.WebSysManagerRole.AddRange(InSertRoles);
                        db.SaveChanges();
                    }
                }
                scope.Complete();
            }
            return new AjaxResult<Object>("帐号保存成功！", 0);
        }

        public Task<List<WebSysManager>> GetUserListByIds(List<long> uids)
        {
            return db.WebSysManager.Where(m => uids.Contains(m.ManagerId)).ToListAsync();
        }
    }
}
