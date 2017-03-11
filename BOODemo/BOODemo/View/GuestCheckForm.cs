using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BOODemo.ViewModel;

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
                var gOrder = RestaurantViewModel.RestaurantEntity.GuestOrderList.Find(
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
            if (this.listBox1.SelectedIndex != -1)
            {
                var dr = MessageBox.Show("are you SURE to finish payment?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    var phList = RestaurantViewModel.ActiveTaskHandlerList.FindAll((t) => t.GetType().ToString().Contains("PaymentTaskHandler"));
                    var ph = phList.Find((t) => ((TaskWarehouse.PaymentTaskHandler)t).GuestOrder.OrderId == this.listBox1.SelectedIndex);
                    ((TaskWarehouse.PaymentTaskHandler)ph).MadePayment();
                    this.RefreshCheckOrderList();
                }
            }
        }

        /// <summary>
        /// 刷新代付款订单列表
        /// </summary>
        public void RefreshCheckOrderList()
        {
            this.listBox1.Items.Clear();
            var c = from or in RestaurantViewModel.RestaurantEntity.GuestOrderList
                    where or.IsRequestPayment && !or.IsPaid
                    select or;
            foreach (var cObj in c)
            {
                this.listBox1.Items.Add(cObj.OrderId.ToString());
            }
        }
    }
}
