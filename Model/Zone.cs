using System;
using System.Collections.Generic;

namespace bbs.Model
{
	/// <summary>
	/// Zone:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Zone
	{
		public Zone()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _description;

        public List<Section> sectionList { set; get; }//一对多  (一个主题对应多个板块)

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
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string description
		{
			set{ _description=value;}
			get{return _description;}
		}
		#endregion Model

	}
}

