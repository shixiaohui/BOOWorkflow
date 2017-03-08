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
     * The nameExpr of task to call
     */
    private String nameExpr;

    /**
     * The parameter string
     */
    private String params;

    /**
     * Get the value of nameExpr
     * @return the task nameExpr to call
     */
    public String getNameExpr() {
        return nameExpr;
    }

    /**
     * Set the value of nameExpr
     * @param nameExpr the task nameExpr to call
     */
    public void setNameExpr(String nameExpr) {
        this.nameExpr = nameExpr;
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
        Context ctx = exctx.getContext(getParentEnterableState());
        Evaluator eval = exctx.getEvaluator();
        ctx.setLocal(getNamespacesKey(), getNamespaces());
        String callingSign = String.valueOf(getTextContentIfNodeResult(eval.eval(ctx, this.nameExpr)));
        ctx.setLocal(getNamespacesKey(), null);
        SCXMLExecutionContext scxmlExecContext = (SCXMLExecutionContext)exctx.getInternalIOProcessor();
        Tasks tasks = scxmlExecContext.getSCXMLExecutor().getStateMachine().getTasks();
        List<Task> tLists = tasks.getTaskList();
        boolean successFlag = false;
        for (Task t : tLists) {
            if (t.getName().equals(callingSign)) {
                // Send Message to APP
                String dasher = "";
                if (t.getRole() != null) {
                    dasher = t.getRole();
                }
                else if (t.getAssignee() != null) {
                    dasher = t.getAssignee();
                }
                EngineBridge.QuickEnqueueBOMessage(callingSign, this.params, dasher, t.getEvent());
                successFlag = true;
                break;
            }
        }
        if (successFlag == false) {
            throw new ModelException();
        }
    }
}
