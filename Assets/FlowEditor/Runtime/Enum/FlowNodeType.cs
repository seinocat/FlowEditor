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

        [LabelText("采集")]
        Collect = 1002,

        [LabelText("开宝箱")]
        OpenTreasureBox = 1003,
        
        [LabelText("等待时间")]
        WaitTime = 1007,

        [LabelText("播放CG")]
        PlayCG = 1008,

        [LabelText("创建资源(特效，Timeline)")]
        CreateAssets = 1009,

        [LabelText("删除资源(特效，Timeline)")]
        DestroyAssets = 1010,

        [LabelText("对话")]
        ShowDialog = 1011,

        [LabelText("播放音乐&音效")]
        Audio = 1012,
        
        [LabelText("锁定屏幕相机")]
        LockCamera = 1016,

        [LabelText("解锁屏幕相机")]
        UnlockCamera = 1017,

        [LabelText("屏幕抖动")]
        ShakeScreen = 1018,

        [LabelText("停止屏幕抖动")]
        StopShakeScreen = 1019,

        [LabelText("打开UI")]
        OpenUI = 1020,

        [LabelText("消息提示")]
        MessageTip = 1021,

        [LabelText("NPC播放动作")]
        PlayNpcAction = 1022,

        [LabelText("改变NPC朝向")]
        SetNpcRotation = 1023,

        [LabelText("改变NPC行为")]
        ChangeAction = 1024,

        [LabelText("请求掉落(客户端)")]
        DropSelfClient = 1025,

        [LabelText("执行事件")]
        ExecuteEvent = 1026,

        [LabelText("完成回执")]
        CompleteReceipt = 1027,

        [LabelText("交互开关")]
        InteractSwitch = 1028,

        [LabelText("流程动画")]
        FlowAnima = 1029,

        [LabelText("接取任务")]
        ReceiveTask = 1030,

        [LabelText("交付任务")]
        DeliveryTask = 1031,

        [LabelText("客户端条件")]
        ClientCondition = 1032,

        [LabelText("结束")]
        End = 999999,


        #endregion

        #region 服务器节点

        [LabelText("发放奖励(奖励表ID)")]
        GiveAward = 1000001,

        [LabelText("发放奖励(指定道具ID和数量)")]
        GiveItems = 1000002,

        [LabelText("设置单位坐标")]
        SetPosition = 1000003,

        [LabelText("设置自动航行路径")]
        AutoFollow = 1000004,

        [LabelText("设置到达目标点(手动)")]
        ManualFollow = 1000005,

        [LabelText("生成指定单位")]
        CreateUnit = 1000006,

        [LabelText("删除自身")]
        DestroyUnit = 1000007,

        [LabelText("设置全局变量")]
        SetGlobalVariable = 1000010,

        [LabelText("获取全局变量")]
        GetGlobalVariable = 1000011,

        [LabelText("运算全局变量")]
        ComputeGlobalVariable = 1000012,

        [LabelText("使用指定道具")]
        UseItem = 1000015,

        [LabelText("提交道具(玩家选择)")]
        CommitItem = 1000016,

        [LabelText("选择分支")]
        SelectBranch = 1000017,

        [LabelText("请求掉落(服务器)")]
        RequestSelfDrop = 1000018,

        [LabelText("移除道具")]
        RemoveItem = 1000019,

        [LabelText("条件节点")]
        Condition = 1000020,

        [LabelText("随机分支")]
        RandomBranch = 1000021,

        [LabelText("创建单位(指定坐标&旋转)")]
        CreateUnitAtPoint = 1000022,

        [LabelText("事件完成标志")]
        CompleteFlag = 9999999,
        #endregion

    }
}
