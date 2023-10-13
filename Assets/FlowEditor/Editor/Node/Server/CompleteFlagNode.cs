using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem("逻辑/完成标志"), GameEventNode, System.Serializable]
    public class CompleteFlagNode : ServerNodeBase
    {
        public override string name => "事件完成";
        
        public override FlowNodeType Type => FlowNodeType.CompleteFlag;
        
        public override Color color => new Color(1f, 0f, 0.02f);
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
    }
}