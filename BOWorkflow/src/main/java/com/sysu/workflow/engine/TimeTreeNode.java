package com.sysu.workflow.engine;

import com.sysu.workflow.SCXMLExecutionContext;

import java.util.ArrayList;

/**
 * Created by Rinkako on 2017/3/15.
 */
public class TimeTreeNode {
    public TimeTreeNode(String filename, String timeId, SCXMLExecutionContext exect, TimeTreeNode parent) {
        this.filename = filename;
        this.Parent = parent;
        this.timeId = timeId;
        this.exect = exect;
        this.Children = new ArrayList<TimeTreeNode>();
    }

    public void AddChild(TimeTreeNode ttn) {
        ttn.Parent = this;
        this.Children.add(ttn);
    }

    public ArrayList<TimeTreeNode> Children;

    public TimeTreeNode Parent;

    public String getTimeId() {
        return timeId;
    }

    public void setTimeId(String timeId) {
        this.timeId = timeId;
    }

    public SCXMLExecutionContext getExect() {
        return exect;
    }

    public void setExect(SCXMLExecutionContext exect) {
        this.exect = exect;
    }

    public String getFilename() {
        return filename;
    }

    public void setFilename(String filename) {
        this.filename = filename;
    }

    private String timeId;

    private String filename;

    private SCXMLExecutionContext exect;
}