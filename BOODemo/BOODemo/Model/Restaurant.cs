using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOODemo.Model
{
    /// <summary>
    /// 实体类：餐厅，为所有餐厅的活动实体提供容器
    /// </summary>
    [Serializable]
    internal sealed class Restaurant
    {
        /// <summary>
        /// 获取收银台
        /// </summary>
        public CasherEntity Casher
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取客户订单向量
        /// </summary>
        public List<GuestOrderEntity> GuestOrderList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取厨房餐单向量
        /// </summary>
        public List<KitchenOrderEntity> KitchenOrderList
        {
            get;
            private set;
        }
    }
}
