using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("道具/发放奖励(指定配置ID)"), GameEventNode, System.Serializable]
    public class GiveAwardNode : ServerNodeBase
    {
        public override string name => "发放奖励";
        
        public override FlowNodeType Type => FlowNodeType.GiveAward;

        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;
        
        [CustomSetting("奖励表ID")] 
        public int AwardID;
    }
}