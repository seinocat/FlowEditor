using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("变量/获取"), GameEventNode, System.Serializable]
    public class GetGlobalVariableNode : ClientNodeBase
    {
        public override string name => "获取全局变量";

        public override FlowNodeType Type => FlowNodeType.GetGlobalVariable;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("全局变量名")] 
        public string GlobalKey;
    }
}