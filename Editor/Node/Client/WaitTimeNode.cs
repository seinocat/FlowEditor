using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.WaitTime), GameEventNode, System.Serializable]
    public class WaitTimeNode : ClientNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());
        
        public override FlowNodeType Type => FlowNodeType.WaitTime;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("时间(秒)")]
        public float Time;

    }
}