using System;
using System.Data;
using System.Collections.Generic;
using bbs.Model;
using System.Collections;
using Util;

namespace bbs.BLL
{
    /// <summary>
    /// TopicService
    /// </summary>
    public partial class TopicService
	{
		private readonly bbs.DAL.TopicDao dal=new bbs.DAL.TopicDao();
        private readonly bbs.DAL.ReplyDao replyDao = new bbs.DAL.ReplyDao();
        private readonly bbs.DAL.UserDao userDao = new bbs.DAL.UserDao();
        private readonly bbs.DAL.SectionDao sectionDao = new bbs.DAL.SectionDao();
        private readonly bbs.DAL.ZoneDao zoneDao = new bbs.DAL.ZoneDao();

        public TopicService()
		{}
        #region  BasicMethod

        //置顶帖子
        public ArrayList findZdTopic(int sectionId)
        {
            DataSet ds = dal.GetList("t_s_id='"+ sectionId+"' and [top] = '1'");

            //将ds对象转换成List集合
            List<Topic> ZdTopicList = DataTableToList(ds.Tables[0]);

            //保存每个贴子的回复作者与回复时间
            Dictionary<int, Reply> topicLastReply = new Dictionary<int, Reply>();

            //保存每个贴子的回复数
            Dictionary<int, int> topicReplyCount = new Dictionary<int, int>();

            //查询置顶帖子
            foreach (Topic topic in ZdTopicList)
            {
                //获取(封装)发贴人的用户信息到贴子对象中去
                topic.topicuser = userDao.GetModel(topic.t_u_id);

                //通过主帖id进行查询回复的贴子对象.但是我们只需要最后回复的那一条
                ds = replyDao.GetList(1, "t_t_id='" + topic.id + "'", "publishtime desc");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Reply reply = replyDao.DataRowToModel(ds.Tables[0].Rows[0]);//将查询出来的回贴DataSet转换成Reply对象
                                                                                //获取(封装)回帖人的用户信息到回贴对象中去
                    reply.replyuser = userDao.GetModel(reply.t_u_id);

                    topicLastReply.Add(topic.id, reply);//主贴ID为key , 回贴对象为value

                    //得到此主贴下的回贴数
                    int count = replyDao.GetRecordCount("t_t_id='" + topic.id + "'");
                    topicReplyCount.Add(topic.id, count);//主贴ID为key , 回贴数为value
                }
            }

            ArrayList mylist = new ArrayList();
            mylist.Add(topicReplyCount);//0下标:回帖数
            mylist.Add(topicLastReply);//1下标:回帖作者与回帖时间
            mylist.Add(ZdTopicList);//2下标:置顶的主贴对象

            return mylist;
        }




        //通过板块id(外键id)进行数据查询并且分页(普通帖子)
        public ArrayList findTopic(int sectionId, int pageNumber)
        {
            int pageCount = 3;   //每页3行数

            Section section = sectionDao.GetModel(sectionId);   //通过主键ID查询板块信息
            section.user = userDao.GetModel(section.t_u_id);    //外键查询，通过section表中的外键ID查询需要的用户和板块的信息
            section.zone = zoneDao.GetModel(section.t_z_id);

            //查询主贴的总记录数
            int recordCount = dal.GetRecordCount("t_s_id='"+ sectionId + " 'and [top] = '0'");  

            //主页分页连接    目标地址，总记录数，页数，每页行数
            string pageCode = PageUtil.genPagination("/topic/TopicList.aspx", recordCount, pageNumber, pageCount, "sectionId="+ sectionId);

            //分页查询数据，返回dataset  每页数据的第一行(pageNumber - 1) * pageCount+1  与最后一行pageNumber * pageCount   between and是>= & <=
            DataSet ds = dal.GetListByPage("t_s_id='"+sectionId+"' and [top]='0'", "", (pageNumber - 1) * pageCount+1, pageNumber * pageCount);

            //将ds对象转换成List集合
            List<Topic> ptTopicList = DataTableToList(ds.Tables[0]);

            //保存每个贴子的回复作者与回复时间
            Dictionary<int, Reply> topicLastReply = new Dictionary<int, Reply>();

            //保存每个贴子的回复数
            Dictionary<int, int> topicReplyCount = new Dictionary<int, int>();

            //查询普通帖子
            foreach (Topic topic in ptTopicList)
            {
                //获取(封装)发贴人的用户信息到贴子对象中去
                topic.topicuser = userDao.GetModel(topic.t_u_id);

                //通过主帖id进行查询回复的贴子对象.但是我们只需要最后回复的那一条
                ds = replyDao.GetList(1, "t_t_id='" + topic.id + "'", "publishtime desc");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Reply reply = replyDao.DataRowToModel(ds.Tables[0].Rows[0]);//将查询出来的回贴DataSet转换成Reply对象
                                                                                //获取(封装)回帖人的用户信息到回贴对象中去
                    reply.replyuser = userDao.GetModel(reply.t_u_id);

                    topicLastReply.Add(topic.id, reply);//主贴ID为key , 回贴对象为value

                    //得到此主贴下的回贴数
                    int count = replyDao.GetRecordCount("t_t_id='" + topic.id + "'");
                    topicReplyCount.Add(topic.id, count);//主贴ID为key , 回贴数为value
                }
            }

            ArrayList mylist = new ArrayList();
            mylist.Add(topicReplyCount);//0下标:回帖数
            mylist.Add(topicLastReply);//1下标:回帖作者与回帖时间
            mylist.Add(ptTopicList);//2下标:普通的主贴对象
            mylist.Add(pageCode);   //3下标:保存分页的连接方法
            mylist.Add(section);    //4下标：保存板块对象
            return mylist;
        }


        //查询未回复的帖子的方法
        public int UnAnswerCount(int sectionId)
        {
            return dal.UnAnswerCount(sectionId);
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
		public bool Exists(int t_u_id,int t_s_id,int id)
		{
			return dal.Exists(t_u_id,t_s_id,id);
		}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int  Add(bbs.Model.Topic model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(bbs.Model.Topic model)
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
		/// 通过外键用户Id删除帖子
		/// </summary>
		public bool DeleteByUid(int id)
        {

            return dal.DeleteByUid(id);
        }


        
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int t_u_id,int t_s_id,int id)
		{
			
			return dal.Delete(t_u_id,t_s_id,id);
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
        public bbs.Model.Topic GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public bbs.Model.Topic GetModelByCache(int id)
		{
			
			string CacheKey = "TopicModel-" + id;
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
			return (bbs.Model.Topic)objModel;
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
		public List<bbs.Model.Topic> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<bbs.Model.Topic> DataTableToList(DataTable dt)
		{
			List<bbs.Model.Topic> modelList = new List<bbs.Model.Topic>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				bbs.Model.Topic model;
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

