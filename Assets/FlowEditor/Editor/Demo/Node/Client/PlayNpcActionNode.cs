using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("实体/动作"), GameEventNode, System.Serializable]
    public class PlayNpcActionNode : ClientNodeBase
    {
        public override string name => "动作";
        
        public override FlowNodeType Type => FlowNodeType.PlayNpcAction;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("动作名称")] 
        public string ActionName;
    }
}