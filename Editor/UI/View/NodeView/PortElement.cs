﻿using System.Reflection;
using GraphProcessor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace FlowEditor.Editor
{
    public class PortElement : PortView
    {
        public VisualElement connector;
        public Label type;
        public Label valueElement;
        public System.Action<PortData, int> OnKeyValueChangeCallback;

        private PortElement(Direction direction, FieldInfo fieldInfo, PortData portData, BaseEdgeConnectorListener edgeConnectorListener)
            : base(direction, fieldInfo, portData, edgeConnectorListener)
        {
            InitElement();
            InitEvent();
            ApplyStyle();
        }

        private void InitElement()
        {
            connector = this.Q<VisualElement>(nameof(connector));
            type = this.Q<Label>(nameof(type));
            valueElement = new Label();
            valueElement.name = nameof(valueElement);
            type.Add(valueElement);
        }

        private void InitEvent()
        {
            this.valueElement.RegisterValueChangedCallback(OnKeyValueChange);
        }

        private void OnKeyValueChange(ChangeEvent<string> evt)
        {
            OnKeyValueChangeCallback?.Invoke(portData, int.Parse(evt.newValue));
        }

        private void ApplyStyle()
        {
            this.valueElement.style.width = new StyleLength(new Length(99, LengthUnit.Percent));
            this.connector.style.width = 30;
            this.type.style.width = 40;
            this.type.style.color = new Color(1, 1, 1, 0);
        }

        public new static PortElement CreatePortView(Direction direction, FieldInfo fieldInfo, PortData portData, BaseEdgeConnectorListener edgeConnectorListener)
        {
            var pv = new PortElement(direction, fieldInfo, portData, edgeConnectorListener);
            return pv;
        }
    }
}