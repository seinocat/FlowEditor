using System.Collections.Generic;
using GraphProcessor;
using SeinoCat.FlowEditor.Runtime;

namespace SeinoCat.FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.RemoveItem), FlowNode, System.Serializable]
    public class RemoveItemNode : ServerNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());
        
        public override FlowNodeType Type => FlowNodeType.RemoveItem;
        
        [Input("In")]
        public FlowNodePort Input;
        
        [Output("Out", false)]
        public FlowNodePort Output;

        [CustomSetting("道具列表"), ListReference(typeof(ItemData), nameof(ItemList))] 
        public List<ItemData> ItemList;
    }
}