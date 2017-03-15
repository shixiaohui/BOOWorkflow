using System;
using System.Threading;
using System.Windows.Forms;
using BOODemo.ViewModel;
using BOODemo.TaskWarehouse;

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
        }

        /// <summary>
        /// 刷新餐单列表
        /// </summary>
        public void RefreshKitchenOrder()
        {
            Thread t = new Thread(new ThreadStart(this.RefreshKitchenOrderHandler));
            t.Start();
        }
        
        /// <summary>
        /// 处理异步刷新
        /// </summary>
        private void RefreshKitchenOrderHandler()
        {
            this.RefreshHandler();
        }

        /// <summary>
        /// 处理跨线程刷新
        /// </summary>
        private void RefreshHandler()
        {
            if (this.listBox1.InvokeRequired)
            {
                this.Invoke(new RefreshOrderCallBack(RefreshKitchenOrderHandler));
            }
            else
            {
                var kOrderProcessingList = RestaurantViewModel.RestaurantEntity.KitchenOrderList.FindAll((t) => t.IsFinish == false);
                this.listBox1.Items.Clear();
                foreach (var ko in kOrderProcessingList)
                {
                    this.listBox1.Items.Add(String.Format("Guest {0}:{1}", ko.GuestOrderId, ko.Id));
                }
                this.listBox1_SelectedIndexChanged(null, null);
            }
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
            }
            else
            {
                this.groupBox2.Enabled = false;
            }
        }

        /// <summary>
        /// 按钮：Produced
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1 && this.listBox2.Items.Count != 0)
            {
                var handler = (ProduceDishesTaskHandler)RestaurantViewModel.ActiveTaskHandlerList.Find((t) => (t is ProduceDishesTaskHandler) &&
                    ((ProduceDishesTaskHandler)t).KitchenOrderId.ToString() == this.listBox1.SelectedItem.ToString().Split(':')[1]);
                handler.Produced();
                this.listBox3.Items.Clear();
                for (int i = 0; i < this.listBox2.Items.Count; i++)
                {
                    this.listBox3.Items.Add(this.listBox2.Items[i]);
                }
                this.listBox2.Items.Clear();
            }
        }

        /// <summary>
        /// 按钮：NotPass
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1 && this.listBox3.Items.Count != 0)
            {
                var handler = (TestQualityTaskHandler)RestaurantViewModel.ActiveTaskHandlerList.Find((t) => (t is TestQualityTaskHandler) &&
                    ((TestQualityTaskHandler)t).KitchenOrderId.ToString() == this.listBox1.SelectedItem.ToString().Split(':')[1]);
                handler.IsQualityTestPass = false;
                this.listBox2.Items.Clear();
                for (int i = 0; i < this.listBox3.Items.Count; i++)
                {
                    this.listBox2.Items.Add(this.listBox3.Items[i]);
                }
                this.listBox3.Items.Clear();
            }
        }

        /// <summary>
        /// 按钮：Pass
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1 && this.listBox3.Items.Count != 0)
            {
                var handler = (TestQualityTaskHandler)RestaurantViewModel.ActiveTaskHandlerList.Find((t) => (t is TestQualityTaskHandler) &&
                    ((TestQualityTaskHandler)t).KitchenOrderId.ToString() == this.listBox1.SelectedItem.ToString().Split(':')[1]);
                handler.IsQualityTestPass = true;
                this.listBox4.Items.Clear();
                for (int i = 0; i < this.listBox3.Items.Count; i++)
                {
                    this.listBox4.Items.Add(this.listBox3.Items[i]);
                }
                this.listBox3.Items.Clear();
            }
        }

        /// <summary>
        /// 按钮：Deliver
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1 && this.listBox4.Items.Count != 0)
            {
                var handler = (DeliverTaskHandler)RestaurantViewModel.ActiveTaskHandlerList.Find((t) => (t is DeliverTaskHandler) &&
                    ((DeliverTaskHandler)t).KitchenOrderId.ToString() == this.listBox1.SelectedItem.ToString().Split(':')[1]);
                handler.Arrived();
                this.listBox4.Items.Clear();
                this.listBox1.SelectedIndex = -1;
                RestaurantViewModel.WaiterFormReference.RefreshOrderList();
            }
        }

        /// <summary>
        /// 异步刷新委托
        /// </summary>
        private delegate void RefreshOrderCallBack();
    }
}
