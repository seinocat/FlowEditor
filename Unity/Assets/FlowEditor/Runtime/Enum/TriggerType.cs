using System;
using UnityEngine;

namespace SeinoCat.FlowEditor.Runtime
{
    [Serializable]
    public enum TriggerType
    {
        [InspectorName("自动触发")]
        Auto = 0,
        
        [InspectorName("手动触发")]
        Manual = 1,
        
        [InspectorName("进入触发")]
        Enter = 2,
        
        [InspectorName("停留触发")]
        Stay = 3,
        
        [InspectorName("离开触发")]
        Exit = 4,
        
        [InspectorName("死亡触发")]
        Dead = 5,
        
        [InspectorName("销毁触发")]
        Dispose = 6,
        
        [InspectorName("激活触发")]
        Activation = 7,

        [InspectorName("创建触发")]
        Create = 8,
    }
}