using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("场景&表现/解锁相机"), GameEventNode, System.Serializable]
    public class UnlockCameraNode : ClientNodeBase
    {
        public override string name => "解锁相机";
        
        public override FlowNodeType Type => FlowNodeType.UnlockCamera;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
    }
}