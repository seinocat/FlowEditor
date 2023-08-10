using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem("实体/设置坐标旋转"), GameEventNode, System.Serializable]
    public class SetPositionNode : ServerNodeBase
    {
        public override string name => "坐标&旋转";
        
        public override FlowNodeType Type => FlowNodeType.SetPosition;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("目标为玩家(注意)")] 
        public bool TargetPlayer = true;
        
        [CustomSetting("地图ID(默认-1为本地图)")] 
        public int MapID = -1;

        [CustomSetting("坐标")] 
        public Vector3 Position;

        [CustomSetting("朝向")] 
        public float RotateY;
    }
}