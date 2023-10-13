using System;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{

    [NodeMenuItem("逻辑/结束"), GameEventNode, Serializable]
    public class EndNode : ClientNodeBase
    {
        public override Color color => new Color(1f, 0.42f, 0f);

        public override string name => "结束";

        public override FlowNodeType Type => FlowNodeType.End;
        
        [Input("End", true)]
        public EventNodePort Input;
    }
}