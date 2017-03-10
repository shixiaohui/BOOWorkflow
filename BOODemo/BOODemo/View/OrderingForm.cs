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
    public partial class OrderingForm : Form
    {
        /// <summary>
        /// 点餐窗口所绑定的单号
        /// </summary>
        public int BindingGuestOrderId { get; set; }

        public OrderingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 按钮：Bean Milk
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Bean Milk");
        }

        /// <summary>
        /// 按钮：Bum
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Bum");
        }

        /// <summary>
        /// 按钮：Layer Cake
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Layer Cake");
        }

        /// <summary>
        /// 按钮：Fried Rice
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Fried Rice");
        }

        /// <summary>
        /// 按钮：Noodles
        /// </summary>
        private void button7_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Noodles");
        }

        /// <summary>
        /// 按钮：Tart
        /// </summary>
        private void button8_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Tart");
        }

        /// <summary>
        /// 按钮：Pie
        /// </summary>
        private void button9_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Pie");
        }

        /// <summary>
        /// 按钮：Pasta
        /// </summary>
        private void button10_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Pasta");
        }

        /// <summary>
        /// 按钮：Ice Cream
        /// </summary>
        private void button11_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("Ice Cream");
        }

        /// <summary>
        /// 按钮：Remove
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                this.listBox1.Items.RemoveAt(this.listBox1.SelectedIndex);
                this.listBox1.SelectedIndex = -1;
            }
        }
    }
}
