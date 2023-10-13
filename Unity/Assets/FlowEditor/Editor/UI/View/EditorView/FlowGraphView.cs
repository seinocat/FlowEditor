using System;
using System.Collections.Generic;
using System.Reflection;
using FlowEditor.Runtime;
using GraphProcessor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace FlowEditor.Editor
{
    public class FlowGraphView : BaseGraphView
    {
        private class GraphGridView : GridBackground { }

        public FlowGraphWindow Window;
        public FileListView FileView;
        public OperationView OperationView;
        public FlowMiniMapView MiniMapView;
        
        public Sprite FolderOpen;
        public Sprite FolderClose;
        public Sprite GraphIcon;

        public FlowGraphView(FlowGraphWindow window) : base(window)
        {
            this.Window = window;
            this.FolderOpen = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/FlowEditor/Resource/Icon/folder1.png");
            this.FolderClose = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/FlowEditor/Resource/Icon/folder0.png");
            this.GraphIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/FlowEditor/Resource/Icon/graph.png");
            
            Insert(0, new GraphGridView());
        }

        public void DrawOperationView()
        {
            this.OperationView = new OperationView(this);
            Add(this.OperationView);
        }

        public void DelOperationView()
        {
            if (this.OperationView != null)
            {
                Remove(this.OperationView);
            }
        }

        public void DrawFileListView()
        {
            this.FileView = new FileListView(this);
            Add(this.FileView);
        }
        
        public void DelFileListView()
        {
            if (this.FileView != null)
            {
                Remove(this.FileView);
            }
        }

        public void DrawMiniMapView()
        {
            this.MiniMapView = new FlowMiniMapView();
            Add(this.MiniMapView);
        }
        
        public void DelMiniMapView()
        {
            if (this.MiniMapView != null)
            {
                Remove(this.MiniMapView);
            }
        }
        
        public override IEnumerable<(string path, Type type)> FilterCreateNodeMenuEntries()
        {
            foreach (var nodeMenuItem in NodeProvider.GetNodeMenuEntries(graph))
            {
                var eventNodeAttr = nodeMenuItem.type.GetCustomAttribute<GameEventNodeAttribute>();
                var discardAttr = nodeMenuItem.type.GetCustomAttribute<DiscardNodeAttribute>();

                if (eventNodeAttr != null && discardAttr == null)
                {
                    yield return nodeMenuItem;
                }
            }
        }
    }
}