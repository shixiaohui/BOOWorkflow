using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using com.sysu.workflow;
using com.sysu.workflow.model;
using com.sysu.workflow.io;
using com.sysu.workflow.env.jexl;


namespace BOODemo
{
    extern alias OpenJDKCore;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            OpenJDKCore.java.net.URL url = new OpenJDKCore.java.net.URL("file", "", "helloworld.xml");
            SCXML scxml = SCXMLReader.read(url);
            SCXMLExecutor executor = new SCXMLExecutor();
            executor.setStateMachine(scxml);
            Evaluator ev = new JexlEvaluator();
            executor.setEvaluator(ev);
            executor.go();
        }
    }
}
