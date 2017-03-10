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
    /// 窗体：收银台
    /// </summary>
    public partial class GuestCheckForm : Form
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public GuestCheckForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 事件：列表选择改变
        /// </summary>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                var gOrder = ViewModel.RestaurantViewModel.RestaurantEntity.GuestOrderList.Find(
                    (t) => t.OrderId.ToString() == this.listBox1.SelectedItem.ToString());
                if (gOrder != null)
                {
                    this.textBox1.Text = gOrder.ToString();
                    this.label3.Text = String.Format("Total: {0}", gOrder.TotalPrice);
                }
            }
        }

        /// <summary>
        /// 按钮：Paid
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
