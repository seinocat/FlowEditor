using System;
using Sirenix.OdinInspector;

namespace FlowEditor.Runtime
{
    // 注意：自动生成代码，请勿手动修改
    [Serializable]
    public enum FlowNodeType
    {
        #region 客户端节点

        [LabelText("开始")]
        Start = 1000,

        [LabelText("交互选项")]
        InteractItem = 1001,
        
        [LabelText("等待时间")]
        WaitTime = 1002,

        [LabelText("打开UI")]
        OpenUI = 1003,

        [LabelText("消息提示")]
        MessageTip = 1004,

        [LabelText("NPC播放动作")]
        PlayNpcAction = 1005,

        [LabelText("执行事件")]
        ExecuteEvent = 1006,

        [LabelText("结束")]
        End = 999999,
        
        #endregion

        #region 服务器节点
        
        [LabelText("发放奖励(指定道具ID和数量)")]
        GiveItems = 1000001,

        [LabelText("设置全局变量")]
        SetGlobalVariable = 1000002,

        [LabelText("获取全局变量")]
        GetGlobalVariable = 1000003,

        [LabelText("运算全局变量")]
        ComputeGlobalVariable = 1000004,

        [LabelText("提交道具(玩家选择)")]
        CommitItem = 1000005,

        [LabelText("选择分支")]
        SelectBranch = 1000006,

        [LabelText("移除道具")]
        RemoveItem = 1000007,

        [LabelText("条件节点")]
        Condition = 1000008,

        [LabelText("事件完成标志")]
        CompleteFlag = 9999999,
        #endregion

    }
}
