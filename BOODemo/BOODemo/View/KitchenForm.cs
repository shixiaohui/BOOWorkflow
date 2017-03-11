using System;
using System.Windows.Forms;
using BOODemo.ViewModel;

namespace BOODemo.View
{
    /// <summary>
    /// 窗体：厨房
    /// </summary>
    public partial class KitchenForm : Form
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public KitchenForm()
        {
            InitializeComponent();
            this.button6.Enabled = false;
        }

        /// <summary>
        /// 刷新餐单列表
        /// </summary>
        public void RefreshKitchenOrder()
        {
            var kOrderProcessingList = RestaurantViewModel.RestaurantEntity.KitchenOrderList.FindAll((t) => t.IsFinish == false);
            this.listBox1.Items.Clear();
            foreach (var ko in kOrderProcessingList)
            {
                this.listBox1.Items.Add(String.Format("Guest {0}:{1}", ko.GuestOrderId, ko.Id));
            }
            this.listBox1.SelectedIndex = -1;
            this.groupBox2.Enabled = false;
        }

        /// <summary>
        /// 事件：餐单列表点击
        /// </summary>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                this.groupBox2.Enabled = true;
                var selectKOrderId = this.listBox1.SelectedItem.ToString().Split(':')[1];
                var kOrder = RestaurantViewModel.RestaurantEntity.KitchenOrderList.Find((t) => t.Id == Convert.ToInt32(selectKOrderId));
                this.listBox2.Items.Clear();
                this.listBox3.Items.Clear();
                this.listBox4.Items.Clear();
                this.listBox5.Items.Clear();
                foreach (var obj in kOrder.PendingList)
                {
                    this.listBox2.Items.Add(obj.Name);
                }
                foreach (var obj in kOrder.QTList)
                {
                    this.listBox3.Items.Add(obj.Name);
                }
                foreach (var obj in kOrder.DeliveringList)
                {
                    this.listBox4.Items.Add(obj.Name);
                }
                foreach (var obj in kOrder.ArrivedList)
                {
                    this.listBox5.Items.Add(obj.Name);
                }
                this.button6.Enabled = (kOrder.PendingList.Count + kOrder.QTList.Count + kOrder.DeliveringList.Count == 0);
            }
            else
            {
                this.groupBox2.Enabled = false;
            }
        }
    }
}
