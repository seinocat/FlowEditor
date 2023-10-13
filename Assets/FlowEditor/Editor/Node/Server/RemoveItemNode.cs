using System.Collections.Generic;
using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.RemoveItem), GameEventNode, System.Serializable]
    public class RemoveItemNode : ServerNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());
        
        public override FlowNodeType Type => FlowNodeType.RemoveItem;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("道具列表"), ListReference(typeof(ItemData), nameof(ItemList))] 
        public List<ItemData> ItemList;
    }
}