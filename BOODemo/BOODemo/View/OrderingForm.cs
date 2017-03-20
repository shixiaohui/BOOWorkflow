using System;
using System.Windows.Forms;
using BOODemo.Model;
using BOODemo.ViewModel;
using BOODemo.TaskWarehouse;

namespace BOODemo.View
{
    public partial class OrderingForm : Form
    {
        /// <summary>
        /// The single date of the order window
        /// </summary>
        private int guestId;

        /// <summary>
        /// The form binds the user order
        /// </summary>
        public int BindingGuestOrderId
        {
            get
            {
                return this.guestId;
            }
            set
            {
                this.guestId = value;
                this.Text = "Ordering [" + guestId + "]";
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newGuest">是否为新客人创建订单</param>
        public OrderingForm(bool newGuest)
        {
            InitializeComponent();
            this.isNewGuest = newGuest;
        }

        /// <summary>
        /// Whether he is a new guest
        /// </summary>
        private bool isNewGuest;

        /// <summary>
        /// Button：Bean Milk
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Bean Milk");
        }

        /// <summary>
        /// Button：Bum
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Bum");
        }

        /// <summary>
        /// Button：Layer Cake
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Layer Cake");
        }

        /// <summary>
        /// Button：Fried Rice
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Fried Rice");
        }

        /// <summary>
        /// Button：Noodles
        /// </summary>
        private void button7_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Noodles");
        }

        /// <summary>
        /// Button：Tart
        /// </summary>
        private void button8_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Tart");
        }

        /// <summary>
        /// Button：Pie
        /// </summary>
        private void button9_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Pie");
        }

        /// <summary>
        /// Button：Pasta
        /// </summary>
        private void button10_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Pasta");
        }

        /// <summary>
        /// Button：Ice Cream
        /// </summary>
        private void button11_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Ice Cream");
        }

        /// <summary>
        /// Button：Remove
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                this.listBox1.Items.RemoveAt(this.listBox1.SelectedIndex);
                this.listBox1.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Button：Submit
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            // Add the dish to the guest order
            GuestOrderEntity gOrder = RestaurantViewModel.RestaurantEntity.GuestOrderList.Find((t) => t.OrderId == this.BindingGuestOrderId);
            for (int i = 0; i < this.listBox1.Items.Count; i++)
            {
                var cDish = RestaurantViewModel.RestaurantEntity.Menu.Find(
                    (t) => String.Compare(t.Name, this.listBox1.Items[i].ToString(), true) == 0);
                gOrder.AddDish(cDish.Clone());
            }
            // Generate a Kitchen Order
            KitchenOrderEntity kOrder = new KitchenOrderEntity(this.BindingGuestOrderId);
            RestaurantViewModel.RestaurantEntity.KitchenOrderList.Add(kOrder);
            // State transition
            var handler = RestaurantViewModel.ActiveTaskHandlerList.Find(
                (x) => ((x is AddItemTaskHandler) && ((AddItemTaskHandler)x).GuestOrderId == gOrder.OrderId)) as AddItemTaskHandler;
            handler.Submit();
            // Refresh the front end
            RestaurantViewModel.WaiterFormReference.RefreshOrderList();
            this.Close();
        }
    }
}
