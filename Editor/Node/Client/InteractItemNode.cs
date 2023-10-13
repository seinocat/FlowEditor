using System;
using System.Collections.Generic;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{

    [NodeMenuItem((int)FlowNodeType.InteractItem), GameEventNode, Serializable]
    public class InteractItemNode : InteractNodeBase
    {
        public override Color color => new Color(0.05f, 0.7f, 0.15f);

        public override string name =>NodeGroupHelper.GetName(Type.GetHashCode());

        public override FlowNodeType Type => FlowNodeType.InteractItem;
        
        [Input("In")]
        public FlowNodePort Input;
        
        [Output("Out")]
        public FlowNodePort Output;

        [CustomSetting("可交互状态")] 
        public AgentState State = AgentState.Idle;
        
        [CustomSetting("显示图标"), AssetReference(typeof(string), "Icon")] 
        public Sprite Sprite;
        
        [CustomSetting("显示文本")] 
        public string Text;
        
        [CustomSetting("优先级")] 
        public int Priority;
        
        [CustomSetting("备注")] 
        public string Desc;
        
        [CustomSetting("条件组"), ListReference(typeof(int), nameof(Conditions))] 
        public List<int> Conditions;
    }
}