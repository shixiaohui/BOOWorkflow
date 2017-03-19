using System;
using System.Collections.Generic;

namespace BOODemo.Model
{
    /// <summary>
    /// Entity class: restaurant, for all the activities of the restaurant to provide containers
    /// </summary>
    [Serializable]
    internal sealed class Restaurant
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Restaurant()
        {
            this.Menu = new List<DishEntity>();
            this.GuestOrderList = new List<GuestOrderEntity>();
            this.KitchenOrderList = new List<KitchenOrderEntity>();
            this.InitMenu();
        }

        /// <summary>
        /// Gets cashier
        /// </summary>
        public CasherEntity Casher
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the guest order vector
        /// </summary>
        public List<GuestOrderEntity> GuestOrderList
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the kitcher order vector
        /// </summary>
        public List<KitchenOrderEntity> KitchenOrderList
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets menu
        /// </summary>
        public List<DishEntity> Menu
        {
            get;
            private set;
        }

        /// <summary>
        /// Initialize the menu
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
