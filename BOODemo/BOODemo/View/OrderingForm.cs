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
    }
}
