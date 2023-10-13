using FlowEditor.Editor;
using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.GiveItems), FlowNode, System.Serializable]
    public class GiveItemsNode : ServerNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());
        
        public override FlowNodeType Type => FlowNodeType.GiveItems;
        
        [Input("In")]
        public FlowNodePort Input;
        
        [Output("Out", false)]
        public FlowNodePort Output;

        [CustomSetting("道具ID")] 
        public int ItemID;
        
        [CustomSetting("道具数量")] 
        public int ItemCount;
    }
}