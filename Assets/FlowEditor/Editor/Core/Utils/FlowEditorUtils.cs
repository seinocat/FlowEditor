using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// <summary>
        /// 获取路径下所有指定类型的资源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="option"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> LoadAllAssets<T>(string path, SearchOption option = SearchOption.AllDirectories) where T : UnityEngine.Object
        {
            List<T> list = new List<T>();
            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] files = dir.GetFiles("*", option);

                foreach (var file in files)
                {
                    if (file.Name.EndsWith(".meta")) continue;

                    string assetName = file.FullName;
                    string assetPath = assetName[assetName.IndexOf("Assets", StringComparison.Ordinal)..];
                    T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                    if (asset)
                    {
                        list.Add(asset);
                    }
                }
            }
            
            return list;
        }

        public static List<string> GetSubFolders(string path, bool onlyName = false)
        {

            var folders = AssetDatabase.GetSubFolders(path).ToList();

            if (onlyName)
            {
                List<string> subFloders = new List<string>();
                for (int i = 0; i < folders.Count; i++)
                {
                    subFloders.Add(Path.GetFileName(folders[i]));
                }
                return subFloders;
            }

            return folders;
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