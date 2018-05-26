﻿using System;
using System.Collections.Generic;

namespace MyCreek.Data.Interface
{
    public interface IAppLibrary
    {
        /// <summary>
        /// 新增
        /// </summary>
        int Add(MyCreek.Data.Model.AppLibrary model);

        /// <summary>
        /// 更新
        /// </summary>
        int Update(MyCreek.Data.Model.AppLibrary model);

        /// <summary>
        /// 查询所有记录
        /// </summary>
        List<MyCreek.Data.Model.AppLibrary> GetAll();

        /// <summary>
        /// 查询单条记录
        /// </summary>
        Model.AppLibrary Get(Guid id);

        /// <summary>
        /// 删除
        /// </summary>
        int Delete(Guid id);

        /// <summary>
        /// 查询记录条数
        /// </summary>
        long GetCount();

        /// <summary>
        /// 得到一页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="size"></param>
        /// <param name="numbe"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        List<MyCreek.Data.Model.AppLibrary> GetPagerData(out string pager, string query = "", string order = "Type,Title", int size = 15, int number = 1, string title = "", string type = "", string address = "");
       
        /// <summary>
        /// 查询一个类别下所有记录
        /// </summary>
        List<MyCreek.Data.Model.AppLibrary> GetAllByType(string type);

        /// <summary>
        /// 删除记录
        /// </summary>
        int Delete(string[] idArray);

        /// <summary>
        /// 根据代码查询一条记录
        /// </summary>
        MyCreek.Data.Model.AppLibrary GetByCode(string code);
    }
}
