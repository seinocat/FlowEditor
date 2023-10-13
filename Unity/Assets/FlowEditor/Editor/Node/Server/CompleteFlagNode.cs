using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.CompleteFlag), FlowNode, System.Serializable]
    public class CompleteFlagNode : ServerNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());
        
        public override FlowNodeType Type => FlowNodeType.CompleteFlag;
        
        public override Color color => new Color(1f, 0f, 0.02f);
        
        [Input("In")]
        public FlowNodePort Input;
        
        [Output("Out", false)]
        public FlowNodePort Output;
    }
}