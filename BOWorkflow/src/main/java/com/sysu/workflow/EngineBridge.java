package com.sysu.workflow;

import com.sysu.workflow.entity.BOMessage;
import com.sysu.workflow.model.ModelException;
import javassist.NotFoundException;

import java.util.HashMap;
import java.util.Map;
import java.util.Queue;
import java.util.concurrent.Exchanger;
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
     * @param handler 应用程序消息处理器
     */
    public void Init(EngineBridgeAppHandler handler) {
        this.SetAppHandler(handler);
    }

    /**
     * 为桥绑定状态机处理器
     * @param executor 要绑定的状态机处理器
     */
    public void SetExecutorReference(int executorId, SCXMLExecutor executor) {
        Integer intPackage = Integer.valueOf(executorId);
        this.executorMap.put(intPackage, executor);
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
     * @param eventName 要触发的外部事件名
     * @param payload 附加在事件上的包装
     * @throws ModelException
     */
    public void SendEventAndTrigger(int executorId, String eventName, Object payload) throws ModelException {
        TriggerEvent tevt = new TriggerEvent(eventName, TriggerEvent.SIGNAL_EVENT, payload);
        Integer intPackage = Integer.valueOf(executorId);
        try {
            this.executorMap.get(intPackage).triggerEvent(tevt);
        }
        catch (Exception ex) {
            throw new IllegalArgumentException();
        }
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
            if (this.appHandler != null) {
                this.appHandler.WasNotified();
            }
        }
        else {
            throw new IllegalArgumentException();
        }
    }

    /**
     * 将一个任务作为消息放入发送到应用程序的队列中
     * @param taskName 任务名称
     * @param paramStr 参数字符串
     * @param roleName 角色名称
     * @param callbackEv 处理完成的事件名
     */
    public static void QuickEnqueueBOMessage(String taskName, String paramStr, String roleName, String callbackEv) {
        BOMessage boMsg = new BOMessage();
        boMsg.AddMessageItem(taskName, paramStr, roleName, callbackEv);
        EngineBridge.GetInstance().EnqueueBOMessage(boMsg);
    }

    /**
     * 将一个任务作为消息放入发送到应用程序的队列中，并且执行角色为空
     * @param taskName 任务名称
     * @param callbackEv 处理完成的事件名
     */
    public static void QuickEnqueueBOMessage(String taskName, String callbackEv) {
        EngineBridge.QuickEnqueueBOMessage(taskName, "", "", callbackEv);
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
        this.executorMap = new HashMap<Integer, SCXMLExecutor>();
    }

    /**
     * 引擎处理器的引用，她是整个状态机树上的根
     */
    private Map<Integer, SCXMLExecutor> executorMap;

    /**
     * 应用程序消息处理器的引用
     */
    private EngineBridgeAppHandler appHandler;

    /**
     * 桥的唯一实例
     */
    private static EngineBridge syncObj = null;
}
