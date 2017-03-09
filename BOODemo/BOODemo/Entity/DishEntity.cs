using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOODemo.Entity
{
    /// <summary>
    /// 实体类：一种料理
    /// </summary>
    internal sealed class DishEntity
    {
        /// <summary>
        /// 获取或设置料理的名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置料理的单价
        /// </summary>
        public double Price
        {
            get;
            set;
        }
    }
}
