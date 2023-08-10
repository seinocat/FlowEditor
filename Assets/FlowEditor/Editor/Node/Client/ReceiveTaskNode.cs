using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("任务/接取任务"), GameEventNode, System.Serializable]
    public class ReceiveTaskNode : ClientNodeBase
    {
        public override string name => "任务接取";
        
        public override FlowNodeType Type => FlowNodeType.ReceiveTask;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("任务ID")] 
        public int TaskID;
    }
}