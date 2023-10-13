using System;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.Start), FlowNode, Serializable]
    public class StartNode : ClientNodeBase
    {
        public override Color color => new Color(1f, 0.42f, 0f);
        
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());

        public override FlowNodeType Type => FlowNodeType.Start;
        
        [Input("In")]
        public FlowStartPort Input;
        
        [Output("Start", false)]
        public FlowNodePort Output;
    }
}