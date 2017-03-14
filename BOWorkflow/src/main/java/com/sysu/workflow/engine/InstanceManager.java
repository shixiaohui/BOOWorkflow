package com.sysu.workflow.engine;

import com.sysu.workflow.SCXMLExecutionContext;
import com.sysu.workflow.SCXMLExecutor;

/**
 * Created by Rinkako on 2017/3/15.
 */
public class InstanceManager {

    public static SCXMLExecutor GetExecutor(String tid) {
        return InstanceManager.GetExecContext(tid).getSCXMLExecutor();
    }

    public static SCXMLExecutionContext GetExecContext(String tid) {
        return InstanceManager.InstanceTree.GetNodeById(tid).getExect();
    }

    public static TimeInstanceTree InstanceTree = new TimeInstanceTree();
}
