using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using com.sysu.workflow;
using com.sysu.workflow.io;
using com.sysu.workflow.env;
using com.sysu.workflow.model;
using com.sysu.workflow.env.jexl;
using com.sysu.workflow.entity;


namespace BOODemo
{
    extern alias OpenJDKCore;
    public partial class Form1 : Form
    {

        SCXMLExecutor executor;
        EngineBridge eb;
        public Form1()
        {
            InitializeComponent();

            OpenJDKCore.java.net.URL url = new OpenJDKCore.java.net.URL("file", "", "helloworld.xml");
            SCXML scxml = SCXMLReader.read(url);
            Evaluator ev = new JexlEvaluator();
            executor = new SCXMLExecutor(ev, new MulitStateMachineDispatcher(), new SimpleErrorReporter());
            executor.setStateMachine(scxml);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            executor.go();
            eb = EngineBridge.GetInstance();
            Queue<BOMessage> msgQueue = new Queue<BOMessage>();
            BOMessage bPtr;
            while ((bPtr = eb.GetPendingMessage()) != null)
            {
                msgQueue.Enqueue(bPtr);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            eb.SendEventAndTrigger("firststateToEnd");
        }
    }
}
