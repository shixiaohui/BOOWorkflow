package com.sysu.workflow.entity;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * 引擎消息类：作为引擎向应用程序发送消息的包装中间件
 * Created by Rinkako on 2017/3/7 0007.
 */
public class BOMessage {
    /**
     * 构造器
     */
    public BOMessage() {
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        this.messageCreateTimestampString = sdf.format(new Date());
        this.agentList = new ArrayList<String>();
        this.tasksList = new ArrayList<String>();
        this.paramsList = new ArrayList<String>();
        this.callbackEventList = new ArrayList<String>();
    }

    /**
     * 将一个要发送给应用程序的任务项和她的处理者添加到消息中
     * @param taskName 任务名称
     * @param paramStr 任务参数字符串
     * @param dealRole 任务处理者
     * @param callbackEv 任务完成后要触发的事件名
     */
    public void AddMessageItem(String taskName, String paramStr, String dealRole, String callbackEv) {
        if (taskName != null && dealRole != null) {
            this.tasksList.add(taskName);
            this.agentList.add(dealRole);
            this.paramsList.add(paramStr);
            this.callbackEventList.add(callbackEv);
        }
        else {
            throw new IllegalArgumentException();
        }
    }

    /**
     * 获取应用程序要处理的任务向量
     * @return 任务向量的引用
     */
    public List<String> GetTaskList() {
        return this.tasksList;
    }

    /**
     * 获取应用程序要处理的任务向量所一一对应的角色向量
     * @return Agent向量的引用
     */
    public List<String> GetAgentList() {
        return this.agentList;
    }

    /**
     * 获取应用程序要处理的任务向量所一一对应的参数字符串向量
     * @return Params向量的引用
     */
    public List<String> GetParamsList() { return this.paramsList; }

    /**
     * 获取应用程序要处理的任务向量所一一对应的参数字符串向量
     * @return Params向量的引用
     */
    public List<String> GetCallbackList() { return this.callbackEventList; }


    /**
     * 这个消息要传递给应用程序的任务向量
     */
    private List<String> tasksList;

    /**
     * 这个消息要传递给应用程序的任务所对应的处理角色向量
     */
    private List<String> agentList;

    /**
     * 这个消息要传递给应用程序的任务所对应的参数字符串向量
     */
    private List<String> paramsList;

    /**
     * 这个消息要传递给应用程序的任务所对应的完成反馈事件向量
     */
    private List<String> callbackEventList;

    /**
     * 消息产生的时间戳
     */
    private String messageCreateTimestampString;
}
