using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("实体/旋转"), GameEventNode, System.Serializable]
    public class SetNpcRotationNode : ClientNodeBase
    {
        public override string name => "旋转";
        
        public override FlowNodeType Type => FlowNodeType.SetNpcRotation;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("朝向")] 
        public float RatateY;
    }
}