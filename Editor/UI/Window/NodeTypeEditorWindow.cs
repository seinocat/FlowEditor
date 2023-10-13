using System.Collections.Generic;
using FlowEditor.Runtime;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace FlowEditor.Editor
{
    public class NodeTypeEditorWindow : OdinEditorWindow
    {
        [LabelText("客户端节点"), TableList(ShowIndexLabels = true)]
        public List<EditorNodeTypeData> EnumDatas;
        
        [LabelText("服务器节点"), TableList(ShowIndexLabels = true, IsReadOnly = true)]
        public List<EditorNetNodeTypeData> EnumNetDatas;
        
        [MenuItem("Tools/FlowEditor/NodeTypeEditor %#x")]
        public static void Open()
        {
            var window = GetWindow<NodeTypeEditorWindow>();
            window.OpenWindow();
        }

        public void OpenWindow()
        {
            this.Init();
            Show();
        }

        private void Init()
        {
            UpdateData();
        }

        [Button("更新数据")]
        public void UpdateData()
        {
            var types = NodeTypeUtils.GetNodeTypes();
            this.EnumDatas = types.Item1;
            this.EnumNetDatas = types.Item2;
        }
        
        [Button("客户端重排序")]
        public void SortClinet()
        {
            int startIndex = 1000;
            foreach (var type in this.EnumDatas)
            {
                if (type.type == FlowNodeType.End) continue;
                type.value = startIndex++;
            }
        }

        [Button("保存")]
        public void Save()
        {
            NodeTypeUtils.WriteType(this.EnumDatas, this.EnumNetDatas);
        }
    }


    
}