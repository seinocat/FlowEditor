using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("道具/请求掉落道具"), GameEventNode, System.Serializable]
    public class RequestSelfDropNode : ServerNodeBase
    {
        public override string name => "掉落";
        
        public override FlowNodeType Type => FlowNodeType.RequestSelfDrop;
        
        public override bool TansferParameter => true;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("掉落类型")]
        public DropType DropType;
    }
}