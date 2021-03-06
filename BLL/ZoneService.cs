﻿using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using bbs.Model;
using bbs.BLL;

namespace bbs.BLL
{
	/// <summary>
	/// ZoneService
	/// </summary>
	public partial class ZoneService
	{
		private readonly bbs.DAL.ZoneDao dal=new bbs.DAL.ZoneDao();

        private readonly bbs.DAL.SectionDao sectionDao = new bbs.DAL.SectionDao();
        private readonly SectionService sectionService = new SectionService();

        private readonly ZoneService zoneService = new ZoneService();    
        private readonly TopicService topicService = new TopicService();
        private readonly ReplyService replyService = new ReplyService();

        public int pageCount = 5;

        public ZoneService()
		{}
        #region  BasicMethod

        //查询所以主题信息时也将此主题下相应的板块信息查询出来
        public List<Zone> GetZoneSectionList()
        {
            DataSet ds = dal.GetList("");
            List<Zone> zoneList = DataTableToList(ds.Tables[0]);

            //注意:下面代码就是重点 利用foreach循环zone 与 section
            foreach (Zone zone in zoneList)
            {
                DataSet ds2 = sectionDao.GetList("t_z_id = '" + zone.id + "'");
                List<Section> sectionList = sectionService.DataTableToList(ds2.Tables[0]);
                zone.sectionList = sectionList;
            }
            return zoneList;
        }


        //查找所有zone
        public List<Zone> FindAllZone(int pageNumber)
        {
            DataSet ds = this.GetListByPage("","id asc",(pageNumber-1)*pageCount+1,pageNumber*pageCount);

            List<Zone> zoneList = this.DataTableToList(ds.Tables[0]);

            return zoneList;
        }


        //删除大板块及其下面所有小版块，帖子和回复
        public bool MyDelete(int zoneId)
        {
            List<Section> sectionList = sectionService.GetModelList("t_z_id=" +zoneId);

            foreach (Section section in sectionList)
            {
                List<Topic> topicList = topicService.GetModelList("t_s_id=" +section.id);

                foreach (Topic topic in topicList)
                {
                    replyService.DeleteByTid(topic.id);  //删除回帖
                }
                topicService.DeleteBySid(section.id);    //删除主贴
            }
            sectionService.DeleteByZid(zoneId);          //删除小版块

            return zoneService.Delete(zoneId);           //删除大板块
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
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(bbs.Model.Zone model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(bbs.Model.Zone model)
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
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public bbs.Model.Zone GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public bbs.Model.Zone GetModelByCache(int id)
		{
			
			string CacheKey = "ZoneModel-" + id;
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
			return (bbs.Model.Zone)objModel;
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
		public List<bbs.Model.Zone> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<bbs.Model.Zone> DataTableToList(DataTable dt)
		{
			List<bbs.Model.Zone> modelList = new List<bbs.Model.Zone>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				bbs.Model.Zone model;
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

