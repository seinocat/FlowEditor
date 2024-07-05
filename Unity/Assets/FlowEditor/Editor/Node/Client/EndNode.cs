using System;
using GraphProcessor;
using SeinoCat.FlowEditor.Runtime;
using UnityEngine;

namespace SeinoCat.FlowEditor.Editor
{

    [NodeMenuItem((int)FlowNodeType.End), FlowNode, Serializable]
    public class EndNode : ClientNodeBase
    {
        public override Color color => new Color(1f, 0.42f, 0f);

        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());

        public override FlowNodeType Type => FlowNodeType.End;
        
        [Input("End", true)]
        public FlowNodePort Input;
    }
}