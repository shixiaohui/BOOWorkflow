using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BOODemo.View
{
    /// <summary>
    /// 窗体：服务员窗体
    /// </summary>
    public partial class WaiterForm : Form
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public WaiterForm()
        {
            InitializeComponent();
            this.listBox1.DataSource = ViewModel.RestaurantViewModel.RestaurantEntity.GuestOrderList;
            this.listBox1.DisplayMember = "OrderId";
            
        }

        /// <summary>
        /// 按钮：Exit System
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// 按钮：Add Item
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 按钮：Make Payment
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 按钮：Create Order
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
