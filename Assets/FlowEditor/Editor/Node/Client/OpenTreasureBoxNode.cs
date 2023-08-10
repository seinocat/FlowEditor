using System;
using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("交互/开启宝箱"), GameEventNode, Serializable]
    public class OpenTreasureBoxNode : ClientNodeBase
    {
        public override string name => "开启宝箱";

        public override FlowNodeType Type => FlowNodeType.OpenTreasureBox;

        [Input("In")]
        public EventNodePort Input;

        [Output("Out", false)]
        public EventNodePort Output;
    }
}
