using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LinqContext = QSP.OrderExpress.Business.Context;
using LinqEntity = QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Enum;

namespace QSPForm.Business
{
    public class FormPermissionSystem
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRoleList"></param>
        /// <returns></returns>
        public List<LinqEntity.FormPermission> SelectByUserRoles(List<LinqEntity.UserRole> userRoleList)
        {
            List<LinqEntity.FormPermission> result = new List<LinqEntity.FormPermission>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from    fp in context.FormPermissions
                        select  fp;

            foreach (LinqEntity.UserRole userRole in userRoleList)
            {
                query = from    fp in query
                        where   fp.RoleId == userRole.RoleId
                        select  fp;
            }

            result = query.ToList<LinqEntity.FormPermission>();

            return result;
        }

    }
}
