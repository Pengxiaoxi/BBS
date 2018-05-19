using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using bbs.Model;
namespace bbs.BLL
{
	/// <summary>
	/// SectionService
	/// </summary>
	public partial class SectionService
	{
		private readonly bbs.DAL.SectionDao dal=new bbs.DAL.SectionDao();
		public SectionService()
		{}
        #region  BasicMethod


        ////查询板块信息
        //public List<Section> GetSectionList()
        //{
        //    DataSet ds = dal.GetList("");
        //    List<Section> SectionList = DataTableToList(ds.Tables[0]);

        //    return SectionList;
        //}


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
		public bool Exists(int t_z_id,int t_u_id,int id)
		{
			return dal.Exists(t_z_id,t_u_id,id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(bbs.Model.Section model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(bbs.Model.Section model)
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
		public bool Delete(int t_z_id,int t_u_id,int id)
		{
			
			return dal.Delete(t_z_id,t_u_id,id);
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
		public bbs.Model.Section GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public bbs.Model.Section GetModelByCache(int id)
		{
			
			string CacheKey = "SectionModel-" + id;
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
			return (bbs.Model.Section)objModel;
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
		public List<bbs.Model.Section> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<bbs.Model.Section> DataTableToList(DataTable dt)
		{
			List<bbs.Model.Section> modelList = new List<bbs.Model.Section>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				bbs.Model.Section model;
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

