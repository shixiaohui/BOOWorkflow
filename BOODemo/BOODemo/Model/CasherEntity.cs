using System;
using System.Collections.Generic;
using System.Linq;

namespace BOODemo.Model
{
    /// <summary>
    /// 实体类：收银台
    /// </summary>
    [Serializable]
    internal sealed class CasherEntity
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public CasherEntity(Restaurant rest)
        {
            this.restaurant = rest;
        }

        /// <summary>
        /// 获取待付款订单向量
        /// </summary>
        public List<GuestOrderEntity> WaitForPaymentOrderList
        {
            get
            {
                var wpc = from goe in this.restaurant.GuestOrderList
                          where goe.IsRequestPayment && !goe.IsPaid
                          select goe;
                return wpc.ToList();
            }
        }

        /// <summary>
        /// 支付订单
        /// </summary>
        /// <param name="oe">要支付的订单的引用</param>
        public void PayFor(GuestOrderEntity oe)
        {
            oe.FinishPayment();
        }

        /// <summary>
        /// 餐厅实体的引用
        /// </summary>
        private Restaurant restaurant;
    }
}
