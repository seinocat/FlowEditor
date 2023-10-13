using System;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.ExecuteEvent), GameEventNode, Serializable]
    public class ExecuteEventNode : ClientNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());

        public override FlowNodeType Type => FlowNodeType.ExecuteEvent;
        
        public override Color color => new Color(1f, 0f, 0.02f);
        
        [Input("In")]
        public FlowNodePort Input;
        
        [Output("Out", false)]
        public FlowNodePort Output;

        [CustomSetting("事件ID")] 
        public int EventID;
    }
}