package com.sysu.workflow;

import com.sun.org.apache.xpath.internal.operations.Bool;

/**
 * 为状态机引擎提供与消息处理路由的接口
 * Created by Rinkako on 2017/5/24.
 */
public interface IRouterAdapter {
    /* Methods of Connection */
    /**
     * 连接SCXML引擎到消息处理路由
     * @param username 登录路由服务的用户名
     * @param password 登录路由服务的密码串（路由可能要求发送已加密的串）
     * @param outResult [out] 登录结果
     * @return 操作是否成功
     */
    Boolean ConnectToRouter(String username, String password, StringBuilder outResult);

    /**
     * 获取当前是否成功连接到消息路由
     * @param handle 连接句柄
     * @param outResult [out] 是否连接到路由
     * @return 操作是否成功
     */
    Boolean CheckConnectToRouter(String handle, StringBuilder outResult);

    /**
     * 断开SCXML引擎到消息路由的连接
     * @param handle 连接句柄
     * @param outResult [out] 断开的结果
     * @return 操作是否成功
     */
    Boolean DisconnectFromRouter(String handle, StringBuilder outResult);

    /* Methods of User */
    /**
     * 通过用户唯一标识符获取用户的ParticipantId
     * @param handle 连接句柄
     * @param username 要检索Pid的用户名
     * @param outResult [out] 该用户的Pid
     * @return 操作是否成功
     */
    Boolean GetParticipantFromUserID(String handle, String username, StringBuilder outResult);

    /* Methods of Case */
    /**
     * 上传一个过程描述文件
     * @param handle 连接句柄
     * @param filePath 过程描述文件在本地的绝对路径（含文件名）
     * @param fileName 过程描述文件的文件名
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean UploadSpecificationFile(String handle, String filePath, String fileName, StringBuilder outResult);

    /**
     * 卸载一个过程
     * 关于第2到第4个参数的意义，请参考YSpecificationID的toMap方法
     * @param handle 连接句柄
     * @param specidentifier a system generated UUID string
     * @param specversion verion
     * @param specuri the user-defined name
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean UnloadSpecification(String handle, String specidentifier, String specversion, String specuri, StringBuilder outResult);

    /**
     * 获取当前加载的过程
     * @param handle 连接句柄
     * @param outResult [out] 当前加载的过程集合的串表达
     * @return 操作是否成功
     */
    Boolean GetLoadedSpecificationList(String handle, StringBuilder outResult);

    /**
     * 获取一个过程的数据
     * 关于第2到第4个参数的意义，请参考YSpecificationID的toMap方法
     * @param handle 连接句柄
     * @param specidentifier a system generated UUID string
     * @param specversion verion
     * @param specuri the user-defined name
     * @param outResult [out] 数据的XML格式串
     * @return 操作是否成功
     */
    Boolean GetSpecificationData(String handle, String specidentifier, String specversion, String specuri, StringBuilder outResult);

    /**
     * 获取一个过程的名称
     * 关于第2到第4个参数的意义，请参考YSpecificationID的toMap方法
     * @param handle 连接句柄
     * @param specidentifier a system generated UUID string
     * @param specversion verion
     * @param specuri the user-defined name
     * @param outResult [out] 过程名称
     * @return 操作是否成功
     */
    Boolean GetSpecificationName(String handle, String specidentifier, String specversion, String specuri, StringBuilder outResult);


    /**
     * 启动一个已经上传的过程描述文件
     * 关于第2到第4个参数的意义，请参考YSpecificationID的toMap方法
     * @param handle 连接句柄
     * @param specidentifier a system generated UUID string
     * @param specversion verion
     * @param specuri the user-defined name
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean launchCase(String handle, String specidentifier, String specversion, String specuri, StringBuilder outResult);

    /**
     * 获取指定流程的所有运行中的案例
     * 关于第2到第4个参数的意义，请参考YSpecificationID的toMap方法
     * @param handle 连接句柄
     * @param specidentifier a system generated UUID string
     * @param specversion verion
     * @param specuri the user-defined name
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean GetRunningCases(String handle, String specidentifier, String specversion, String specuri, StringBuilder outResult);

    /**
     * 取消一个Case
     * @param handle 连接句柄
     * @param caseId Case的Id
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean CancelCase(String handle,  String caseId, StringBuilder outResult);



    /* Methods of WorkQueue */
    /**
     * 通过ParticipantId获取该人物各类型的工作列表
     * UNDEFINED     -1
     * OFFERED       0
     * ALLOCATED     1
     * STARTED       2
     * SUSPENDED     3
     * UNOFFERED     4
     * WORKLISTED    5
     * @param handle 连接句柄
     * @param pid 用户的ParticipantId
     * @param queueType 要检索的队列的类型
     * @param outResult [out] 工作列表的XML表达
     * @return 操作是否成功
     */
    Boolean GetWorkQueueByPid(String handle, String pid, String queueType, StringBuilder outResult);

    /**
     * 用户接受一个工作项
     * @param handle 连接句柄
     * @param pid 用户的ParticipantId
     * @param itemId 工作项的Id
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean AcceptOfferItemByPid(String handle, String pid, String itemId, StringBuilder outResult);

    /**
     * 让一个用户开始一个工作项
     * @param handle 连接句柄
     * @param pid 用户ParticipantId
     * @param itemId 工作项的Id
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean StartItemByPid(String handle, String pid, String itemId, StringBuilder outResult);

    /**
     * 取消一个用户的一个工作项
     * @param handle 连接句柄
     * @param pid 用户ParticipantId
     * @param itemId 工作项的Id
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean DeallocateByPid(String handle, String pid, String itemId, StringBuilder outResult);

    /**
     * 重分派一个用户的一个工作项
     * @param handle 连接句柄
     * @param pid 用户ParticipantId
     * @param itemId 工作项的Id
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean ReallocateByPid(String handle, String pid, String itemId, StringBuilder outResult);

    /**
     * 保存一个工作项参数的编辑
     * @param handle 连接句柄
     * @param pid 用户ParticipantId
     * @param itemId 工作项的Id
     * @param dataString 更新时刻的字符串
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean UpdateByPid(String handle, String pid, String itemId, String dataString, StringBuilder outResult);

    /**
     * 更新一个用户的一个工作项
     * @param handle 连接句柄
     * @param pid 用户ParticipantId
     * @param itemId 工作项的Id
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean CompleteByPid(String handle, String pid, String itemId, StringBuilder outResult);

    /**
     * 跳过一个用户的一个工作项
     * @param handle 连接句柄
     * @param pid 用户ParticipantId
     * @param itemId 工作项的Id
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean SkipByPid(String handle, String pid, String itemId, StringBuilder outResult);

    /**
     * 将一个工作项从一个用户委派给另一个用户
     * @param handle 连接句柄
     * @param pidFrom 用户ParticipantId
     * @param pidTo 用户ParticipantId
     * @param itemId 工作项的Id
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean DelegateByPid(String handle, String pidFrom, String pidTo, String itemId, StringBuilder outResult);

    /**
     * 工作项批处理
     * @param handle 连接句柄
     * @param pid 用户ParticipantId
     * @param itemId 工作项的Id
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean PileByPid(String handle, String pid, String itemId, StringBuilder outResult);

    /**
     * 挂起一个用户的一个工作项
     * @param handle 连接句柄
     * @param pid 用户ParticipantId
     * @param itemId 工作项的Id
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean SuspendByPid(String handle, String pid, String itemId, StringBuilder outResult);

    /**
     * 解除一个用户的一个工作项的挂起状态
     * @param handle 连接句柄
     * @param pid 用户ParticipantId
     * @param itemId 工作项的Id
     * @param outResult [out] 执行结果
     * @return 操作是否成功
     */
    Boolean UnsuspendByPid(String handle, String pid, String itemId, StringBuilder outResult);

    /**
     * 由一个工作项的id获取一个工作项
     * @param handle 连接句柄
     * @param itemId 工作项的Id
     * @param outResult [out] 工作项的XML表达
     * @return 操作是否成功
     */
    Boolean GetWorkItemByIid(String handle, String itemId, StringBuilder outResult);

    /**
     * 由一个工作项的id获取一个工作项的参数列表
     * @param handle 连接句柄
     * @param itemId 工作项的Id
     * @param outResult [out] 工作项参数列表的XML表达
     * @return 操作是否成功
     */
    Boolean GetWorkItemParametersByIid(String handle, String itemId, StringBuilder outResult);

    /**
     * 由一个工作项的id获取一个工作项的数据模式
     * @param handle 连接句柄
     * @param itemId 工作项的Id
     * @param outResult [out] 工作项数据模式的字符串表达
     * @return 操作是否成功
     */
    Boolean GetWorkItemDataSchemaByIid(String handle, String itemId, StringBuilder outResult);


}
