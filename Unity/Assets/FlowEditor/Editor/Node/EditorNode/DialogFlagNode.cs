using System;
using System.Collections.Generic;
using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{

    [NodeMenuItem("配置/对话配置"), FlowNode, Serializable]
    public class DialogFlagNode : EditorNodeBase
    {
        public override string name => "对话配置";
        
        [Input("End")]
        public EditorPort Input;
        
        [CustomSetting("对话列表")] 
        public List<int> Dialogs;
    }
}