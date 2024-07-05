using UnityEditor;
using UnityEngine;

namespace SeinoCat.FlowEditor.Editor
{
    public class GraphItemData
    {
        public string Name;
        public string Path;
        public string FlowPath;
        public bool IsFolder;
        
        public string DiskPath => Application.dataPath.Replace("Assets", Path);
        public string EventDiskPath = "Assets/Editor/FlowGraphs/";
        
        public static GraphItemData CreateDictory(string path)
        {
            GraphItemData data = new GraphItemData();
            data.IsFolder = true;
            data.Path = path;
            data.FlowPath = data.Path[data.EventDiskPath.Length..];
            data.Name = System.IO.Path.GetFileName(data.Path);
            return data;
        }
        
        public static GraphItemData CreateGraph(FlowGraphBase graph)
        {
            GraphItemData data = new GraphItemData();
            data.IsFolder = false;
            data.Path = AssetDatabase.GetAssetPath(graph);
            data.FlowPath = data.Path[data.EventDiskPath.Length..];
            data.Name = graph.name;
            return data;
        }
    }
}