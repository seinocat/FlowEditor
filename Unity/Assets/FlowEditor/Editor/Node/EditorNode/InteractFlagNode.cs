using System;
using GraphProcessor;
using SeinoCat.FlowEditor.Runtime;

namespace SeinoCat.FlowEditor.Editor
{

    [NodeMenuItem("配置/交互配置"), FlowNode, Serializable]
    public class InteractFlagNode : EditorNodeBase
    {
        public override string name => "交互配置";
        
        [Input("In")]
        public EditorPort Input;
        
        [Output("Start")]
        public FlowNodePort Output;

        [CustomSetting("备注")]
        public string desc;
    }
}