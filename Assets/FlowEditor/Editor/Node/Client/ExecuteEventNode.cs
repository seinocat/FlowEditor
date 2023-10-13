using System;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem("逻辑/执行事件"), GameEventNode, Serializable]
    public class ExecuteEventNode : ClientNodeBase
    {
        public override string name => "执行事件";

        public override FlowNodeType Type => FlowNodeType.ExecuteEvent;
        
        public override Color color => new Color(1f, 0f, 0.02f);
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("事件ID")] 
        public int EventID;
    }
}