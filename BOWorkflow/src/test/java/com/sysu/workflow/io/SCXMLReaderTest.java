
package com.sysu.workflow.io;

import com.sysu.workflow.*;
import com.sysu.workflow.env.MulitStateMachineDispatcher;
import com.sysu.workflow.env.SimpleErrorReporter;
import com.sysu.workflow.env.jexl.JexlEvaluator;
import com.sysu.workflow.model.SCXML;
import org.junit.Assert;
import org.junit.Test;

import java.io.UnsupportedEncodingException;
import java.net.URL;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.HashMap;
import java.util.Map;

import org.apache.commons.codec.binary.Base64;

/**
 * Unit tests
 */
public class SCXMLReaderTest {

    public static synchronized String encrypt(String text)
            throws NoSuchAlgorithmException, UnsupportedEncodingException {
        MessageDigest md = MessageDigest.getInstance("SHA");
        md.update(text.getBytes("UTF-8"));
        byte raw[] = md.digest();
        return new Base64(-1).encodeToString(raw);            // -1 means no line breaks
    }


    public static synchronized String encrypt(String text, String defText) {
        if (defText == null) defText = text;
        try {
            return encrypt(text);
        }
        catch (Exception e) {
            return defText;
        }
    }







    @Test
    public void testSCXMLReader() throws Exception {

        String str = "YAWL";
        String tt = encrypt(str);

        //URL url = SCXMLTestHelper.getResource("helloworld.xml");
        URL url = new URL("file", "", "D:\\Documents\\GitProject\\BOOWorkflow\\BOWorkflow\\target\\classes\\GuestOrder.xml");
        SCXML scxml = new SCXMLReader().read(url);
        Evaluator evaluator = new JexlEvaluator();
        SCXMLExecutor executor = new SCXMLExecutor(evaluator, new MulitStateMachineDispatcher(), new SimpleErrorReporter());
        executor.setStateMachine(scxml);
        executor.go();

        TriggerEvent tEvt = new TriggerEvent("submit", TriggerEvent.SIGNAL_EVENT, null);
        executor.triggerEvent(tEvt);

        tEvt = new TriggerEvent("produced", TriggerEvent.SIGNAL_EVENT, null);
        executor.triggerEvent(tEvt);

        EventDataPackage edp = new EventDataPackage();
        edp.passed = "1";
        tEvt = new TriggerEvent("testCompleted", TriggerEvent.SIGNAL_EVENT, edp);
        executor.triggerEvent(tEvt);

        tEvt = new TriggerEvent("delivered", TriggerEvent.SIGNAL_EVENT, null);
        executor.triggerEvent(tEvt);

        tEvt = new TriggerEvent("archived", TriggerEvent.SIGNAL_EVENT, null);
        executor.triggerEvent(tEvt);

        tEvt = new TriggerEvent("requestCheck", TriggerEvent.SIGNAL_EVENT, null);
        executor.triggerEvent(tEvt);

        tEvt = new TriggerEvent("calculated", TriggerEvent.SIGNAL_EVENT, null);
        executor.triggerEvent(tEvt);

        tEvt = new TriggerEvent("paid", TriggerEvent.SIGNAL_EVENT, null);
        executor.triggerEvent(tEvt);

        tEvt = new TriggerEvent("archived", TriggerEvent.SIGNAL_EVENT, null);
        executor.triggerEvent(tEvt);

        Assert.assertNotNull(scxml);
    }

    public class EventDataPackage {
        public String passed;
    }


    @Test
    public void testExecutor() throws Exception {
/*
        URL url = SCXMLTestHelper.getResource("crowdsourcingTest.xml");
        SCXML scxml = new SCXMLReader().read(url);
        //实例化数据模型解析器
        Evaluator evaluator = new JexlEvaluator();


        //实例化引擎
        SCXMLExecutor executor = new SCXMLExecutor(evaluator, new MulitStateMachineDispatcher(), new SimpleErrorReporter());

        executor.setStateMachine(scxml);

        CrowdSourcingTaskEntity crowdSourcingTaskEntity = new CrowdSourcingTaskEntity();
        crowdSourcingTaskEntity.setTaskName("wode task name");
        crowdSourcingTaskEntity.setTaskDescription("wode task description");


        Context rootConext = evaluator.newContext(null);
        rootConext.set("crowdSourcingTaskEntity", crowdSourcingTaskEntity);
        executor.setRootContext(rootConext);

        executor.go();

        CrowdSourcingTaskEntity c = (CrowdSourcingTaskEntity) executor.getRootContext().get("crowdSourcingTaskEntity");*/

      /*  Map<String,Object> initData = new HashMap<String, Object>();
        initData.put("taskName","写一篇关于众包的文章");
        initData.put("taskDescription","不少于3000字");
        initData.put("judgeCount",3);
        initData.put("decomposeCount",2);
        initData.put("decomposeVoteCount",3);
        initData.put("solveCount",2);
        initData.put("solveVoteCount",3);

        executor.triggerEvent(new TriggerEvent("init", TriggerEvent.SIGNAL_EVENT, initData));
        */


        Map<String,Integer> map = new HashMap<String, Integer>();
        map.put("simple", 0);


        /*
        executor.triggerEvent(new TriggerEvent("judgeComplete", TriggerEvent.SIGNAL_EVENT,map));
        executor.triggerEvent(new TriggerEvent("judgeComplete", TriggerEvent.SIGNAL_EVENT,map));
        executor.triggerEvent(new TriggerEvent("judgeComplete", TriggerEvent.SIGNAL_EVENT,map));*/

    }


}

