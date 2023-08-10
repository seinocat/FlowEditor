using System.Collections.Generic;
using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("道具/使用道具"), GameEventNode, System.Serializable]
    public class UseItemNode : ServerNodeBase
    {
        public override string name => "使用道具";
        
        public override FlowNodeType Type => FlowNodeType.UseItem;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("道具列表"), ListReference(typeof(ItemData), nameof(ItemList))] 
        public List<ItemData> ItemList;
    }
}