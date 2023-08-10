using System;
using System.Collections.Generic;
using System.IO;
using GraphProcessor;
using UnityEditor;

namespace FlowEditor.Editor
{
    public static class FlowEditorUtils
    {
        /// <summary>
        /// 获取路径下所有指定类型的资源
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> LoadAllAssets<T>(string path) where T : UnityEngine.Object
        {
            List<T> list = new List<T>();
            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                
                FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    if (file.Name.EndsWith(".meta")) continue;

                    string assetName = file.FullName;
                    string assetPath = assetName.Substring(assetName.IndexOf("Assets", StringComparison.Ordinal));
                    T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                    if (asset)
                    {
                        list.Add(asset);
                    }
                }
            }
            
            return list;
        } 
        
        public static string GetSortTypeName(this SortType type)
        {
            switch (type)
            {
                case SortType.Time:
                    return "按创建时间";
                case SortType.Name:
                    return "按名字";
                case SortType.Count:
                    return "按使用频率";
                default:
                    return "按名字";
            }
        }


        public static List<BaseNode> GetOutputNodeList(this BaseNode node)
        {
            List<BaseNode> nodeList = new List<BaseNode>();
            var nodes = node.GetOutputNodes();
            foreach (var item in nodes)
            {
                nodeList.Add(item);
            }
            return nodeList;
        }
        
        public static List<BaseNode> GetInputNodeList(this BaseNode node)
        {
            List<BaseNode> nodeList = new List<BaseNode>();
            var nodes = node.GetInputNodes();
            foreach (var item in nodes)
            {
                nodeList.Add(item);
            }
            return nodeList;
        }
    }
}