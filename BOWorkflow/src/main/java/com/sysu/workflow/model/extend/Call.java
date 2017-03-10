package com.sysu.workflow.model.extend;

import com.sysu.workflow.*;
import com.sysu.workflow.model.Action;
import com.sysu.workflow.model.ModelException;

import java.io.Serializable;
import java.util.List;

/**
 * Call标签类
 * Created by Rinkako on 2017/3/8.
 */
public class Call extends Action implements Serializable {
    /**
     * Serial version UID.
     */
    private static final long serialVersionUID = 1L;

    /**
     * The name of task to call
     */
    private String name;

    /**
     * The parameter string
     */
    private String params;

    /**
     * Get the value of name
     * @return the task name to call
     */
    public String getName() {
        return name;
    }

    /**
     * Set the value of name
     * @param name the task name to call
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * Get the value of params
     * @return the task parameter string
     */
    public String getParams() {
        return params;
    }

    /**
     * Set the value of params
     * @param params the task patameter
     */
    public void setParams(String params) {
        this.params = params;
    }

    /**
     * Execute RPC
     * @param exctx The ActionExecutionContext for this execution instance
     * @throws ModelException
     * @throws SCXMLExpressionException
     */
    @Override
    public void execute(ActionExecutionContext exctx) throws ModelException, SCXMLExpressionException {
        SCXMLExecutionContext scxmlExecContext = (SCXMLExecutionContext)exctx.getInternalIOProcessor();
        Tasks tasks = scxmlExecContext.getSCXMLExecutor().getStateMachine().getTasks();
        if (tasks != null) {
            List<Task> tLists = tasks.getTaskList();
            boolean successFlag = false;
            for (Task t : tLists) {
                if (t.getName().equals(this.name)) {
                    // Send Message to APP
                    String dasher = "";
                    if (t.getRole() != null) {
                        dasher = t.getRole();
                    } else if (t.getAssignee() != null) {
                        dasher = t.getAssignee();
                    }
                    EngineBridge.QuickEnqueueBOMessage(scxmlExecContext.getSCXMLExecutor().getExecutorIndex(),
                            this.name, this.params, dasher, t.getEvent());
                    successFlag = true;
                    break;
                }
            }
            if (successFlag == false) {
                throw new ModelException();
            }
        }
        else {
            throw new ModelException();
        }
    }
}
