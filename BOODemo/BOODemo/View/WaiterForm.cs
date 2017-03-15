using System;
using System.Threading;
using System.Windows.Forms;
using BOODemo.Model;
using BOODemo.ViewModel;

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
            buttonTip.ShowAlways = true;
            this.button1.Enabled = this.button2.Enabled = false;
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
            if (this.listBox1.SelectedIndex != -1)
            {
                RestaurantViewModel.Send(this.listBox1.SelectedItem.ToString(), "addItem", null);
            }
        }

        /// <summary>
        /// 按钮：Make Payment
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                RestaurantViewModel.Send(this.listBox1.SelectedItem.ToString(), "requestCheck", null);
            }
        }

        /// <summary>
        /// 按钮：Create Order
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            var execId = RestaurantViewModel.CreateBussinessObjectInstance();
            GuestOrderEntity gOrder = new GuestOrderEntity(execId);
            RestaurantViewModel.RestaurantEntity.GuestOrderList.Add(gOrder);
            RestaurantViewModel.executorDict[execId].go();
            OrderingForm of = new OrderingForm();
            of.BindingGuestOrderId = gOrder.OrderId;
            of.ShowDialog(this);
        }

        /// <summary>
        /// 事件：列表点选项目改变
        /// </summary>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                this.button1.Enabled = this.button2.Enabled = true;
                buttonTip.SetToolTip(this.button1, "Add dishes for this order.");

                var kOrderList = RestaurantViewModel.RestaurantEntity.KitchenOrderList.FindAll(
                    (t) => t.GuestOrderId.ToString() == this.listBox1.SelectedItem.ToString());
                if (kOrderList.Count != 0 && kOrderList.TrueForAll((t) => t.IsFinish) == false)
                {
                    this.button1.Enabled = this.button2.Enabled = false;
                    buttonTip.SetToolTip(this.button1, "Now dishes are producing, please add item after all ordered dishes are deliverd.");
                }

                var orderObj = RestaurantViewModel.RestaurantEntity.GuestOrderList.Find(
                    (t) => t.OrderId.ToString() == this.listBox1.SelectedItem.ToString());
                this.textBox1.Text = orderObj.ToString();
            }
        }

        /// <summary>
        /// 刷新客户未完成订单
        /// </summary>
        public void RefreshOrderList()
        {
            Thread t = new Thread(new ThreadStart(this.RefreshActiveOrderHandler));
            t.Start();
        }
        
        /// <summary>
        /// 处理跨线程刷新
        /// </summary>
        private void RefreshHandler()
        {
            if (this.listBox1.InvokeRequired)
            {
                this.Invoke(new RefreshCallBack(this.RefreshActiveOrderHandler));
            }
            else
            {
                this.listBox1.Items.Clear();
                var gOrderList = RestaurantViewModel.RestaurantEntity.GuestOrderList.FindAll((x) => x.IsPaid == false);
                for (int i = 0; i < gOrderList.Count; i++)
                {
                    this.listBox1.Items.Add(gOrderList[i].OrderId.ToString());
                }
                this.textBox1.Text = String.Empty;
                this.button1.Enabled = this.button2.Enabled = false;
                this.listBox1.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// 处理异步刷新
        /// </summary>
        private void RefreshActiveOrderHandler()
        {
            this.RefreshHandler();
        }

        /// <summary>
        /// 异步刷新委托
        /// </summary>
        private delegate void RefreshCallBack();

        /// <summary>
        /// 按钮提示语
        /// </summary>
        private ToolTip buttonTip = new ToolTip();
    }
}
