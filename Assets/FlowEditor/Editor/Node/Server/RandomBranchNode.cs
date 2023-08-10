using System;
using System.Collections.Generic;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEngine;

namespace FlowEditor.Editor
{

    [NodeMenuItem("逻辑/随机分支(客户端)"), GameEventNode, Serializable]
    public class RandomBranchNode : ServerNodeBase
    {
        public override string name => "随机分支(客户端)";

        public override FlowNodeType Type => FlowNodeType.RandomBranch;
        
        [Input("In")]
        public EventNodePort Input;
        
        [CustomSetting("分支端口", false)] 
        public int PortCount = 2;
        
        [Output, SerializeField, HideInInspector]
        public EventNodePort Branchs;
        
        [CustomSetting("分支列表"), ListReference(typeof(RandomBranchData), nameof(BranchDatas))]
        public List<RandomBranchData> BranchDatas = new List<RandomBranchData>();
        
        [CustomPortBehavior(nameof(Branchs))]
        protected IEnumerable<PortData> OutputPortBehavior(List<SerializableEdge> edges)
        {

            for (int i = 0; i < PortCount; i++)
            {
                yield return new PortData
                {
                    displayName = $"分支{i}",
                    displayType = typeof(EventNodePort),
                    identifier = i.ToString(),
                    acceptMultipleEdges = false,
                };
            }
        }
        
        public void AddBranch()
        {
            BranchDatas.Add(new RandomBranchData());
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
        
        private void SetNextNode(RandomBranchData data, NodePort port)
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
}