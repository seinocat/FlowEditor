using System;
using System.Collections.Generic;
using FlowEditor.Runtime;
using GraphProcessor;

namespace FlowEditor.Editor
{

    [NodeMenuItem("配置/交互条件配置"), GameEventNode, Serializable]
    public class InteractConditionNode : EditorNodeBase
    {
        public override string name => "交互条件配置";
        
        [Input("End")] 
        public EditorPort Input;

        [CustomSetting("条件组"), ListReference(typeof(int), nameof(Conditions))] 
        public List<int> Conditions;
    }
}