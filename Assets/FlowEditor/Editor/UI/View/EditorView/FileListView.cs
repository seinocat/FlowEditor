using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using FlowEditor.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace FlowEditor.Editor
{
    public sealed class FileListView : Blackboard
    {
        public static Vector2 ScrollOffset;
        
        private FlowGraphView m_GraphView;
        private FlowGraphWindow m_Window;
        private List<FlowGraphBase> m_GraphBases;
        private OrderOptionView m_OrderOptionView;
        private SearchView m_SearchView;
        private ScrollView m_Scroll;
        private bool m_Dirty = true;
        private const string WIDTH = "FlowEditor_FileList_Width";
        private const string HEIGHT = "FlowEditor_FileList_Heigh";
        private const string POSX = "FlowEditor_FileList_PosX";
        private const string POSY = "FlowEditor_FileList_PosY";
        
        public FileListView(FlowGraphView graphView)
        {
            this.m_Window = graphView.Window;
            this.m_GraphView = graphView;
            this.m_OrderOptionView = new OrderOptionView(m_GraphView);
            this.m_SearchView = new SearchView(m_GraphView);
            this.m_GraphBases = FlowEditorUtils.LoadAllAssets<FlowGraphBase>(FlowGraphWindow.ResourcePath);
            var width = Cookie.GetPublic(WIDTH, 340f);
            var height = Cookie.GetPublic(HEIGHT, 600f);
            var posx = Cookie.GetPublic(POSX, 0f);
            var posy = Cookie.GetPublic(POSY, 200f);
            this.SetPosition(new Rect(posx, posy, width, height));
            this.scrollable = true;
            FieldInfo fieldInfo = typeof(Blackboard).GetField("m_ScrollView", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                this.m_Scroll = fieldInfo.GetValue(this) as ScrollView;
            }
            this.DrawView();
            this.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }

        public void DrawView()
        {
            this.addItemRequested = OnAddBtnClick;
            this.RefreshFiles();
        }

        public void SetDirty()
        {
            this.m_Dirty = true;
            this.SetScrollOffset();
            this.RefreshFiles();
        }
        
        public void SetScrollOffset()
        {
            ScrollOffset = this.m_Scroll.scrollOffset;
        }
        
        public void OnAddBtnClick(Blackboard blackboard)
        {
            CustomGameEventWindow.OpenWindow("创建新流程", "New FlowGraph", (CreateName) =>
            {
                var graph = ScriptableObject.CreateInstance<FlowGraphBase>();
                var path = $"Assets/Editor/FlowGraphs/{CreateName}.asset";
                if (AssetDatabase.LoadAssetAtPath<FlowGraphBase>(path) == null)
                {
                    AssetDatabase.CreateAsset(graph, path);
                    AssetDatabase.Refresh();
                    this.SetDirty();
                    CustomGameEventWindow.CloseWindow();
                }
                else
                {
                    EditorUtility.DisplayDialog("提示", "该配置已存在!", "确定");
                }
            });
        }
        
        public void RefreshFiles()
        {
            this.contentContainer.Clear();

            var searchkey = this.m_SearchView.m_InputText;

            // 配置有增删，重新获取
            if (this.m_Dirty)
            {
                this.m_GraphBases = FlowEditorUtils.LoadAllAssets<FlowGraphBase>(FlowGraphWindow.ResourcePath);
                this.m_Dirty = false;
            }
            
            List<GraphItemView> itemList = new List<GraphItemView>();
            for (int i = 0; i < this.m_GraphBases.Count; i++)
            {
                var graphItem = this.m_GraphBases[i];
                
                if (!string.IsNullOrEmpty(searchkey) && !Regex.IsMatch(graphItem.name, searchkey, RegexOptions.IgnoreCase))
                {
                    continue;
                }

                var graphItemView = new GraphItemView(this.m_Window, graphItem);
                itemList.Add(graphItemView);
            }
            
            itemList.Sort((x, y) =>
            {
                int compare;
                switch (this.m_GraphView.SortType)
                {
                    case SortType.Time:
                        compare = x.Graph.Timestamp.CompareTo(y.Graph.Timestamp);
                        break;
                    case SortType.Name:
                        compare = string.Compare(x.Graph.name, y.Graph.name, StringComparison.Ordinal);
                        break;
                    case SortType.Count:
                        compare = -x.Graph.OpenCount.CompareTo(y.Graph.OpenCount);
                        break;
                    default:
                        compare = -x.Graph.Timestamp.CompareTo(y.Graph.Timestamp);
                        break;
                }

                return this.m_GraphView.OrderPositive ? compare : -compare;
            });

            // 将排序和搜索栏置顶
            this.Insert(0, this.m_OrderOptionView);
            this.Insert(1, this.m_SearchView);
            
            for (int i = 0; i < itemList.Count; i++)
            {
                this.contentContainer.Add(itemList[i]);
            }
            
            //Scroll定位
            this.m_Scroll.scrollOffset = ScrollOffset;
        }

        public void OnGeometryChanged(GeometryChangedEvent evt)
        {
            var rect = this.GetPosition();
            Cookie.SetPublic(WIDTH, rect.width);
            Cookie.SetPublic(HEIGHT, rect.height);
            Cookie.SetPublic(POSX, rect.position.x);
            Cookie.SetPublic(POSY, rect.position.y);
        }
    }
}