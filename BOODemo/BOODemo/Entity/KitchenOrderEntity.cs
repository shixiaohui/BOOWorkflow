using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOODemo.Entity
{
    /// <summary>
    /// 实体类：厨房餐单
    /// </summary>
    internal sealed class KitchenOrderEntity
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public KitchenOrderEntity(int bindingGuestOrderId)
        {
            lock (GlobalDataContext.KitchenOrderCounterMutex)
            {
                this.Id = GlobalDataContext.KitchenOrderIdCounter++;
            }
            this.CreateTimeStamp = DateTime.Now;
            this.FinishedTimeStamp = null;
            this.PendingList = new List<DishEntity>();
            this.QTList = new List<DishEntity>();
            this.WaitForDeliverList = new List<DishEntity>();
            this.DeliveringList = new List<DishEntity>();
            this.ArrivedList = new List<DishEntity>();
        }

        /// <summary>
        /// 将一道料理准备好并等待品测
        /// </summary>
        /// <param name="pendingIdx">该料理在待处理向量的位置</param>
        public void ProduceDish(int pendingIdx)
        {
            var rObj = this.PendingList[pendingIdx];
            this.PendingList.RemoveAt(pendingIdx);
            this.QTList.Add(rObj);
        }

        /// <summary>
        /// 将一道品测未通过的料理返回待处理队列
        /// </summary>
        /// <param name="qtIdx">该料理在待品测向量的位置</param>
        public void NotpassQTDish(int qtIdx)
        {
            var rObj = this.QTList[qtIdx];
            this.QTList.RemoveAt(qtIdx);
            this.PendingList.Add(rObj);
        }

        /// <summary>
        /// 将一道品测通过的料理放入递送队列
        /// </summary>
        /// <param name="qtIdx">该料理在待品测向量的位置</param>
        public void PassQTDish(int qtIdx)
        {
            var rObj = this.QTList[qtIdx];
            this.QTList.RemoveAt(qtIdx);
            this.WaitForDeliverList.Add(rObj);
        }

        /// <summary>
        /// 将一道菜递出
        /// </summary>
        /// <param name="wdIdx">该料理在待递送向量的位置</param>
        public void DeliverDish(int wdIdx)
        {
            var rObj = this.WaitForDeliverList[wdIdx];
            this.WaitForDeliverList.RemoveAt(wdIdx);
            this.DeliveringList.Add(rObj);
        }

        /// <summary>
        /// 一道菜已经递送完毕
        /// </summary>
        /// <param name="dIdx">该料理在待到达向量的位置</param>
        public void ArriveDish(int dIdx)
        {
            var rObj = this.DeliveringList[dIdx];
            this.DeliveringList.RemoveAt(dIdx);
            this.ArrivedList.Add(rObj);
        }

        /// <summary>
        /// 完成本厨房餐单
        /// </summary>
        public void Finish()
        {
            this.FinishedTimeStamp = DateTime.Now;
        }

        /// <summary>
        /// 获取该厨房餐单是否已经完成
        /// </summary>
        public bool IsFinish
        {
            get
            {
                return this.FinishedTimeStamp != null;
            }
        }

        /// <summary>
        /// 获取厨房餐单唯一编号
        /// </summary>
        public int Id
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取厨房餐单所绑定的客户订单ID
        /// </summary>
        public int GuestOrderId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取厨房餐单被创建的时间戳
        /// </summary>
        public DateTime CreateTimeStamp
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取厨房餐单完成的时间戳
        /// </summary>
        public DateTime? FinishedTimeStamp
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取待料理向量
        /// </summary>
        public List<DishEntity> PendingList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取待品测向量
        /// </summary>
        public List<DishEntity> QTList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取待递送向量
        /// </summary>
        public List<DishEntity> WaitForDeliverList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取递送途中向量
        /// </summary>
        public List<DishEntity> DeliveringList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取递送成功向量
        /// </summary>
        public List<DishEntity> ArrivedList
        {
            get;
            private set;
        }
    }
}
