using System;
namespace bbs.Model
{
	/// <summary>
	/// Topic:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Topic
	{
		public Topic()
		{}
		#region Model
		private int _id;
		private int _t_u_id;
		private int _t_s_id;
		private string _content;
		private DateTime? _modifytime;
		private DateTime? _publishtime;
		private string _title;
		private string _good;
		private string _top;
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
		public int t_u_id
		{
			set{ _t_u_id=value;}
			get{return _t_u_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int t_s_id
		{
			set{ _t_s_id=value;}
			get{return _t_s_id;}
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
		public string title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string good
		{
			set{ _good=value;}
			get{return _good;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string top
		{
			set{ _top=value;}
			get{return _top;}
		}
		#endregion Model

	}
}

