using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FlowEditor.Runtime
{
    [Serializable]
    public enum FlowType
    {
        None,
        [InspectorName("交互流程")]
        Interact,
        [InspectorName("事件流程")]
        Event
    }
    
    [Serializable]
    public enum ComputeType
    {
        [InspectorName("加")]
        Add = 0,
        [InspectorName("减")]
        Sub = 1
    }

    [Serializable]
    public enum DropType
    {
        [InspectorName("宝箱")]
        TreasureBox
    }

    [Serializable]
    public enum OpenUIType
    {
        [InspectorName("提交道具")]
        CommitItem,
    }

    [Serializable]
    public enum DisapperType
    {
        [InspectorName("死亡表现")]
        Dead,
        [InspectorName("消失表现")]
        Disapper
    }


}