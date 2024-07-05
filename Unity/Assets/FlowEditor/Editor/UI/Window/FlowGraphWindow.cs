using System.Collections.Generic;
using GraphProcessor;
using SeinoCat.FlowEditor.Runtime;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace SeinoCat.FlowEditor.Editor
{
    public class FlowGraphWindow : BaseGraphWindow
    {
        public static string ResourcePath = "Assets/Editor/FlowGraphs";
        
        public FlowGraphBase Graph => this.graph as FlowGraphBase;
        public FlowGraphView GraphView => this.graphView as FlowGraphView;
        public static List<FlowGraphBase> GraphBases => FlowUtils.LoadAllAssets<FlowGraphBase>(ResourcePath);
        
        public static string OPEN_GRAPH = "Flow_CurOpen";
        
        public void SetGraph(BaseGraph baseGraph)
        {
            this.graph = baseGraph;
        }
        
        [MenuItem("Tools/FlowEditor/Editor %#z")]
        public static void OpenWindow()
        {
            var window = GetWindow<FlowGraphWindow>();
            if (GraphBases.Count == 0)
            {
                EditorUtility.DisplayDialog("提示", "当前没有事件配置!请通过右键Create/Flow创建！", "确定");
                return;
            }

            var open = Cookie.GetPublic(OPEN_GRAPH, string.Empty);
            FlowGraphBase graph;
            if (string.IsNullOrEmpty(open))
            {
                graph = GraphBases[0];
            }
            else
            {
                graph = GraphBases.Find(x => x.name == open);
                if (graph == null) graph = GraphBases[0];
            }
            
            window.SetGraph(graph);
            window.Show();
        }
        
        [OnOpenAsset(0)]
        public static bool OnBaseGraphOpened(int instanceID, int line)
        {
            var graph = EditorUtility.InstanceIDToObject(instanceID) as FlowGraphBase;
            if (graph)
            {
                var window = GetWindow<FlowGraphWindow>();
                Cookie.SetPublic(OPEN_GRAPH, graph.name);
                window.InitializeGraph(graph);
                window.Show();
            }
            return graph;
        }
        
        [MenuItem("Assets/Create/Flow", false, 10)]
        public static void CreateGraphAsset()
        {
            var graph = CreateInstance<FlowGraphBase>();
            var path = $"{ResourcePath}/New Flow{GraphBases.Count + 1}.asset";
            AssetDatabase.CreateAsset(graph, path);
            AssetDatabase.Refresh();
        }
        
        protected override void InitializeWindow(BaseGraph baseGraph)
        {
            NodeGroupHelper.LoadConfig();
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