using System;
using UnityEngine;

namespace SeinoCat.FlowEditor.Runtime
{
    [Serializable]
    public enum AgentState
    {
        [InspectorName("任意")]
        Any = 0,
        
        [InspectorName("空闲中")]
        Idle = 1,
        
        [InspectorName("交互中")]
        Interaction = 2,
        
        [InspectorName("战斗中")]
        InBattle = 4,
        
        [InspectorName("死亡")]
        Dead = 5,
        
        [InspectorName("销毁")]
        Dispose = 6,
        
        [InspectorName("敌对")]
        Enemy = 7,
        
        [InspectorName("不可用")]
        Unavailable = 8,
        
        [InspectorName("不可见")]
        InVisiable = 9,
        
    }
}