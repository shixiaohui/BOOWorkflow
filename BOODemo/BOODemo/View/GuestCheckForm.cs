using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using BOODemo.ViewModel;

namespace BOODemo.View
{
    /// <summary>
    /// Form: Cashier
    /// </summary>
    public partial class GuestCheckForm : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GuestCheckForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Refresh the payment order list
        /// </summary>
        public void RefreshCheckOrderList()
        {
            Thread t = new Thread(new ThreadStart(this.RefreshPayingOrderHandler));
            t.Start();
        }

        /// <summary>
        /// Event: List selection changes
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
        /// Butten：Paid
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                var dr = MessageBox.Show("are you SURE to finish payment?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    var phList = RestaurantViewModel.ActiveTaskHandlerList.FindAll((t) => t.GetType().ToString().Contains("PaymentTaskHandler"));
                    var ph = phList.Find((t) => ((TaskWarehouse.PaymentTaskHandler)t).GuestOrder.OrderId == Convert.ToInt32(this.listBox1.SelectedItem.ToString()));
                    ((TaskWarehouse.PaymentTaskHandler)ph).MadePayment();
                    this.textBox1.Text = String.Empty;
                    this.RefreshCheckOrderList();
                    RestaurantViewModel.WaiterFormReference.RefreshOrderList();
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
                this.Invoke(new RefreshCallBack(this.RefreshPayingOrderHandler));
            }
            else
            {
                this.listBox1.Items.Clear();
                var c = from or in RestaurantViewModel.RestaurantEntity.GuestOrderList
                        where or.IsRequestPayment && !or.IsPaid
                        select or;
                foreach (var cObj in c)
                {
                    this.listBox1.Items.Add(cObj.OrderId.ToString());
                }
                this.listBox1.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Handle asynchronous refresh
        /// </summary>
        private void RefreshPayingOrderHandler()
        {
            this.RefreshHandler();
        }

        /// <summary>
        /// Asynchronous refresh delegate
        /// </summary>
        private delegate void RefreshCallBack();
    }
}
