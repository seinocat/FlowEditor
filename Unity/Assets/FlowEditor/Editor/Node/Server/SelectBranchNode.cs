using System;
using System.Collections.Generic;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;


namespace FlowEditor.Editor
{
    [NodeMenuItem((int)FlowNodeType.SelectBranch), FlowNode, System.Serializable]
    public class SelectBranchNode : ServerNodeBase
    {
        public override string name => NodeGroupHelper.GetName(Type.GetHashCode());
        
        public override FlowNodeType Type => FlowNodeType.SelectBranch;
        
        [Input("In")]
        public FlowNodePort Input;
        
        [CustomSetting("分支端口", false)] 
        public int PortCount = 2;
        
        [CustomSetting("分支列表"), ListReference(typeof(BranchData), nameof(BranchDatas))]
        public List<BranchData> BranchDatas = new List<BranchData>();
        
        [Output, SerializeField, HideInInspector]
        public FlowNodePort Branchs;
        
        [CustomPortBehavior(nameof(Branchs))]
        protected IEnumerable<PortData> OutputPortBehavior(List<SerializableEdge> edges)
        {

            for (int i = 0; i < PortCount; i++)
            {
                yield return new PortData
                {
                    displayName = $"分支{i}",
                    displayType = typeof(FlowNodePort),
                    identifier = i.ToString(),
                    acceptMultipleEdges = false,
                };
            }
        }
        
        public void AddBranch()
        {
            BranchDatas.Add(new BranchData());
        }

        public void RemoveBranch(int id)
        {
            BranchDatas.RemoveAt(id - 1);
        }

        public void ClearBranch()
        {
            BranchDatas.Clear();
        }

        public override void UpdateForExport()
        {
            foreach (var port in this.outputPorts)
            {
                if (port == null || string.IsNullOrEmpty(port.portData.identifier))
                {
                    continue;
                }
                
                var index = Math.Max(0, int.Parse(port.portData.identifier));
                if(index <= this.BranchDatas.Count - 1)
                    SetNextNode(this.BranchDatas[index], port);
            }
        }
        
        private void SetNextNode(BranchData data, NodePort port)
        {
            if (port.GetEdges().Count > 0)
            {
                data.NextID = port.GetEdges()[0].inputNode.computeOrder;
            }
            else
            {
                data.NextID = -1;
            }
        }
    }

    [Serializable]
    public class BranchDataNode
    {
        
    }
}