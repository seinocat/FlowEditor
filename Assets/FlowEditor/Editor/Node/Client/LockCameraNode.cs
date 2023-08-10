using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("场景&表现/锁定相机"), GameEventNode, System.Serializable]
    public class LockCameraNode : ClientNodeBase
    {
        public override string name => "锁定相机";
        
        public override FlowNodeType Type => FlowNodeType.LockCamera;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
    }
}