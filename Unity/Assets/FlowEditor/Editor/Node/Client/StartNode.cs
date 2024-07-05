using System;
using GraphProcessor;
using SeinoCat.FlowEditor.Runtime;
using UnityEngine;

namespace SeinoCat.FlowEditor.Editor
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