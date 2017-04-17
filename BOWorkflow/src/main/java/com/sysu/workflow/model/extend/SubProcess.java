package com.sysu.workflow.model.extend;

import com.sysu.workflow.ActionExecutionContext;
import com.sysu.workflow.SCXMLExpressionException;
import com.sysu.workflow.model.Action;
import com.sysu.workflow.model.ModelException;

/**
 * 子过程类，状态转移或进入动作调用一个子过程
 * Created by LittleHuiHui on 2017/4/15.
 */
public class SubProcess extends Action {
    /**
     * 子过程的ID
     */
    private String id;
    /**
     * 子过程的name
     */
    private String name;
    /**
     * 过程绑定的事件
     */
    private String event;
    /**
     * 子过程的xml描述文件
     */
    private String src;
    /**
     * 过程调用开始时间，不需要用户定义的，调用过程时自动保存在数据库
     */
    private String startTime;
    /**
     * 过程调用结束时间，不需要用户定义的，调用完成时自动保存在数据库
     */
    private String endTime;

    /**
     *
     * @return
     */
    public String getId() {
        return id;
    }

    /**
     *
     * @param id
     */
    public void setId(String id) {
        this.id = id;
    }

    /**
     *
     * @return
     */
    public String getName() {
        return name;
    }

    /**
     *
     * @param name
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     *
     * @return
     */
    public String getEvent() {
        return event;
    }

    /**
     *
     * @param event
     */
    public void setEvent(String event) {
        this.event = event;
    }

    public String getSrc() {
        return src;
    }

    public void setSrc(String src) {
        this.src = src;
    }

    public String getStartTime() {
        return startTime;
    }

    public void setStartTime(String startTime) {
        this.startTime = startTime;
    }

    public String getEndTime() {
        return endTime;
    }

    /**
     *
     * @param endTime
     */
    public void setEndTime(String endTime) {
        this.endTime = endTime;
    }

    /**
     *
     * @param exctx The ActionExecutionContext for this execution instance
     * @throws ModelException
     * @throws SCXMLExpressionException
     */
    public void execute(ActionExecutionContext exctx) throws ModelException, SCXMLExpressionException{

    }
}
