using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOORouter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            BOORouterServer server = new BOORouterServer();
            var t = new Thread(new ThreadStart(server.BeginAsyncAccept)) {IsBackground = true};
            t.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //http://localhost:8080/resourceService/gateway?action=connect&userid=admin&password=Se4tMaQCi9gr0Q2usp7P56Sk5vM=
            var args = new Dictionary<string, string>
            {
                {"action", "connect"},
                {"userid", "admin"},
                {"password", "Se4tMaQCi9gr0Q2usp7P56Sk5vM="}
            };
            BOORouterClient.PostAndFetch("http://localhost:8080/resourceService/gateway", args, out string res);
            Console.WriteLine(res);
        }
    }
}
