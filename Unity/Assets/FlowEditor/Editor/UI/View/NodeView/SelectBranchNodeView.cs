﻿using System.Reflection;
using GraphProcessor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace SeinoCat.FlowEditor.Editor
{
    [NodeCustomEditor(typeof(SelectBranchNode))]
    public class SelectBranchNodeView : BaseNodeView
    {
        private PropertyField PortCount;

        private SelectBranchNode node => nodeTarget as SelectBranchNode;
        
        public override void Enable()
        {
            base.Enable();

            InitElement();
            InitEvent();
        }
        
        private void InitElement()
        {
            PortCount = this.Q<PropertyField>(nameof(PortCount));
        }

        private void InitEvent()
        {
            PortCount.RegisterValueChangeCallback(OnPortCount);
        }

        private void OnPortCount(SerializedPropertyChangeEvent evt)
        {
            var changeCount = System.Math.Abs(evt.changedProperty.intValue - node.BranchDatas.Count);
            
            if (evt.changedProperty.intValue == 0)
            {
                node.ClearBranch();
            }
            else if (evt.changedProperty.intValue < node.BranchDatas.Count)
            {
                node.BranchDatas.RemoveRange(evt.changedProperty.intValue, node.BranchDatas.Count - evt.changedProperty.intValue);
            }
            else if (evt.changedProperty.intValue > node.BranchDatas.Count)
            {
                for (int i = changeCount; i > 0; i--)
                {
                    node.AddBranch();
                }
            }
            this.ForceUpdatePorts();
        }
        
        protected override PortView CreatePortView(Direction direction, FieldInfo fieldInfo, PortData portData, BaseEdgeConnectorListener listener)
        {
            if (int.TryParse(portData.identifier, out var id))
            {
                var port = PortElement.CreatePortView(direction, fieldInfo, portData, listener);
                port.OnKeyValueChangeCallback += OnKeyValueChange;
                port.valueElement.text = portData.displayName;
                return port;
            }
            return base.CreatePortView(direction, fieldInfo, portData, listener);
        }
        
        private void OnKeyValueChange(PortData data, int value)
        {
            if (int.TryParse(data.identifier, out var id))
            {
                data.identifier = value.ToString();
                data.displayName = value.ToString();
            }
        }
    }
}