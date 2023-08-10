using System;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem("逻辑/开始"), GameEventNode, Serializable]
    public class StartNode : ClientNodeBase
    {
        public override Color color => new Color(1f, 0.42f, 0f);
        
        public override string name => "开始";

        public override FlowNodeType Type => FlowNodeType.Start;
        
        [Input("In")]
        public EventStartPort Input;
        
        [Output("Start", false)]
        public EventNodePort Output;
    }
}