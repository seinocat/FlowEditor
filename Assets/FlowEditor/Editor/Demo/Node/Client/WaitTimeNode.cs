using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("逻辑/等待"), GameEventNode, System.Serializable]
    public class WaitTimeNode : ClientNodeBase
    {
        public override string name => "等待";
        
        public override FlowNodeType Type => FlowNodeType.WaitTime;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("时间(秒)")]
        public float Time;

    }
}