using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using bbs.Model;
using Util;
using System.Collections;

namespace bbs.BLL
{
	/// <summary>
	/// ReplyService
	/// </summary>
	public partial class ReplyService
	{
        private readonly bbs.DAL.TopicDao topicDao = new bbs.DAL.TopicDao();
        private readonly bbs.DAL.UserDao userDao = new bbs.DAL.UserDao();
        private readonly bbs.DAL.SectionDao sectionDao = new bbs.DAL.SectionDao();
        private readonly bbs.DAL.ReplyDao dal=new bbs.DAL.ReplyDao();

        public int pageCount = 3;
        public ReplyService()
		{}
        #region  BasicMethod


        //通过主贴的id查询出该贴信息，并将该帖子下的所有回复内容及回复人查询出
        public ArrayList finfTopicById(int topicId, int pageNumber)
        {
            

            //通过topicId查询出帖子信息
            Topic topic = topicDao.GetModel(topicId);

            //通过帖子的外键t_u_id查出"发帖人"的信息并封装到topic对象中
            topic.topicuser = userDao.GetModel(topic.t_u_id);

            //分页查询出"回帖人"的信息得到DataSet集合（游标）
            DataSet replyDataSet = dal.GetListByPage("t_t_id='"+ topicId +"'","publishtime asc",(pageNumber-1)*pageCount+1,pageNumber*pageCount);

            //将DataSet集合装换（封装）成List集合
            List<Reply> replayList = this.DataTableToList(replyDataSet.Tables[0]);

            //通过Foreach循环将replyList中的信息封装到reply对象中
            foreach (Reply reply in replayList)
            {
                User user = userDao.GetModel(reply.t_u_id);
                reply.replyuser = user;
            }

            //通过帖子Id求总记录数（总回复数）
            int maxRecoed = GetRecordCount("t_t_id='"+topicId+"'");

            //生成回帖分页的连接
            string pageCode = PageUtil.genPagination("/topic/TopicDetails.aspx", maxRecoed, pageNumber, pageCount, "topicId=" +topicId);

            Section section = sectionDao.GetModel(topic.t_s_id);  //通过帖子中的外键t_s_id求出板块信息

            //创建一个ArrayList对象将返回的数据封装到此对象中
            ArrayList mylist = new ArrayList();

            mylist.Add(topic);      //0下标  主贴对象
            mylist.Add(replayList); //1下标  回帖集合
            mylist.Add(pageCode);   //2下标  分页连接
            mylist.Add(section);    //3下标  板块信息

            return mylist;
        }

        //判断当前页是否是最后一页，是否进行跳转 传值ajax
        public int CheckPage(int topicId, int pageNumber)
        {
            int maxRecord = this.GetRecordCount("t_t_id=" +topicId);
            int maxPage;
            //求maxPage
            if (maxRecord % pageCount == 0)
            {
                maxPage = maxRecord / pageCount;
            }
            else
            {
                maxPage = maxRecord / pageCount + 1;
            }

            if (maxPage != 0)
            {
                if (pageNumber > maxPage)
                {
                    return -1;  //重新跳转一次
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
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
		public bool Exists(int t_t_id,int t_u_id,int id)
		{
			return dal.Exists(t_t_id,t_u_id,id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(bbs.Model.Reply model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(bbs.Model.Reply model)
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
		/// 通过外键帖子Id删除回帖
		/// </summary>
		public bool DeleteByTid(int id)
        {

            return dal.DeleteByTid(id);
        }


        /// <summary>
        /// 通过外键用户Id删除回帖
        /// </summary>
        public bool DeleteByUid(int id)
        {

            return dal.DeleteByUid(id);
        }



        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int t_t_id,int t_u_id,int id)
		{
			
			return dal.Delete(t_t_id,t_u_id,id);
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
		public bbs.Model.Reply GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public bbs.Model.Reply GetModelByCache(int id)
		{
			
			string CacheKey = "ReplyModel-" + id;
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
			return (bbs.Model.Reply)objModel;
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
		public List<bbs.Model.Reply> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<bbs.Model.Reply> DataTableToList(DataTable dt)
		{
			List<bbs.Model.Reply> modelList = new List<bbs.Model.Reply>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				bbs.Model.Reply model;
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
        /// 获取记录总数
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

