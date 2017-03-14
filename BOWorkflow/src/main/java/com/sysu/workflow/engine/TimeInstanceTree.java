package com.sysu.workflow.engine;

import com.sysu.workflow.SCXMLExecutionContext;

import java.sql.Time;
import java.util.ArrayList;
import java.util.Stack;
import java.util.concurrent.BlockingQueue;

/**
 * Created by Rinkako on 2017/3/15.
 */
public class TimeInstanceTree {

    public TimeInstanceTree() {
        this.Root = null;
    }

    public void SetRoot(TimeTreeNode root) {
        this.Root = root;
    }

    public TimeTreeNode GetNodeById(String tid) {
        if (this.Root == null) {
            return null;
        }
        Stack<TimeTreeNode> searchStack = new Stack<TimeTreeNode>();
        searchStack.push(this.Root);
        while (!searchStack.empty()) {
            TimeTreeNode curNode = searchStack.pop();
            if (curNode.getTimeId().equals(tid)) {
                return curNode;
            }
            for (int i = 0; i < curNode.Children.size(); i++) {
                searchStack.push(curNode.Children.get(i));
            }
        }
        return null;
    }

    public ArrayList<TimeTreeNode> GetNodeVectorByTarget(String target) {
        if (this.Root == null) {
            return null;
        }
        ArrayList<TimeTreeNode> rVec = new ArrayList<TimeTreeNode>();
        Stack<TimeTreeNode> searchStack = new Stack<TimeTreeNode>();
        searchStack.push(this.Root);
        while (!searchStack.empty()) {
            TimeTreeNode curNode = searchStack.pop();
            if (curNode.getFilename().equals(target)) {
                rVec.add(curNode);
            }
            for (int i = 0; i < curNode.Children.size(); i++) {
                searchStack.push(curNode.Children.get(i));
            }
        }
        return rVec;
    }

    public ArrayList<TimeTreeNode> GetAllNodeVector() {
        ArrayList<TimeTreeNode> offs = new ArrayList<TimeTreeNode>();
        if (null != this.Root) {
            offs = this.GetOffspringsVector(this.Root.getTimeId());
            offs.add(0, this.Root);
        }
        return offs;
    }

    public ArrayList<TimeTreeNode> GetSelfAndOffspringsVector(String tid) {
        TimeTreeNode rNode = this.GetNodeById(tid);
        ArrayList<TimeTreeNode> offs = new ArrayList<TimeTreeNode>();
        if (rNode != null) {
            offs = this.GetOffspringsVector(tid);
            offs.add(0, rNode);
        }
        return offs;
    }

    public ArrayList<TimeTreeNode> GetOffspringsVector(String tid) {
        TimeTreeNode tRoot = this.GetNodeById(tid);
        ArrayList<TimeTreeNode> offsprings = new ArrayList<TimeTreeNode>();
        if (tRoot != null) {
            Stack<TimeTreeNode> searchStack = new Stack<TimeTreeNode>();
            searchStack.push(tRoot);
            while (!searchStack.empty()) {
                TimeTreeNode curNode = searchStack.pop();
                for (int i = 0; i < curNode.Children.size(); i++) {
                    TimeTreeNode sNode = curNode.Children.get(i);
                    offsprings.add(sNode);
                    searchStack.push(sNode);
                }
            }
        }
        return offsprings;
    }

    public ArrayList<TimeTreeNode> GetOffspringsVectorByTarget(String tid, String target) {
        TimeTreeNode tRoot = this.GetNodeById(tid);
        ArrayList<TimeTreeNode> offsprings = new ArrayList<TimeTreeNode>();
        if (tRoot != null) {
            Stack<TimeTreeNode> searchStack = new Stack<TimeTreeNode>();
            searchStack.push(tRoot);
            while (!searchStack.empty()) {
                TimeTreeNode curNode = searchStack.pop();
                for (int i = 0; i < curNode.Children.size(); i++) {
                    TimeTreeNode sNode = curNode.Children.get(i);
                    if (sNode.getFilename().equals(target)) {
                        offsprings.add(sNode);
                    }
                    searchStack.push(sNode);
                }
            }
        }
        return offsprings;
    }

    public ArrayList<TimeTreeNode> GetChildrenVectorByTarget(String tid, String target) {
        TimeTreeNode nNode = this.GetNodeById(tid);
        ArrayList<TimeTreeNode> ret = new ArrayList<TimeTreeNode>();
        if (nNode != null) {
            for (TimeTreeNode tn : nNode.Children) {
                if (tn.getFilename().equals(target)) {
                    ret.add(tn);
                }
            }
        }
        return ret;
    }

    public TimeTreeNode Root;


}
