using System;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem("单位/创建单位(指定坐标&旋转)"), GameEventNode, Serializable]
    public class CreateUnitAtPointNode : ServerNodeBase
    {
        public override string name => "创建单位(指定坐标&旋转)";

        public override FlowNodeType Type => FlowNodeType.CreateUnitAtPoint;
        
        public override bool TansferParameter => true;

        [Input("In")]
        public EventNodePort Input;

        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("单位ID")]
        public int EntityID;
        
        [CustomSetting("使用输入坐标")] 
        public bool UseInputPos;
        
        [CustomSetting("坐标")] 
        public Vector3 Position;

        [CustomSetting("朝向")] 
        public float Rotate;
    }
}
