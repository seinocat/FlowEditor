using System;
using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("实体/动画"), GameEventNode, Serializable]
    public class FlowAnimaNode : ClientNodeBase
    {
        public override string name => "动画";

        public override FlowNodeType Type => FlowNodeType.FlowAnima;

        [Input("In")]
        public EventNodePort Input;

        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("配置名称")] 
        public string FlowName;
    }
}
