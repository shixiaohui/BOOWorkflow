﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOODemo.Model
{
    /// <summary>
    /// 实体类：一种料理
    /// </summary>
    [Serializable]
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

        /// <summary>
        /// 获取或设置该实体绑定的厨房餐单id
        /// </summary>
        public int KitchenOrderId
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置餐品是否通过品测
        /// </summary>
        public bool PassedQT
        {
            get;
            set;
        }
    }
}