using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem("AI/前往指定区域"), GameEventNode, System.Serializable]
    public class ManualFollowNode : ServerNodeBase
    {
        public override string name => "前往指定区域";
        
        public override FlowNodeType Type => FlowNodeType.ManualFollow;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("目标坐标")] 
        public Vector3 Position;
        
        [CustomSetting("目标范围")] 
        public float Range;
        
        [CustomSetting("移动速度")] 
        public float MoveSpeed;
        
        [CustomSetting("抵达停止")] 
        public bool ReachStop = false;
    }
}