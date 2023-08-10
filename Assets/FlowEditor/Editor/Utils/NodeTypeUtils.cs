using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FlowEditor.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FlowEditor.Editor
{
    public static class NodeTypeUtils
    {
        public static string NodeTypePath => Application.dataPath + "{path}/{nodeTypeName}.cs";
        
        public static Tuple<List<EditorNodeTypeData>, List<EditorNetNodeTypeData>> GetNodeTypes()
        {
            List<EditorNodeTypeData> EnumDatas = new List<EditorNodeTypeData>();
            List<EditorNetNodeTypeData> EnumNetDatas = new List<EditorNetNodeTypeData>();
            var values = Enum.GetValues(typeof(FlowNodeType));
            foreach (var value in values)
            {
                var attribute = value.GetType().GetField(value.ToString()).GetCustomAttribute<LabelTextAttribute>();
                var name = Enum.GetName(typeof(FlowNodeType), value);
                var type = value.GetHashCode();
                if (value.GetHashCode() > FlowNodeType.End.GetHashCode())
                {
                    EditorNetNodeTypeData data = new EditorNetNodeTypeData();
                    data.name = name;
                    data.value = type;
                    data.desc = attribute?.Text;
                    data.type = (FlowNodeType)type;
                    EnumNetDatas.Add(data);
                }
                else
                {
                    EditorNodeTypeData data = new EditorNodeTypeData();
                    data.name = name;
                    data.value = type;
                    data.desc = attribute?.Text;
                    data.type = (FlowNodeType)value.GetHashCode();
                    EnumDatas.Add(data);
                }
            }

            return new Tuple<List<EditorNodeTypeData>, List<EditorNetNodeTypeData>>(EnumDatas, EnumNetDatas);
        }

        public static void AddNodeType(string name, string desc, bool client = true)
        {
            var types = GetNodeTypes();
            var clientTypes = types.Item1;
            var serverTypes = types.Item2;

            int maxIndex = 0;
            if (client)
            {
                foreach (var type in clientTypes)
                {
                    if (type.type == FlowNodeType.End) continue;

                    if (type.value > maxIndex)
                    {
                        maxIndex = type.value;
                    }
                }
                
                EditorNodeTypeData data = new EditorNodeTypeData();
                data.name = name;
                data.value = maxIndex + 1;
                data.desc = desc;
                clientTypes.Add(data);
                clientTypes.Sort((x, y) => x.value.CompareTo(y.value));
            }
            else
            {
                foreach (var type in serverTypes)
                {
                    if (type.type == FlowNodeType.CompleteFlag) continue;

                    if (type.value > maxIndex)
                    {
                        maxIndex = type.value;
                    }
                }
                
                EditorNetNodeTypeData data = new EditorNetNodeTypeData();
                data.name = name;
                data.value = maxIndex + 1;
                data.desc = desc;
                serverTypes.Add(data);
                serverTypes.Sort((x, y) => x.value.CompareTo(y.value));
            }
            
            WriteType(clientTypes, serverTypes);
        }
        
        public static void WriteType(List<EditorNodeTypeData> client, List<EditorNetNodeTypeData> server)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("using Sirenix.OdinInspector;");
            builder.AppendLine("");
            builder.AppendLine("namespace FlowEditor.Runtime");
            builder.AppendLine("{");
            builder.AppendLine("    // 注意：自动生成代码，请勿手动修改");
            builder.AppendLine("    [Serializable]");
            builder.AppendLine("    public enum FlowNodeType");
            builder.AppendLine("    {");
            //客户端节点
            builder.AppendLine("        #region 客户端节点");
            builder.AppendLine("");
            foreach (var type in client)
            {
                builder.AppendLine($"        [LabelText(\"{type.desc}\")]");
                builder.AppendLine($"        {type.name} = {type.value},");
                builder.AppendLine("");
            }
            builder.AppendLine("");
            builder.AppendLine("        #endregion");
            builder.AppendLine("");
            //服务器节点
            builder.AppendLine("        #region 服务器节点");
            builder.AppendLine("");
            foreach (var type in server)
            {
                builder.AppendLine($"        [LabelText(\"{type.desc}\")]");
                builder.AppendLine($"        {type.name} = {type.value},");
                builder.AppendLine("");
            }
            builder.AppendLine("");
            builder.AppendLine("        #endregion");
            builder.AppendLine("");
            
            builder.AppendLine("    }");
            builder.AppendLine("}");
            
            FlowWriter.WriteFile(builder, NodeTypePath);
        }

        
        
    }
}