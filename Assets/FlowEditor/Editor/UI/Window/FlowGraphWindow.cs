using System.Collections.Generic;
using GraphProcessor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace FlowEditor.Editor
{
    public class FlowGraphWindow : BaseGraphWindow
    {
        public static string ResourcePath = "Assets/Editor/FlowGraphs";
        
        public FlowGraphBase Graph => this.graph as FlowGraphBase;
        public FlowGraphView GraphView => this.graphView as FlowGraphView;
        public static List<FlowGraphBase> GraphBases => FlowEditorUtils.LoadAllAssets<FlowGraphBase>(ResourcePath);
        
        [MenuItem("Tools/FlowEditor/Editor")]
        public static void OpenWindow()
        {
            var window = GetWindow<FlowGraphWindow>();
            if (GraphBases.Count == 0)
            {
                EditorUtility.DisplayDialog("提示", "当前没有配置!请通过右键Create/FlowGraph！", "确定");
                return;
            }
            window.InitializeGraph(GraphBases[0]);
            window.Show();
        }
        
        [OnOpenAsset(0)]
        public static bool OnBaseGraphOpened(int instanceID, int line)
        {
            var graph = EditorUtility.InstanceIDToObject(instanceID) as FlowGraphBase;
            if (graph)
            {
                var window = GetWindow<FlowGraphWindow>();
                window.InitializeGraph(graph);
                window.Show();
                graph.OpenCount++;
            }
            return graph;
        }
        
        [MenuItem("Assets/Create/FlowGraph", false, 10)]
        public static void CreateGraphAsset()
        {
            var graph = CreateInstance<FlowGraphBase>();
            var path = $"{ResourcePath}/New FlowGraph{GraphBases.Count + 1}.asset";
            AssetDatabase.CreateAsset(graph, path);
            AssetDatabase.Refresh();
        }
        
        protected override void InitializeWindow(BaseGraph baseGraph)
        {
            if (baseGraph != null && (baseGraph.nodes == null || baseGraph.nodes.Count == 0))
            {
                var rootNode = BaseNode.CreateFromType(typeof(RootNode), new Vector2(656.5f, 390.4f));
                baseGraph.AddNode(rootNode);
            }
            graphView = new FlowGraphView(this);
            graphView.Add(new FlowToolBarView(graphView));
            if (this.Graph != null)
            {
                this.Graph.m_Window = this;
            }
            rootView.Add(graphView);
        }
        
        
        
    }
    

}