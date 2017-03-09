using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOODemo.Model
{
    /// <summary>
    /// 实体类：用户订单
    /// </summary>
    internal sealed class GuestOrderEntity
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public GuestOrderEntity()
        {
            lock (GlobalDataContext.OrderCounterMutex)
            {
                this.OrderId = GlobalDataContext.OrderIdCounter++;
            }
            this.CreateTimeStamp = DateTime.Now;
            this.PaidTimeStamp = null;
            this.orderedList = new List<DishEntity>();
        }

        /// <summary>
        /// 为订单添加一个料理
        /// </summary>
        /// <param name="dish">料理实体</param>
        public void AddDish(DishEntity dish)
        {
            this.orderedList.Add(dish);
        }

        /// <summary>
        /// 为订单移除一个料理
        /// </summary>
        /// <param name="index">要移除的料理在订单向量的下标</param>
        public void RemoveDish(int index)
        {
            this.orderedList.RemoveAt(index);
        }

        /// <summary>
        /// 订单已完成支付
        /// </summary>
        public void FinishPayment()
        {
            this.PaidTimeStamp = DateTime.Now;
        }

        /// <summary>
        /// 将订单转化为描述文本
        /// </summary>
        /// <returns>订单详情</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("ID: {0}", this.OrderId));
            sb.AppendLine(String.Format("Order Time: {0}", this.CreateTimeStamp));
            sb.AppendLine("----------");
            foreach (var item in this.orderedList)
            {
                sb.AppendLine(String.Format(" {0} : ${1}", item.Name, item.Price));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取订单的唯一编号
        /// </summary>
        public int OrderId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取订单被创建的时间戳
        /// </summary>
        public DateTime CreateTimeStamp
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取订单被支付的时间戳
        /// </summary>
        public DateTime? PaidTimeStamp
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取订单的总价值
        /// </summary>
        public double TotalPrice
        {
            get
            {
                return this.orderedList.Aggregate(0.0, (acct, t) => acct + t.Price);
            }
        }

        /// <summary>
        /// 获取订单是否已经支付
        /// </summary>
        public bool IsPaid
        {
            get
            {
                return this.PaidTimeStamp != null;
            }
        }

        /// <summary>
        /// 客户已点餐品
        /// </summary>
        /// <remarks>同一餐品点多份按照多次点一份算</remarks>
        private List<DishEntity> orderedList;
    }
}
