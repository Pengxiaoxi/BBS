using System;
namespace bbs.Model
{
	/// <summary>
	/// Reply:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Reply
	{
		public Reply()
		{}
		#region Model
		private int _id;
		private int _t_t_id;
		private int _t_u_id;
		private DateTime? _modifytime;
		private DateTime? _publishtime;
		private string _content;
		private string _title;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int t_t_id
		{
			set{ _t_t_id=value;}
			get{return _t_t_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int t_u_id
		{
			set{ _t_u_id=value;}
			get{return _t_u_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? modifytime
		{
			set{ _modifytime=value;}
			get{return _modifytime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? publishtime
		{
			set{ _publishtime=value;}
			get{return _publishtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string title
		{
			set{ _title=value;}
			get{return _title;}
		}
		#endregion Model

	}
}

