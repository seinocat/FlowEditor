﻿using GraphProcessor;
using SeinoCat.FlowEditor.Runtime;

namespace SeinoCat.FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.CommitItem), FlowNode, System.Serializable]
    public class CommitItemNode : ServerNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());
        
        public override FlowNodeType Type => FlowNodeType.CommitItem;

        public override bool TansferParameter => true;
        
        [Input("In")]
        public FlowNodePort Input;
        
        [Output("Out", false)]
        public FlowNodePort Output;
    }
}