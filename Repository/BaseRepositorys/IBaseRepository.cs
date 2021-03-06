﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepositorys
{
    public interface IBaseRepository<T>
    {
        #region 添加
        /// <summary>
        /// 受影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int InsertInt(T entity);
        /// <summary>
        /// 返回主键
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        long InsertLong(T entity);
        /// <summary>
        /// 返回实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T InsertEntity(T entity);
        /// <summary>
        /// 返回Bool
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool InsertBool(T entity);

        #endregion

        #region 修改
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(T entity);

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(T entity);
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteById(int id);
        /// <summary>
        /// 批量删除 主键集合
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        int DeleteListByIds(List<int> ids);
        #endregion

        #region 查询
        
        /// <summary>
        /// 主键取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);
      
        /// <summary>
        /// OpenId取实体
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        T GetByOpenId(string openId);
        #endregion
    }
}
