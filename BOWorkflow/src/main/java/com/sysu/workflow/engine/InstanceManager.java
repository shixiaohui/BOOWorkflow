package com.sysu.workflow.engine;

import com.sysu.workflow.SCXMLExecutionContext;
import com.sysu.workflow.SCXMLExecutor;

/**
 * 实例管理器类：为业务对象的实例树提供公共的上下文访问
 * Created by Rinkako on 2017/3/15.
 */
public class InstanceManager {
    /**
     * 通过唯一编号获取实例树对应节点上的执行器
     * @param tid 节点的唯一编号
     * @return 对应节点上的执行器
     */
    public static SCXMLExecutor GetExecutor(String tid) {
        return InstanceManager.GetExecContext(tid).getSCXMLExecutor();
    }

    /**
     * 通过唯一编号获取实例树对应节点上的执行器上下文
     * @param tid 节点的唯一编号
     * @return 对应节点上的执行器上下文
     */
    public static SCXMLExecutionContext GetExecContext(String tid) {
        return InstanceManager.InstanceTree.GetNodeById(tid).getExect();
    }

    /**
     * 业务对象的实例树
     */
    public static TimeInstanceTree InstanceTree = new TimeInstanceTree();
}
