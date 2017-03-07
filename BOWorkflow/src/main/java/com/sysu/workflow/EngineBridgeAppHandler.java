package com.sysu.workflow;

/**
 * 为应用程序获取状态机的状态变化提供接口
 * Created by Rinkako on 2017/3/7 0007.
 */
public interface EngineBridgeAppHandler {
    /**
     * 通知应用程序处理消息队列
     */
    void WasNotified();
}
