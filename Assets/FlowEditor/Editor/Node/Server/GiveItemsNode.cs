using FlowEditor.Editor;
using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("道具/发放奖励(指定ID和数量)"), GameEventNode, System.Serializable]
    public class GiveItemsNode : ServerNodeBase
    {
        public override string name => "发放奖励";
        
        public override FlowNodeType Type => FlowNodeType.GiveItems;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("道具ID")] 
        public int ItemID;
        
        [CustomSetting("道具数量")] 
        public int ItemCount;
    }
}