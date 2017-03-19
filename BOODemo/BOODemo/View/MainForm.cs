using System;
using System.Windows.Forms;
using BOODemo.ViewModel;

namespace BOODemo.View
{
    extern alias OpenJDKCore;
    /// <summary>
    /// Form: the initial form, configure the controller to reference the rest of the form
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Constructor
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
        /// Event: After the form is loaded
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
