using System.Collections.Generic;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.Condition), GameEventNode, System.Serializable]
    public class ConditionNode : ServerNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());
        
        public override FlowNodeType Type => FlowNodeType.Condition;
        
        [Input("In")]
        public FlowNodePort Input;
        
        [Output("True")]
        public FlowNodePort Output;
        
        [Output("False", false)] 
        public FlowNodePort Output2;
        
        [CustomSetting("条件组"), ListReference(typeof(int), nameof(Conditions))] 
        public List<int> Conditions;

        [CustomSetting("成功节点ID"), HideInInspector]
        public int SuccessID;
        
        [CustomSetting("失败节点ID"), HideInInspector]
        public int FailID;


        public ConditionNode()
        {
            onAfterEdgeConnected += OnEdgeChange;
            onAfterEdgeDisconnected += OnEdgeChange;
        }
        
        private void OnEdgeChange(SerializableEdge edge)
        {
            this.graph.UpdateComputeOrder();
            foreach (var port in this.outputPorts)
            {
                switch (port.fieldName)
                {
                    case "Output":
                        this.SetNextNode(true, port);
                        break;
                    case "Output2":
                        this.SetNextNode(false, port);
                        break;
                }
            }
        }

        private void SetNextNode(bool isSuccess, NodePort port)
        {
            if (port.GetEdges().Count > 0)
            {
                if (isSuccess)
                {
                    this.SuccessID = port.GetEdges()[0].inputNode.computeOrder;
                }
                else
                {
                    this.FailID = port.GetEdges()[0].inputNode.computeOrder;
                }
                 
            }
            else
            {
                if (isSuccess)
                {
                    this.SuccessID = -1;
                }
                else
                {
                    this.FailID = -1;
                }
            }
        }
    }
}