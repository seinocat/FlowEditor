using System;
using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{

    [NodeMenuItem("道具/掉落(客户端)"), GameEventNode, Serializable]
    public class DropSelfClientNode : ClientNodeBase
    {
        public override string name => "掉落(客户端)";

        public override FlowNodeType Type => FlowNodeType.DropSelfClient;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("掉落类型")]
        public DropType DropType;

        [CustomSetting("延迟时间")] 
        public float DelayTime;
    }
}