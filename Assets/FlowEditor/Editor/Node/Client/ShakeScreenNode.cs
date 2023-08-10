using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("场景&表现/相机抖动-开始"), GameEventNode, System.Serializable]
    public class ShakeScreenNode : ClientNodeBase
    {
        public override string name => "相机抖动-开始";
        
        public override FlowNodeType Type => FlowNodeType.ShakeScreen;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("震动配置ID")]
        public int ShakeId;
        
        [CustomSetting("延迟时间")]
        public float DelayTime;
    }
}