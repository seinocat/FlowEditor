using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("单位/删除单位"), GameEventNode, System.Serializable]
    public class DestroyUnitNode : ServerNodeBase
    {
        public override string name => "删除单位";
        
        public override FlowNodeType Type => FlowNodeType.DestroyUnit;
        
        public override bool TansferParameter => true;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("消失类型")]
        public DisapperType DisapperType;
    }
}