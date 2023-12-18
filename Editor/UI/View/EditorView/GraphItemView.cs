using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FlowEditor.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace FlowEditor.Editor
{
    public class GraphItemView : VisualElement
    {
        public FlowGraphBase m_Graph;
        public GraphItemData m_Data;
        private FlowGraphWindow m_Window;
        private FileListView m_FileView;

        private Label m_FolderCollapse;
        private Image m_FolderIcon;
        private List<GraphItemView> m_SubViews;
        private int m_Indent;
        private string CollapseKey => $"FlowEvent_Collapse_{m_Data.FlowPath}";
        
        private DateTime lastClickTime;
        private float doubleClickThreshold = 0.4f;
        private Action<GraphItemView, ContextualMenuPopulateEvent> m_MenuPopulateAction;

        public bool Collapse { get; private set; }
        public bool IsSelected { get; private set; }

        public static GraphItemView CreateFolderView(FileListView fileView, FlowGraphWindow window, int indent, string path)
        {
            GraphItemData data = GraphItemData.CreateDictory(path);
            GraphItemView view = new GraphItemView(fileView, window, indent, data);
            
            return view;
        }
        
        public static GraphItemView CreateGraphView(FileListView fileView, FlowGraphWindow window, int indent, FlowGraphBase graph)
        {
            GraphItemData data = GraphItemData.CreateGraph(graph);
            GraphItemView view = new GraphItemView(fileView, window, indent, data, graph);
            return view;
        }
        
        public GraphItemView(FileListView fileView, FlowGraphWindow window, int indent, GraphItemData data, FlowGraphBase graph = null)
        {
            this.m_SubViews = new List<GraphItemView>();
            this.m_Window = window;
            this.m_FileView = fileView;
            this.m_Graph = graph;
            this.m_Data = data;
            this.m_Indent = indent;
            this.Collapse = Cookie.GetPublic(this.CollapseKey, 0) == 0;
            this.m_MenuPopulateAction = fileView.MenuPopulate;
            this.style.flexDirection = FlexDirection.Row;
            this.style.justifyContent = Justify.FlexStart;

            RegisterCallback<PointerDownEvent>((pointDown) =>
            {
                if (pointDown.button == 0)
                {
                    this.m_FileView.SetSelected(this);
                
                    // 获取当前点击的时间戳
                    DateTime currentClickTime = DateTime.Now;

                    // 计算时间差
                    TimeSpan timeSinceLastClick = currentClickTime - lastClickTime;

                    if (timeSinceLastClick.TotalSeconds < doubleClickThreshold)
                    {
                        if (this.m_Data.IsFolder)
                        {
                            DoCollapse(!this.Collapse);
                        }
                        else
                        {
                            OpenBtnClick();
                        }

                        // 重置上一次点击的时间戳
                        lastClickTime = DateTime.MinValue;
                    }
                    else
                    {
                        // 更新上一次点击的时间戳
                        lastClickTime = currentClickTime;
                    }
                }

                if (pointDown.button == 1)
                {
                    if (!this.IsSelected)
                    {
                        this.m_FileView.SetSelected(this);
                    }
                }
            });
            
            
            this.AddManipulator(new ContextualMenuManipulator(BuildContextMenu));
        }

        public void SetSelect(bool value)
        {
            this.IsSelected = value;
            this.style.backgroundColor = value ? new Color(0.22f, 0.4f, 0.67f) : new Color(0f,0f,0f,0f);
        }
        
        private void BuildContextMenu(ContextualMenuPopulateEvent evt)
        {
            m_MenuPopulateAction?.Invoke(this, evt);
        }

        public void DrawView()
        {
            if (m_Data.IsFolder)
            {
                DrawFolder();
                if (!Collapse)
                {
                    DrawSubFolder();
                    DrawSubGraph();
                }
            }
            else
            {
                DrawGraph();
            }
        }

        public void DoCollapse(bool value)
        {
            if (this.Collapse == value) return;
            
            this.Collapse = value;
            Cookie.SetPublic(this.CollapseKey, this.Collapse ? 0 : 1);
            if (ExistSubFolderOrFile())
                this.m_FolderCollapse.text = this.Collapse ? "▶" : "▼";
            else
                this.m_FolderCollapse.text = "    ";
            this.m_FolderIcon.sprite = this.Collapse ? this.m_Window.GraphView.FolderClose : this.m_Window.GraphView.FolderOpen;
            if (!this.Collapse)
            {
                DrawSubFolder();
                DrawSubGraph();
            }
            else
            {
                ClearSubView();
            }
        }

        private void DrawFolder()
        {
            this.contentContainer.style.alignItems = Align.Center;
            this.contentContainer.style.justifyContent = Justify.Center;
            
            this.m_FolderCollapse = new Label();
            this.m_FolderCollapse.style.fontSize = 8;
            if (ExistSubFolderOrFile())
                this.m_FolderCollapse.text = this.Collapse ? "▶" : "▼";
            else
                this.m_FolderCollapse.text = "    ";
            this.m_FolderCollapse.style.marginLeft = 20 * this.m_Indent;
            Add(m_FolderCollapse);
            
            this.m_FolderCollapse.RegisterCallback<PointerDownEvent>((pointDown) =>
            {
                if (this.m_Data.IsFolder)
                {
                    DoCollapse(!this.Collapse);
                }
            });
            
            this.m_FolderIcon = new Image();
            m_FolderIcon.sprite = this.Collapse ? this.m_Window.GraphView.FolderClose : this.m_Window.GraphView.FolderOpen;
            m_FolderIcon.style.width = 15;
            m_FolderIcon.style.height = 15;
            Add(m_FolderIcon);

            Label graphName = new Label();
            graphName.text = this.m_Data.Name;
            graphName.style.fontSize = 12;
            graphName.style.flexGrow = 1;
            Add(graphName);
        }
        
        private bool ExistSubFolderOrFile()
        {
            var folders = FlowUtils.GetSubFolders(this.m_Data.Path);
            var graphs = FlowUtils.LoadAllAssets<FlowGraphBase>(this.m_Data.Path, SearchOption.TopDirectoryOnly);
            return folders.Count > 0 || graphs.Count > 0;
        }

        
        private void DrawGraph()
        {
            this.contentContainer.style.alignItems = Align.Center;
            this.contentContainer.style.justifyContent = Justify.Center;
            
            Image graphIcon = new Image();
            graphIcon.sprite = this.m_Window.GraphView.GraphIcon;
            graphIcon.style.marginLeft = 20 * this.m_Indent;
            graphIcon.style.width = 15;
            graphIcon.style.height = 15;
            Add(graphIcon);
            
            Label graphName = new Label();
            graphName.text = this.m_Graph.name;
            graphName.style.flexGrow = 1;
            
            if (this.m_Window.Graph == this.m_Graph)
            {
                graphName.style.color = Color.cyan;
            }
            Add(graphName);
        }
        
        private void DrawSubFolder()
        {
            var curIndex = this.m_FileView.contentContainer.IndexOf(this);
            var folders = FlowUtils.GetSubFolders(this.m_Data.Path);
            curIndex++;
            for (int i = 0; i < folders.Count; i++)
            {
                var view = CreateFolderView(this.m_FileView, this.m_Window, this.m_Indent + 1, folders[i]);
                this.m_SubViews.Add(view);
                this.m_FileView.Insert(curIndex, view);
                view.DrawView();
                if (view.m_SubViews.Count > 0)
                {
                    curIndex += view.GetSubViewCount() + 1;
                }
                else
                {
                    curIndex++;
                }
                
            }
        }

        private void DrawSubGraph()
        {
            var curIndex = this.m_FileView.IndexOf(this) + this.m_SubViews.Count + 1;
            var graphs = FlowUtils.LoadAllAssets<FlowGraphBase>(this.m_Data.Path, SearchOption.TopDirectoryOnly);
            graphs.Sort((x, y) => string.Compare(x.name, y.name, StringComparison.Ordinal));
            
            for (int i = 0; i < graphs.Count; i++)
            {
                var view = CreateGraphView(this.m_FileView, this.m_Window, this.m_Indent + 1, graphs[i]);
                this.m_SubViews.Add(view);
                this.m_FileView.Insert(curIndex, view);
                view.DrawView();
                if (view.m_SubViews.Count > 0)
                {
                    curIndex += view.GetSubViewCount() + 1;
                }
                else
                {
                    curIndex++;
                }
            }
        }

        private void ClearSubView()
        {
            for (int i = 0; i < this.m_SubViews.Count; i++)
            {
                this.m_SubViews[i].ClearSubView();
                this.m_FileView.Remove(this.m_SubViews[i]);
            }
            this.m_SubViews.Clear();
        }
        
        public int GetSubViewCount()
        {
            return m_SubViews.Count + m_SubViews.Sum(view => view.GetSubViewCount());
        }

        private void OpenBtnClick()
        {
            if (this.m_Window.Graph != this.m_Graph)
            {
                this.m_FileView.SetScrollOffset();
                Cookie.SetPublic(FlowGraphWindow.OPEN_GRAPH, this.m_Graph.name);
                this.m_Window.InitializeGraph(this.m_Graph);
            }
        }


    }
}