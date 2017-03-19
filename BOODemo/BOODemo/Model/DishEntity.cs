using System;

namespace BOODemo.Model
{
    /// <summary>
    /// Entity class: dish
    /// </summary>
    [Serializable]
    internal sealed class DishEntity
    {
        /// <summary>
        /// Gets or sets the name of the dish
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the price of the dish
        /// </summary>
        public double Price
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the id of the kitchen oder that bound by the entity
        /// </summary>
        public int KitchenOrderId
        {
            get { return this.kitchenOrderId; }
            set { this.kitchenOrderId = value; }
        }

        private int kitchenOrderId = -1;

        /// <summary>
        /// Gets or sets whether the dish pass the test quality or not
        /// </summary>
        public bool PassedQT
        {
            get;
            set;
        }

        /// <summary>
        /// Clone object
        /// </summary>
        /// <returns>A clone of the entity</returns>
        public DishEntity Clone()
        {
            var DishClone = new DishEntity()
            {
                Name = this.Name,
                Price = this.Price,
                KitchenOrderId = this.KitchenOrderId,
                PassedQT = this.PassedQT
            };
            return DishClone;
        }
    }
}
