using System;
using System.Collections.Generic;
using GraphProcessor;
using SeinoCat.FlowEditor.Runtime;
using UnityEngine;

namespace SeinoCat.FlowEditor.Editor
{

    [NodeMenuItem("配置/事件配置"), FlowNode, Serializable]
    public class EventFlagNode : EditorNodeBase
    {
        public override string name => "事件配置";
        
        [Input("In")]
        public EditorPort Input;
        
        [Output("Out", false)]
        public FlowStartPort Output;

        [CustomSetting("事件ID(必填)")] 
        public int EventID;
        
        [CustomSetting("事件名称")] 
        public string EventName;
        
        [CustomSetting("触发类型")] 
        public TriggerType TriggerType = TriggerType.Manual;

        [CustomSetting("可触发状态")] 
        public AgentState State = AgentState.Idle;
        
        [CustomSetting("优先级")] 
        public int Priority;
        
        [CustomSetting("显示图标")] 
        public Sprite Sprite;
        
        [CustomSetting("显示文本")] 
        public string Text;
        
        [CustomSetting("备注")] 
        public string Desc;
        
        [CustomSetting("条件组")] 
        public List<int> Conditions;
    }
}