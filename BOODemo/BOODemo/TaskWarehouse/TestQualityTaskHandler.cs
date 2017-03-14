using System;
using System.Collections.Generic;
using BOODemo.ViewModel;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// 任务解决器：厨房餐单品测
    /// </summary>
    internal sealed class TestQualityTaskHandler : TaskUtils.AbstractTaskHandler  
    {
        /// <summary>
        /// 初始化任务处理器
        /// </summary>
        /// <param name="paraDict">参数字典，键是形参，键值是实参对象</param>
        /// <returns>初始化任务是否成功</returns>
        public override bool Init(Dictionary<string, object> paraDict)
        {
            try
            {
                this.KitchenOrderId = (int)paraDict["kitchenOrderId"];
                this.bindingGuestOrderId = (int)paraDict["guestOrderId"];
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 开始处理任务
        /// </summary>
        /// <returns>任务是否成功开始</returns>
        public override bool Begin()
        {
            var kOrder = RestaurantViewModel.RestaurantEntity.KitchenOrderList.Find((x) => x.Id == this.KitchenOrderId);
            for (int i = 0; i < kOrder.PendingList.Count; i++)
            {
                kOrder.ProducedDish(i);
            }
            kOrder.PendingList.Clear();
            RestaurantViewModel.KitchenFormReference.Refresh();
            return true;
        }

        /// <summary>
        /// 获取任务处理的返回结果
        /// </summary>
        /// <param name="result">[out] 返回结果的包装</param>
        /// <returns>是否成功获取到了要返回的执行结果</returns>
        public override bool GetResult(out object result)
        {
            if (this.isPass == null)
            {
                result = null;
                return false;
            }
            var resPack = new TestQualityTaskResultPackage();
            resPack.passed = (bool)this.isPass ? "1" : "0";
            result = resPack;
            return true;
        }

        /// <summary>
        /// 设置是否品测通过
        /// </summary>
        public bool IsQualityTestPass
        {
            get
            {
                if (this.isPass == null)
                {
                    throw new FieldAccessException();
                }
                return (bool)this.isPass;
            }
            set
            {
                this.isPass = value;
                var ko = RestaurantViewModel.RestaurantEntity.KitchenOrderList.Find((x) => x.Id == this.KitchenOrderId);
                if ((bool)this.isPass)
                {
                    for (int i = 0; i < ko.QTList.Count; i++)
                    {
                        ko.QTList[i].PassedQT = true;
                    }
                }
                else
                {
                    for (int i = 0; i < ko.QTList.Count; i++)
                    {
                        ko.NotpassQTDish(i);
                    }
                    ko.QTList.Clear();
                }
                RestaurantViewModel.KitchenFormReference.Refresh();
                this.isFinished = true;
            }
        }

        /// <summary>
        /// 厨房餐单id
        /// </summary>
        public int KitchenOrderId = -1;

        /// <summary>
        /// 绑定的客户订单id
        /// </summary>
        private int bindingGuestOrderId = -1;

        /// <summary>
        /// 品测是否通过标记位
        /// </summary>
        private bool? isPass = null;

    }

    /// <summary>
    /// 品测结果包装
    /// </summary>
    public class TestQualityTaskResultPackage
    {
        /// <summary>
        /// 品测是否通过标记，0为失败，1为成功
        /// </summary>
        public string passed;
    }
}
