using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("道具/提交道具"), GameEventNode, System.Serializable]
    public class CommitItemNode : ServerNodeBase
    {
        public override string name => "提交道具";
        
        public override FlowNodeType Type => FlowNodeType.CommitItem;

        public override bool TansferParameter => true;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
    }
}