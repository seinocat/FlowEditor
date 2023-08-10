using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("实体/实体行为"), GameEventNode, System.Serializable]
    public class ChangeActionNode : ClientNodeBase
    {
        public override string name => "实体行为";
        
        public override FlowNodeType Type => FlowNodeType.ChangeAction;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("行为ID")] 
        public int ActionID;
    }
}