using System;
using System.Windows.Forms;

using com.sysu.workflow;
using com.sysu.workflow.io;
using com.sysu.workflow.env;
using com.sysu.workflow.model;
using com.sysu.workflow.env.jexl;

using BOODemo.Core;

namespace BOODemo.View
{
    extern alias OpenJDKCore;
    public partial class MainForm : Form
    {

        SCXMLExecutor executor;
        EngineBridge eb;
        StateMachineMessageHandler MsgHandler;

        public MainForm()
        {
            InitializeComponent();



            //eb = EngineBridge.GetInstance();
            //MsgHandler = new StateMachineMessageHandler();
            //OpenJDKCore.java.net.URL url = new OpenJDKCore.java.net.URL("file", String.Empty, "helloworld.xml");
            //SCXML scxml = SCXMLReader.read(url);
            //Evaluator ev = new JexlEvaluator();
            //executor = new SCXMLExecutor(ev, new MulitStateMachineDispatcher(), new SimpleErrorReporter());
            //executor.setStateMachine(scxml);
            //eb.SetExecutorReference(0, executor);
            //eb.Init(MsgHandler);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //executor.go();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //eb.SendEventAndTrigger(0, "gotoEnd", null);
        }
    }
}
