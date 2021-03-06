﻿using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using bbs.Model;

namespace bbs.BLL
{
	/// <summary>
	/// UserService
	/// </summary>
	public partial class UserService
	{
		private readonly bbs.DAL.UserDao dal=new bbs.DAL.UserDao();

        public int pageCount = 5;

        public UserService()
		{}
        #region  BasicMethod

        ////Login方法
        //public List<User> GetUser(string nickName,string passWord)
        //{
        //    DataSet ds = dal.GetList("nickName='"+nickName+"'"+ "and passWord='"+passWord+"'");
        //    //User userInfo = DataTableToList(ds.Tables[0]);

        //    List<User> userInfo = DataTableToList(ds.Tables[0]);

        //    return userInfo;
        //}

        //FindAllUser
        public List<User> FindAllUser(int pageNumber)
        {
            DataSet ds = this.GetListByPage("", "id asc", (pageNumber-1)*pageCount+1, pageNumber*pageCount);

            List<User> userList = this.DataTableToList(ds.Tables[0]);

            return userList;
        }




        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string nickname,int id)
		{
			return dal.Exists(nickname,id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(bbs.Model.User model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(bbs.Model.User model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string nickname,int id)
		{
			
			return dal.Delete(nickname,id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public bbs.Model.User GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public bbs.Model.User GetModelByCache(int id)
		{
			
			string CacheKey = "UserModel-" + id;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (bbs.Model.User)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<bbs.Model.User> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<bbs.Model.User> DataTableToList(DataTable dt)
		{
			List<bbs.Model.User> modelList = new List<bbs.Model.User>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				bbs.Model.User model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

