using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace SeinoCat.FlowEditor.Runtime
{
    [Serializable]
    public class SelectData
    {
        [LabelText("条件组"), LabelWidth(120)] 
        public List<ConditionData> Conditions;
    
        [LabelText("下一节点ID"), LabelWidth(120)] 
        public int NextNodeID;
    }
    
    [Serializable]
    public class ConditionData
    {
        [LabelText("条件ID"), LabelWidth(120)]
        public int ConditionID;
    
        [LabelText("条件说明"), LabelWidth(120)] 
        public string Desc;
        
    }
}