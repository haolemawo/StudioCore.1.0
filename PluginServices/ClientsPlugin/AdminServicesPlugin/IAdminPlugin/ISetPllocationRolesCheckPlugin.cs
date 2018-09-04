﻿using Domain.BaseModels;
using Domain.DTOModels;
using Domain.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsPlugin.AdminServicesPlugin
{
   public interface ISetPllocationRolesCheckPlugin
    {
        /// <summary>
        /// 设置角色数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
         BaseResult SetPllocationRolesCheck(string safetySecretKey, bool isUsable,AuthRequest entity);
    }
}