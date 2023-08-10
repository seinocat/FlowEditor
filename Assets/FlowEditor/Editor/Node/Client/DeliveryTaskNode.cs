using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("任务/交付任务"), GameEventNode, System.Serializable]
    public class DeliveryTaskNode :  ClientNodeBase
    {
        public override string name => "任务交付";
        
        public override FlowNodeType Type => FlowNodeType.DeliveryTask;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("任务ID")] 
        public int TaskID;
    }
}