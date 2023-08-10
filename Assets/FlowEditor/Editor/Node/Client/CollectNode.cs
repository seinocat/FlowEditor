using System;
using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("交互/采集"), GameEventNode, Serializable]
    public class CollectNode : ClientNodeBase
    {
        public override string name => "采集";

        public override FlowNodeType Type => FlowNodeType.Collect;

        [Input("In")]
        public EventNodePort Input;

        [Output("Out", false)]
        public EventNodePort Output;
    }
}
