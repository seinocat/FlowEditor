using System;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{

    [NodeMenuItem((int)FlowNodeType.End), GameEventNode, Serializable]
    public class EndNode : ClientNodeBase
    {
        public override Color color => new Color(1f, 0.42f, 0f);

        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());

        public override FlowNodeType Type => FlowNodeType.End;
        
        [Input("End", true)]
        public FlowNodePort Input;
    }
}