using System.Collections.Generic;
using FlowEditor.Runtime;
using GraphProcessor;
using Unity.Mathematics;

namespace FlowEditor.Editor
{
    [NodeMenuItem("AI/路径寻路"), GameEventNode, System.Serializable]
    public class AutoFollowNode : ServerNodeBase
    {
        public override string name => "路径寻路";
        
        public override FlowNodeType Type => FlowNodeType.AutoFollow;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("路径点")] 
        public  float MoveSpeed = 1;

        [CustomSetting("路径点"), ListReference(typeof(float3), nameof(PathList))] 
        public  List<float3> PathList;
    }
}