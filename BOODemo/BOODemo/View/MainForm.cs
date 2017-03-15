using System;
using System.Windows.Forms;
using BOODemo.ViewModel;

namespace BOODemo.View
{
    extern alias OpenJDKCore;
    /// <summary>
    /// 窗体：初始窗体，配置控制器对其余窗体的引用
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            WaiterForm wf = new WaiterForm();
            KitchenForm kf = new KitchenForm();
            GuestCheckForm gcf = new GuestCheckForm();
            RestaurantViewModel.WaiterFormReference = wf;
            RestaurantViewModel.KitchenFormReference = kf;
            RestaurantViewModel.GuestCheckFormReference = gcf;
            wf.Show();
            kf.Show();
            gcf.Show();
        }
        
        /// <summary>
        /// 事件：窗体载入后
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
