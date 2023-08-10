using System;
using System.Collections.Generic;
using System.Reflection;
using FlowEditor.Runtime;
using GraphProcessor;
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
        public MiniMap MiniMapView;
        public SortType SortType;
        public bool OrderPositive;

        private const string SORT_TYPE = "FlowEditor_SortType";
        private const string ORDER_TYPE = "FlowEditor_OrderType";


        public FlowGraphView(FlowGraphWindow window) : base(window)
        {
            this.Window = window;
            this.SortType = (SortType)Cookie.GetPublic(SORT_TYPE, 0);
            this.OrderPositive = Cookie.GetPublic(ORDER_TYPE, 0) == 0;
            
            Insert(0, new GraphGridView());
            // DrawOperationView();
            // DrawFileListView();
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
            this.MiniMapView = new MiniMap();
            this.MiniMapView.SetPosition(new Rect(0f, this.Window.position.height - 200f, 300f, 200f));
            Add(this.MiniMapView);
        }
        
        public void DelMiniMapView()
        {
            if (this.MiniMapView != null)
            {
                Remove(this.MiniMapView);
            }
        }

        public void RefreshFiles()
        {
            this.FileView.RefreshFiles();
        }
        
        public void SetSortType(SortType type)
        {
            Cookie.SetPublic(SORT_TYPE, (int)type);
            FileListView.m_Offset = Vector2.zero;
            this.SortType = type;
            this.RefreshFiles();
        }

        public void SetOrderType(bool positive)
        {
            Cookie.SetPublic(ORDER_TYPE, positive ? 0 : 1);
            FileListView.m_Offset = Vector2.zero;
            this.OrderPositive = positive;
            this.RefreshFiles();
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
    
    public enum SortType
    {
        /// <summary>
        /// 按时间
        /// </summary>
        Time,
        /// <summary>
        /// 按首字母
        /// </summary>
        Name,
        /// <summary>
        /// 使用频率
        /// </summary>
        Count
    }
}