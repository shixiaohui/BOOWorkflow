package com.sysu.workflow.model.extend;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

/**
 * 任务容器类：为任务标签下的业务流程对象提供容器
 * Created by Rinkako on 2017/3/7.
 */
public class Tasks implements Serializable {
    /**
     * Serial version UID.
     */
    private static final long serialVersionUID = 1L;

    /**
     * The set of &lt;task&gt; elements, parsed as Elements, that are
     * children of this &lt;tasks&gt; element.
     */
    private List<Task> tasklist;

    /**
     * Constructor.
     */
    public Tasks() {
        this.tasklist = new ArrayList<Task>();
    }

    /**
     * Get all the data children of this task container.
     *
     * @return Returns the task.
     */
    public final List<Task> getTaskList() {
        return this.tasklist;
    }

    /**
     * Add a Task.
     *
     * @param taskObj The task child to be added.
     */
    public final void addData(final Task taskObj) {
        if (taskObj != null) {
            this.tasklist.add(taskObj);
        }
    }
}
