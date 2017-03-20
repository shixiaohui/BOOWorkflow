using System;
using System.Threading;
using System.Windows.Forms;
using BOODemo.Model;
using BOODemo.ViewModel;

namespace BOODemo.View
{
    /// <summary>
    /// Form: Servant form
    /// </summary>
    public partial class WaiterForm : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WaiterForm()
        {
            InitializeComponent();
            buttonTip.ShowAlways = true;
            this.button1.Enabled = this.button2.Enabled = false;
        }

        /// <summary>
        /// Refresh customer does not complete order
        /// </summary>
        public void RefreshOrderList()
        {
            Thread t = new Thread(new ThreadStart(this.RefreshActiveOrderHandler));
            t.Start();
        }

        /// <summary>
        /// Button：Exit System
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Button：Add Item
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                RestaurantViewModel.Send(this.listBox1.SelectedItem.ToString(), "addItem", null);
                OrderingForm of = new OrderingForm(false);
                of.BindingGuestOrderId = Convert.ToInt32(this.listBox1.SelectedItem.ToString());
                of.ShowDialog(this);
            }
        }

        /// <summary>
        /// Button：Make Payment
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                if (MessageBox.Show("Sure?", "Information", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MessageBox.Show("Please make payment at Casher.");
                    this.button1.Enabled = false;
                    RestaurantViewModel.Send(this.listBox1.SelectedItem.ToString(), "requestCheck", null);
                }
            }
        }

        /// <summary>
        /// Button：Create Order
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            var execId = RestaurantViewModel.CreateBussinessObjectInstance();
            GuestOrderEntity gOrder = new GuestOrderEntity(execId);
            RestaurantViewModel.RestaurantEntity.GuestOrderList.Add(gOrder);
            RestaurantViewModel.executorDict[execId].go();
            OrderingForm of = new OrderingForm(true);
            of.BindingGuestOrderId = gOrder.OrderId;
            of.ShowDialog(this);
        }

        /// <summary>
        /// Event: List Click item to change
        /// </summary>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                var orderObj = RestaurantViewModel.RestaurantEntity.GuestOrderList.Find(
                    (t) => t.OrderId.ToString() == this.listBox1.SelectedItem.ToString());
                this.button1.Enabled = this.button2.Enabled = true;
                buttonTip.SetToolTip(this.button1, "Add dishes for this order.");

                var kOrderList = RestaurantViewModel.RestaurantEntity.KitchenOrderList.FindAll(
                    (t) => t.GuestOrderId.ToString() == this.listBox1.SelectedItem.ToString());
                if (kOrderList.Count != 0 && kOrderList.TrueForAll((t) => t.IsFinish) == false)
                {
                    this.button1.Enabled = this.button2.Enabled = false;
                    buttonTip.SetToolTip(this.button1, "Now dishes are producing, please add item after all ordered dishes are deliverd.");
                }
                this.textBox1.Text = orderObj.ToString();
                if (orderObj.IsRequestPayment == true)
                {
                    this.button1.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Handle cross-thread refresh
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
        /// Handle asynchronous refresh
        /// </summary>
        private void RefreshActiveOrderHandler()
        {
            this.RefreshHandler();
        }

        /// <summary>
        /// Asynchronous refresh delegate
        /// </summary>
        private delegate void RefreshCallBack();

        /// <summary>
        /// Button prompt
        /// </summary>
        private ToolTip buttonTip = new ToolTip();
    }
}
