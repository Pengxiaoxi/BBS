using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace bbs.DAL
{
	/// <summary>
	/// 数据访问类:SectionDao
	/// </summary>
	public partial class SectionDao
	{
		public SectionDao()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("t_z_id", "t_section"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int t_z_id,int t_u_id,int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from t_section");
			strSql.Append(" where t_z_id=@t_z_id and t_u_id=@t_u_id and id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@t_z_id", SqlDbType.Int,4),
					new SqlParameter("@t_u_id", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)			};
			parameters[0].Value = t_z_id;
			parameters[1].Value = t_u_id;
			parameters[2].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(bbs.Model.Section model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_section(");
			strSql.Append("t_z_id,t_u_id,name,logo)");
			strSql.Append(" values (");
			strSql.Append("@t_z_id,@t_u_id,@name,@logo)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@t_z_id", SqlDbType.Int,4),
					new SqlParameter("@t_u_id", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.VarChar,40),
					new SqlParameter("@logo", SqlDbType.VarChar,100)};
			parameters[0].Value = model.t_z_id;
			parameters[1].Value = model.t_u_id;
			parameters[2].Value = model.name;
			parameters[3].Value = model.logo;

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
		public bool Update(bbs.Model.Section model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_section set ");
			strSql.Append("name=@name,");
			strSql.Append("logo=@logo");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,40),
					new SqlParameter("@logo", SqlDbType.VarChar,100),
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@t_z_id", SqlDbType.Int,4),
					new SqlParameter("@t_u_id", SqlDbType.Int,4)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.logo;
			parameters[2].Value = model.id;
			parameters[3].Value = model.t_z_id;
			parameters[4].Value = model.t_u_id;

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
			strSql.Append("delete from t_section ");
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
		public bool Delete(int t_z_id,int t_u_id,int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_section ");
			strSql.Append(" where t_z_id=@t_z_id and t_u_id=@t_u_id and id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@t_z_id", SqlDbType.Int,4),
					new SqlParameter("@t_u_id", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)			};
			parameters[0].Value = t_z_id;
			parameters[1].Value = t_u_id;
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
			strSql.Append("delete from t_section ");
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
		public bbs.Model.Section GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,t_z_id,t_u_id,name,logo from t_section ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			bbs.Model.Section model=new bbs.Model.Section();
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
		public bbs.Model.Section DataRowToModel(DataRow row)
		{
			bbs.Model.Section model=new bbs.Model.Section();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["t_z_id"]!=null && row["t_z_id"].ToString()!="")
				{
					model.t_z_id=int.Parse(row["t_z_id"].ToString());
				}
				if(row["t_u_id"]!=null && row["t_u_id"].ToString()!="")
				{
					model.t_u_id=int.Parse(row["t_u_id"].ToString());
				}
				if(row["name"]!=null)
				{
					model.name=row["name"].ToString();
				}
				if(row["logo"]!=null)
				{
					model.logo=row["logo"].ToString();
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
			strSql.Append("select id,t_z_id,t_u_id,name,logo ");
			strSql.Append(" FROM t_section ");
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
			strSql.Append(" id,t_z_id,t_u_id,name,logo ");
			strSql.Append(" FROM t_section ");
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
			strSql.Append("select count(1) FROM t_section ");
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
			strSql.Append(")AS Row, T.*  from t_section T ");
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
			parameters[0].Value = "t_section";
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

