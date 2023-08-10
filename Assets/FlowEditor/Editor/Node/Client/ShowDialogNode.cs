using System.Collections.Generic;
using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{
    [NodeMenuItem("交互/对话"), GameEventNode, System.Serializable]
    public class ShowDialogNode : ClientNodeBase
    {
        public override string name => "对话";
        
        public override FlowNodeType Type => FlowNodeType.ShowDialog;
        
        [Input("In")]
        public EventNodePort Input;
        
        [Output("Out", false)]
        public EventNodePort Output;

        [CustomSetting("对话组ID"), ListReference(typeof(int), nameof(GroupIds))] 
        public List<int> GroupIds;
    }
}