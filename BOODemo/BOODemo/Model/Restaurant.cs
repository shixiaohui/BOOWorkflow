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
        /// 构造器
        /// </summary>
        public Restaurant()
        {
            this.Menu = new List<DishEntity>();
            this.GuestOrderList = new List<GuestOrderEntity>();
            this.KitchenOrderList = new List<KitchenOrderEntity>();
            this.InitMenu();
        }

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

        /// <summary>
        /// 获取菜单
        /// </summary>
        public List<DishEntity> Menu
        {
            get;
            private set;
        }

        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// <remarks>For test only, data should be read from datasource</remarks>
        private void InitMenu()
        {
            Menu.Add(new DishEntity() { Name = "Bean Milk", Price = 1 });
            Menu.Add(new DishEntity() { Name = "Bum", Price = 1.5 });
            Menu.Add(new DishEntity() { Name = "Layer Cake", Price = 2.5 });
            Menu.Add(new DishEntity() { Name = "Fried Rice", Price = 3 });
            Menu.Add(new DishEntity() { Name = "Noodles", Price = 4.5 });
            Menu.Add(new DishEntity() { Name = "Tart", Price = 3.5 });
            Menu.Add(new DishEntity() { Name = "Pie", Price = 1 });
            Menu.Add(new DishEntity() { Name = "Pasta", Price = 4 });
            Menu.Add(new DishEntity() { Name = "Ice Cream", Price = 1 });
        }
    }
}
