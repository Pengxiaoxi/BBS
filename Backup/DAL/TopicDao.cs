using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace bbs.DAL
{
	/// <summary>
	/// 数据访问类:TopicDao
	/// </summary>
	public partial class TopicDao
	{
		public TopicDao()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("t_u_id", "t_topic"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int t_u_id,int t_s_id,int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from t_topic");
			strSql.Append(" where t_u_id=@t_u_id and t_s_id=@t_s_id and id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@t_u_id", SqlDbType.Int,4),
					new SqlParameter("@t_s_id", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)			};
			parameters[0].Value = t_u_id;
			parameters[1].Value = t_s_id;
			parameters[2].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(bbs.Model.Topic model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_topic(");
			strSql.Append("t_u_id,t_s_id,content,modifytime,publishtime,title,good,top)");
			strSql.Append(" values (");
			strSql.Append("@t_u_id,@t_s_id,@content,@modifytime,@publishtime,@title,@good,@top)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@t_u_id", SqlDbType.Int,4),
					new SqlParameter("@t_s_id", SqlDbType.Int,4),
					new SqlParameter("@content", SqlDbType.VarChar,1000),
					new SqlParameter("@modifytime", SqlDbType.DateTime),
					new SqlParameter("@publishtime", SqlDbType.DateTime),
					new SqlParameter("@title", SqlDbType.VarChar,200),
					new SqlParameter("@good", SqlDbType.VarChar,10),
					new SqlParameter("@top", SqlDbType.VarChar,10)};
			parameters[0].Value = model.t_u_id;
			parameters[1].Value = model.t_s_id;
			parameters[2].Value = model.content;
			parameters[3].Value = model.modifytime;
			parameters[4].Value = model.publishtime;
			parameters[5].Value = model.title;
			parameters[6].Value = model.good;
			parameters[7].Value = model.top;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(bbs.Model.Topic model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_topic set ");
			strSql.Append("content=@content,");
			strSql.Append("modifytime=@modifytime,");
			strSql.Append("publishtime=@publishtime,");
			strSql.Append("title=@title,");
			strSql.Append("good=@good,");
			strSql.Append("top=@top");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@content", SqlDbType.VarChar,1000),
					new SqlParameter("@modifytime", SqlDbType.DateTime),
					new SqlParameter("@publishtime", SqlDbType.DateTime),
					new SqlParameter("@title", SqlDbType.VarChar,200),
					new SqlParameter("@good", SqlDbType.VarChar,10),
					new SqlParameter("@top", SqlDbType.VarChar,10),
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@t_u_id", SqlDbType.Int,4),
					new SqlParameter("@t_s_id", SqlDbType.Int,4)};
			parameters[0].Value = model.content;
			parameters[1].Value = model.modifytime;
			parameters[2].Value = model.publishtime;
			parameters[3].Value = model.title;
			parameters[4].Value = model.good;
			parameters[5].Value = model.top;
			parameters[6].Value = model.id;
			parameters[7].Value = model.t_u_id;
			parameters[8].Value = model.t_s_id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_topic ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int t_u_id,int t_s_id,int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_topic ");
			strSql.Append(" where t_u_id=@t_u_id and t_s_id=@t_s_id and id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@t_u_id", SqlDbType.Int,4),
					new SqlParameter("@t_s_id", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)			};
			parameters[0].Value = t_u_id;
			parameters[1].Value = t_s_id;
			parameters[2].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_topic ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public bbs.Model.Topic GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,t_u_id,t_s_id,content,modifytime,publishtime,title,good,top from t_topic ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			bbs.Model.Topic model=new bbs.Model.Topic();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public bbs.Model.Topic DataRowToModel(DataRow row)
		{
			bbs.Model.Topic model=new bbs.Model.Topic();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["t_u_id"]!=null && row["t_u_id"].ToString()!="")
				{
					model.t_u_id=int.Parse(row["t_u_id"].ToString());
				}
				if(row["t_s_id"]!=null && row["t_s_id"].ToString()!="")
				{
					model.t_s_id=int.Parse(row["t_s_id"].ToString());
				}
				if(row["content"]!=null)
				{
					model.content=row["content"].ToString();
				}
				if(row["modifytime"]!=null && row["modifytime"].ToString()!="")
				{
					model.modifytime=DateTime.Parse(row["modifytime"].ToString());
				}
				if(row["publishtime"]!=null && row["publishtime"].ToString()!="")
				{
					model.publishtime=DateTime.Parse(row["publishtime"].ToString());
				}
				if(row["title"]!=null)
				{
					model.title=row["title"].ToString();
				}
				if(row["good"]!=null)
				{
					model.good=row["good"].ToString();
				}
				if(row["top"]!=null)
				{
					model.top=row["top"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,t_u_id,t_s_id,content,modifytime,publishtime,title,good,top ");
			strSql.Append(" FROM t_topic ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,t_u_id,t_s_id,content,modifytime,publishtime,title,good,top ");
			strSql.Append(" FROM t_topic ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM t_topic ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from t_topic T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "t_topic";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

