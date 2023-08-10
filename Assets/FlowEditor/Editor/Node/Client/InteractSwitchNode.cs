using System;
using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{

    [NodeMenuItem("实体/交互开关"), GameEventNode, Serializable]
    public class InteractSwitchNode : ClientNodeBase
    {
        public override string name => "交互开关";

        public override FlowNodeType Type => FlowNodeType.InteractSwitch;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("开启交互")] 
        public bool Open = true;
    }
}