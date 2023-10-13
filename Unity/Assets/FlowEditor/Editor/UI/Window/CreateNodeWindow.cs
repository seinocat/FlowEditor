using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FlowEditor.Runtime;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace FlowEditor.Editor
{
    public class CreateNodeWindow : OdinEditorWindow
    {
        [Serializable]
        public enum NodeType
        {
            [LabelText("客户端节点")]
            Client,
            [LabelText("服务器节点")]
            Server,
            [LabelText("Editor节点")]
            Editor,
        }

        [Serializable]
        public enum FieldType
        {
            [LabelText("Int")] 
            IntType,
            
            [LabelText("String")] 
            StringType,
            
            [LabelText("Vector3")] 
            Vector3Type,
            
            [LabelText("Object")] 
            ObjectType,
            
            [LabelText("List")] 
            ListType,
            
            [LabelText("Custom")] 
            CustomType,
        }


        [Serializable]
        public class FieldData
        {
            [LabelText("类型")] 
            public FieldType fieldType;

            [LabelText("List类型"), ShowIf("fieldType", FieldType.ListType)]
            public FieldType listType;

            [LabelText("自定义类型名"), ShowIf("@fieldType == FieldType.CustomType || listType == FieldType.CustomType")]
            public string typeName;

            [LabelText("字段名")] 
            public string fieldName;
            
            [LabelText("备注")] 
            public string desc;
        }

        [MenuItem("Tools/FlowEditor/CreateNode")]
        public static void Open()
        {
            var window = GetWindow<CreateNodeWindow>();
            window.Show();
        }

        [LabelText("类型")] 
        public NodeType nodeType;

        [LabelText("类名")] 
        public string nodeName;

        [LabelText("分类")]
        public string groupName;
        
        [LabelText("备注")] 
        public string nodeDesc;
        
        [LabelText("字段")] 
        public List<FieldData> fieldList = new List<FieldData>();

        [Button("创建节点")]
        public void Create()
        {
            if (!Check()) return;
            
            this.WirteEditorNode();
            this.WirteRuntimeNode();
            this.WriteNodeType();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("提示", "创建成功!", "确定");
            this.Close();
            
        }


        private bool Check()
        {
            var typeNames = Enum.GetNames(typeof(FlowNodeType)).ToList();
            if (typeNames.Contains(this.nodeName))
            {
                EditorUtility.DisplayDialog("提示", $"{this.nodeName}节点已经存在!", "确定");
                return false;
            }
            return true;
        }

        public void WirteEditorNode()
        {
            StringBuilder builder = new StringBuilder();
            bool hasList = this.fieldList.Exists(x => x.fieldType == FieldType.ListType);
            bool hasCustom = this.fieldList.Exists(x => x.fieldType == FieldType.CustomType || x.listType == FieldType.CustomType);
            string portName = nodeType == NodeType.Editor ? "EditorPort" : "EventNodePort";
            
            builder.AppendLine("using System;");
            builder.AppendLine("using GraphProcessor;");
            builder.AppendLine("using FlowEditor.Runtime;");
            builder.AppendLine("using UnityEngine;");
            
            if (hasList) builder.AppendLine("using System.Collections.Generic;");

            builder.AppendLine("");
            builder.AppendLine("namespace FlowEditor.Editor");
            builder.AppendLine("{");
            builder.AppendLine($"    [NodeMenuItem(\"{this.groupName}/{this.nodeDesc}\"), GameEventNode, Serializable]");
            builder.AppendLine($"    public class {this.nodeName}Node : {Enum.GetName(typeof(NodeType), this.nodeType)}NodeBase");
            builder.AppendLine("    {");
            builder.AppendLine($"        public override string name => \"{this.nodeDesc}\";");
            if (this.nodeType != NodeType.Editor)
            {
                builder.AppendLine("");
                builder.AppendLine($"        public override ENodeType Type => ENodeType.{this.nodeName};");
            }
            builder.AppendLine("");
            builder.AppendLine("        [Input(\"In\")]");
            builder.AppendLine($"        public {portName} Input;");
            builder.AppendLine("");
            builder.AppendLine("        [Output(\"Out\", false)]");
            builder.AppendLine("        public EventNodePort Output;");

            foreach (var field in this.fieldList)
            {
                builder.AppendLine("");
                
                if (field.fieldType == FieldType.ListType)
                {
                    if (field.listType == FieldType.CustomType)
                    {
                        builder.AppendLine($"        [CustomSetting(\"{field.desc}\"), ListReference(typeof({field.typeName}), nameof({field.fieldName}))] ");
                        builder.AppendLine($"        public List<{field.typeName}> {field.fieldName};");
                    }
                    else
                    {
                        builder.AppendLine($"        [CustomSetting(\"{field.desc}\"), ListReference(typeof({GetTypeName(field.listType)}), nameof({field.fieldName}))] ");
                        builder.AppendLine($"        public List<{GetTypeName(field.listType)}> {field.fieldName};");
                    }
                }
                else if(field.fieldType == FieldType.CustomType)
                {
                    builder.AppendLine($"        [CustomSetting(\"{field.desc}\")] ");
                    builder.AppendLine($"        public {field.typeName} {field.fieldName};");
                }
                else
                {
                    builder.AppendLine($"        [CustomSetting(\"{field.desc}\")] ");
                    builder.AppendLine($"        public {GetTypeName(field.fieldType)} {field.fieldName};");
                }

            }
            
            builder.AppendLine("    }");
            builder.AppendLine("}");
            
            this.Save(builder);
        }

        public void WirteRuntimeNode()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("namespace FlowEditor.Editor");
            builder.AppendLine("{");
            builder.AppendLine($"    public partial class {this.nodeName}Node : EventNodeBase");
            builder.AppendLine("    {");
            builder.AppendLine("         protected override void OnEnter()");
            builder.AppendLine("         {");
            builder.AppendLine("");
            builder.AppendLine("         }");
            builder.AppendLine("");
            builder.AppendLine("         protected override EventStatus OnRunning()");
            builder.AppendLine("         {");
            builder.AppendLine("            return EventStatus.Success;");
            builder.AppendLine("         }");
            builder.AppendLine("    }");
            builder.AppendLine("}");
            
            this.Save(builder, false);
        }

        public void WriteNodeType()
        {
            if (this.nodeType != NodeType.Editor)
            {
                NodeTypeUtils.AddNodeType(this.nodeName, this.nodeDesc, this.nodeType == NodeType.Client);
            }
        }

        public string GetTypeName(FieldType type)
        {
            string typeName;
            switch (type)
            {
                case FieldType.IntType:
                    typeName = "int";
                    break;
                case FieldType.StringType:
                    typeName = "string";
                    break;
                case FieldType.Vector3Type:
                    typeName = "Vector3";
                    break;
                case FieldType.ObjectType:
                    typeName = "Object";
                    break;
                case FieldType.ListType:
                    typeName = "List";
                    break;
                default:
                    typeName = "int";
                    break;
            }

            return typeName;
        }

        public void Save(StringBuilder builder, bool editorNode = true)
        {
            string dicPath;
            switch (this.nodeType)
            {
                case NodeType.Client:
                    dicPath = "Client";
                    break;
                case NodeType.Server:
                    dicPath = "Server";
                    break;
                case NodeType.Editor:
                    dicPath = "EditorNode";
                    break;
                default:
                    dicPath = "Client";
                    break;
            }

            string path;
            if (editorNode)
            {
                path = Application.dataPath + $"/{this.nodeName}Node.cs";
            }
            else
            {
                path = Application.dataPath + $"/{dicPath}/{this.nodeName}Node.cs";
            }
            
            FileStream fileStream = new FileStream(path, FileMode.Create);
            StreamWriter fileWriter = new StreamWriter(fileStream, Encoding.UTF8);
            try
            {
                fileWriter.Write(builder.ToString());
                fileWriter.Flush();
            }
            catch (Exception ex)
            {
                Debug.LogError($"JsonNodes生成失败 :{ex}");
            }
            finally
            {
                fileWriter.Close();
                fileStream.Close();
            }
            
            
        }
        
    }
}