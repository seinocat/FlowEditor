using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("场景&表现/相机抖动-停止"), GameEventNode, System.Serializable]
    public class StopShakeScreenNode : ClientNodeBase
    {
        public override string name => "相机抖动-停止";
        
        public override FlowNodeType Type => FlowNodeType.StopShakeScreen;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
    }
}