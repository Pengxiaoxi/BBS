using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace bbs.DAL
{
	/// <summary>
	/// 数据访问类:UserDao
	/// </summary>
	public partial class UserDao
	{
		public UserDao()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "t_user"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string nickname,int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from t_user");
			strSql.Append(" where nickname=@nickname and id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@nickname", SqlDbType.VarChar,20),
					new SqlParameter("@id", SqlDbType.Int,4)			};
			parameters[0].Value = nickname;
			parameters[1].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(bbs.Model.User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_user(");
			strSql.Append("email,face,mobile,nickname,password,regtime,sex,truename,type)");
			strSql.Append(" values (");
			strSql.Append("@email,@face,@mobile,@nickname,@password,@regtime,@sex,@truename,@type)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@email", SqlDbType.VarChar,100),
					new SqlParameter("@face", SqlDbType.VarChar,200),
					new SqlParameter("@mobile", SqlDbType.VarChar,20),
					new SqlParameter("@nickname", SqlDbType.VarChar,20),
					new SqlParameter("@password", SqlDbType.VarChar,30),
					new SqlParameter("@regtime", SqlDbType.DateTime),
					new SqlParameter("@sex", SqlDbType.VarChar,4),
					new SqlParameter("@truename", SqlDbType.VarChar,20),
					new SqlParameter("@type", SqlDbType.VarChar,10)};
			parameters[0].Value = model.email;
			parameters[1].Value = model.face;
			parameters[2].Value = model.mobile;
			parameters[3].Value = model.nickname;
			parameters[4].Value = model.password;
			parameters[5].Value = model.regtime;
			parameters[6].Value = model.sex;
			parameters[7].Value = model.truename;
			parameters[8].Value = model.type;

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
		public bool Update(bbs.Model.User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_user set ");
			strSql.Append("email=@email,");
			strSql.Append("face=@face,");
			strSql.Append("mobile=@mobile,");
			strSql.Append("password=@password,");
			strSql.Append("regtime=@regtime,");
			strSql.Append("sex=@sex,");
			strSql.Append("truename=@truename,");
			strSql.Append("type=@type");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@email", SqlDbType.VarChar,100),
					new SqlParameter("@face", SqlDbType.VarChar,200),
					new SqlParameter("@mobile", SqlDbType.VarChar,20),
					new SqlParameter("@password", SqlDbType.VarChar,30),
					new SqlParameter("@regtime", SqlDbType.DateTime),
					new SqlParameter("@sex", SqlDbType.VarChar,4),
					new SqlParameter("@truename", SqlDbType.VarChar,20),
					new SqlParameter("@type", SqlDbType.VarChar,10),
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@nickname", SqlDbType.VarChar,20)};
			parameters[0].Value = model.email;
			parameters[1].Value = model.face;
			parameters[2].Value = model.mobile;
			parameters[3].Value = model.password;
			parameters[4].Value = model.regtime;
			parameters[5].Value = model.sex;
			parameters[6].Value = model.truename;
			parameters[7].Value = model.type;
			parameters[8].Value = model.id;
			parameters[9].Value = model.nickname;

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
			strSql.Append("delete from t_user ");
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
		public bool Delete(string nickname,int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_user ");
			strSql.Append(" where nickname=@nickname and id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@nickname", SqlDbType.VarChar,20),
					new SqlParameter("@id", SqlDbType.Int,4)			};
			parameters[0].Value = nickname;
			parameters[1].Value = id;

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
			strSql.Append("delete from t_user ");
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
		public bbs.Model.User GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,email,face,mobile,nickname,password,regtime,sex,truename,type from t_user ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			bbs.Model.User model=new bbs.Model.User();
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
		public bbs.Model.User DataRowToModel(DataRow row)
		{
			bbs.Model.User model=new bbs.Model.User();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["email"]!=null)
				{
					model.email=row["email"].ToString();
				}
				if(row["face"]!=null)
				{
					model.face=row["face"].ToString();
				}
				if(row["mobile"]!=null)
				{
					model.mobile=row["mobile"].ToString();
				}
				if(row["nickname"]!=null)
				{
					model.nickname=row["nickname"].ToString();
				}
				if(row["password"]!=null)
				{
					model.password=row["password"].ToString();
				}
				if(row["regtime"]!=null && row["regtime"].ToString()!="")
				{
					model.regtime=DateTime.Parse(row["regtime"].ToString());
				}
				if(row["sex"]!=null)
				{
					model.sex=row["sex"].ToString();
				}
				if(row["truename"]!=null)
				{
					model.truename=row["truename"].ToString();
				}
				if(row["type"]!=null)
				{
					model.type=row["type"].ToString();
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
			strSql.Append("select id,email,face,mobile,nickname,password,regtime,sex,truename,type ");
			strSql.Append(" FROM t_user ");
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
			strSql.Append(" id,email,face,mobile,nickname,password,regtime,sex,truename,type ");
			strSql.Append(" FROM t_user ");
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
			strSql.Append("select count(1) FROM t_user ");
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
			strSql.Append(")AS Row, T.*  from t_user T ");
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
			parameters[0].Value = "t_user";
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

