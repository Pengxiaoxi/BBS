using System;
namespace bbs.Model
{
	/// <summary>
	/// Section:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Section
	{
		public Section()
		{}
		#region Model
		private int _id;
		private int _t_z_id;
		private int _t_u_id;
		private string _name;
		private string _logo;
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
		public int t_z_id
		{
			set{ _t_z_id=value;}
			get{return _t_z_id;}
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
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string logo
		{
			set{ _logo=value;}
			get{return _logo;}
		}
		#endregion Model

	}
}

