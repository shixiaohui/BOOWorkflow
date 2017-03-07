package com.sysu.workflow;

import com.sysu.workflow.entity.BOMessage;
import com.sysu.workflow.model.ModelException;

import java.util.Queue;
import java.util.concurrent.LinkedBlockingDeque;

/**
 * 桥类：为应用程序和引擎建立通讯接口
 * Created by Rinkako on 2017/3/7 0007.
 */
public class EngineBridge {
    /**
     * 工厂方法，获取类的唯一实例
     * @return 桥接器的引用
     */
    public static EngineBridge GetInstance() {
        return EngineBridge.syncObj == null ?
                EngineBridge.syncObj = new EngineBridge() : EngineBridge.syncObj;
    }

    /**
     * <p>初始化桥接器</p>
     * <p>该方法在应用程序初始化时被调用</p>
     * @param executor 根状态机的处理器
     * @param handler 应用程序消息处理器
     */
    public void Init(SCXMLExecutor executor, EngineBridgeAppHandler handler) {
        this.SetExecutorReference(executor);
        this.SetAppHandler(handler);
    }

    /**
     * 为桥绑定状态机处理器
     * @param executor 要绑定的状态机处理器
     */
    public void SetExecutorReference(SCXMLExecutor executor) {
        this.rootExecutor = executor;
    }

    /**
     * 为桥绑定应用程序的消息处理器
     * @param handler 要绑定的消息处理器
     */
    public void SetAppHandler(EngineBridgeAppHandler handler) {
        this.appHandler = handler;
    }

    /**
     * 向引擎发送一个外部事件并触发它
     * @param eventName
     * @throws ModelException
     */
    public void SendEventAndTrigger(String eventName) throws ModelException {
        TriggerEvent tevt = new TriggerEvent(eventName, TriggerEvent.SIGNAL_EVENT, null);
        this.rootExecutor.triggerEvent(tevt);
    }

    /**
     * 获取一个待处理的消息，若没有则返回null
     * @return
     */
    public BOMessage GetPendingMessage() {
        return this.stateMachieMessageQueue.poll();
    }

    /**
     * 获取消息队列中剩余的消息数量
     * @return
     */
    public int Count() {
        return this.stateMachieMessageQueue.size();
    }

    /**
     * 将一个要发送给应用程序的消息放入队列
     * @param msg 要发送的消息
     */
    public void EnqueueBOMessage(BOMessage msg) {
        if (msg != null) {
            this.stateMachieMessageQueue.add(msg);
            this.appHandler.WasNotified();
        }
        else {
            throw new IllegalArgumentException();
        }
    }

    /**
     * 将一个任务作为消息放入发送到应用程序的队列中
     * @param taskName
     * @param roleName
     */
    public static void QuickEnqueueBOMessage(String taskName, String roleName) {
        BOMessage boMsg = new BOMessage();
        boMsg.AddMessageItem(taskName, roleName);
        EngineBridge.GetInstance().EnqueueBOMessage(boMsg);
    }

    /**
     * 将一个任务作为消息放入发送到应用程序的队列中，并且执行角色为空
     * @param taskName
     */
    public static void QuickEnqueueBOMessage(String taskName) {
        EngineBridge.QuickEnqueueBOMessage(taskName, "");
    }

    /**
     * 状态机要传递给应用程序的消息队列
     */
    private Queue<BOMessage> stateMachieMessageQueue;

    /**
     * 私有的构造器
     */
    private EngineBridge() {
        this.stateMachieMessageQueue = new LinkedBlockingDeque<BOMessage>();
    }

    /**
     * 引擎处理器的引用，她是整个状态机树上的根
     */
    private SCXMLExecutor rootExecutor;

    /**
     * 应用程序消息处理器的引用
     */
    private EngineBridgeAppHandler appHandler;

    /**
     * 桥的唯一实例
     */
    private static EngineBridge syncObj = null;
}
