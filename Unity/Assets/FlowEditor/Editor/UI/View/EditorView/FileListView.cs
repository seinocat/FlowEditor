﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Seino.Utils;
using SeinoCat.FlowEditor.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace SeinoCat.FlowEditor.Editor
{
    public sealed class FileListView : Blackboard
    {
        #region 字段
        
        public static Vector2 m_Offset;
        public static List<EventFlagNode> ExportData = new();
        
        private FlowGraphWindow m_Window;
        private SearchView m_SearchView;
        private ScrollView m_Scroll;
        private GraphItemView m_CurSelected;
        private List<GraphItemView> m_SelectedList = new();
        private List<FlowGraphBase> m_GraphsList = new();
        private bool m_Shift;
        private bool m_Ctrl;
        private const string WIDTH = "FlowEditor_FileList_Width";
        private const string HEIGHT = "FlowEditor_FileList_Heigh";
        
        #endregion
        
        #region 构造

        public FileListView(FlowGraphView graphView)
        {
            this.m_Window = graphView.Window;
            this.m_SearchView = new SearchView(graphView);
            this.scrollable = true;
            this.addItemRequested = OnAddBtnClick;
            FieldInfo fieldInfo = typeof(Blackboard).GetField("m_ScrollView", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null) this.m_Scroll = fieldInfo.GetValue(this) as ScrollView;
            var width = Cookie.GetPublic(WIDTH, 425f);
            var height = Cookie.GetPublic(HEIGHT, 1000f);
            FlowSetting.Initialize();
            SetPosition(new Rect(0, 23, width, height));
            Register();
            DrawView();
        }

        private void Register()
        {
            RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
            RegisterCallback<KeyUpEvent>((evt) =>
            {
                if (!evt.shiftKey) this.m_Shift = false;
                if (!evt.ctrlKey) this.m_Ctrl = false;
                
                if (evt.keyCode == KeyCode.Return)
                {
                    if (m_CurSelected != null && !m_CurSelected.m_Data.IsFolder && this.m_Window.Graph != this.m_CurSelected.m_Graph)
                    {
                        SetScrollOffset();
                        this.m_Window.InitializeGraph(this.m_CurSelected.m_Graph);
                    }
                }
                if (evt.keyCode == KeyCode.Delete)
                {
                    BatchDelete();
                }
            });
            
            RegisterCallback<KeyDownEvent>((evt) =>
            {
                if (evt.keyCode == KeyCode.UpArrow)
                {
                    if (m_CurSelected != null)
                    {
                        int index = this.contentContainer.IndexOf(m_CurSelected);
                        index = Mathf.Max(0, --index);
                        SetSelected(this.contentContainer[index] as GraphItemView);
                    }
                }
                if (evt.keyCode == KeyCode.DownArrow)
                {
                    if (m_CurSelected != null)
                    {
                        int index = this.contentContainer.IndexOf(m_CurSelected);
                        index = Mathf.Min(this.contentContainer.childCount - 1, ++index);
                        SetSelected(this.contentContainer[index] as GraphItemView);
                    }
                }
                if (evt.keyCode == KeyCode.LeftArrow)
                {
                    if (m_CurSelected != null)
                    {
                        if (m_CurSelected.m_Data.IsFolder)
                        {
                            if (m_CurSelected.Collapse)
                            {
                                var index = IndexOf(m_CurSelected);
                                var start = Mathf.Max(0, index - 1);
                                for (int i = start; i >= 0; i--)
                                {
                                    if (this.contentContainer[i] is GraphItemView itemView)
                                    {
                                        if (itemView.m_Data.IsFolder && !itemView.Collapse)
                                        {
                                            SetSelected(itemView);
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                m_CurSelected.SetCollapse(true);
                            }
                        }
                        else
                        {
                            var index = IndexOf(m_CurSelected);
                            var start = Mathf.Max(0, index - 1);
                            for (int i = start; i >= 0; i--)
                            {
                                if (this.contentContainer[i] is GraphItemView itemView)
                                {
                                    if (itemView.m_Data.IsFolder)
                                    {
                                        SetSelected(itemView);
                                        break;
                                    }
                                }
                            }
                        }
                        
                        
                        
                    }
                }
                if (evt.keyCode == KeyCode.RightArrow)
                {
                    if (m_CurSelected != null)
                    {
                        if (m_CurSelected.m_Data.IsFolder)
                        {
                            if (!m_CurSelected.Collapse)
                            {
                                var index = IndexOf(m_CurSelected);
                                var start = Mathf.Min(childCount - 1, index + 1);
                                for (int i = start; i < childCount; i++)
                                {
                                    if (this.contentContainer[i] is GraphItemView itemView)
                                    {
                                        if (itemView.m_Data.IsFolder && itemView.Collapse)
                                        {
                                            SetSelected(itemView);
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                m_CurSelected.SetCollapse(false);
                            } 
                        }
                        else
                        {
                            var index = IndexOf(m_CurSelected);
                            var start = Mathf.Min(childCount - 1, index + 1);
                            for (int i = start; i < childCount; i++)
                            {
                                if (this.contentContainer[i] is GraphItemView itemView)
                                {
                                    if (itemView.m_Data.IsFolder)
                                    {
                                        SetSelected(itemView);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (evt.shiftKey) this.m_Shift = true;
                if (evt.ctrlKey) this.m_Ctrl = true;
            });
        }
        
        private void DrawView()
        {
            this.m_GraphsList = FlowUtils.LoadAllAssets<FlowGraphBase>(FlowSetting.GraphRootPath);
            this.contentContainer.Clear();
            ExportData.Clear();
            ClearSelected();
            var searchkey = this.m_SearchView.m_InputText;
            Add(this.m_SearchView);

            foreach (var graphBase in m_GraphsList)
            {
                foreach (var node in graphBase.nodes)
                {
                    if (node is EventFlagNode eventNode)
                    {
                        ExportData.Add(eventNode);
                    }
                }
                
            }

            if (string.IsNullOrEmpty(searchkey))
            {
                DrawHierarchy();
            }
            else
            {
                DrawSearch(searchkey);
            }
            
            if (this.m_Window.Graph != null)
            {
                if (m_CurSelected == null)
                {
                    foreach (var view in this.contentContainer.Children())
                    {
                        if (view is GraphItemView item)
                        {
                            if (item.m_Graph == this.m_Window.Graph)
                            {
                                SetSelected(item);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var view in this.contentContainer.Children())
                    {
                        if (view is GraphItemView item)
                        {
                            if (item.m_Graph == m_CurSelected.m_Graph)
                            {
                                SetSelected(item);
                                break;
                            }
                        }
                    }
                    
                }
            }
            //Scroll定位
            this.m_Scroll.scrollOffset = m_Offset;
        }
        
        private void DrawHierarchy()
        {
            var folders = FlowUtils.GetSubFolders(FlowSetting.GraphRootPath);
            for (int i = 0; i < folders.Count; i++)
            {
                var view = GraphItemView.CreateFolderView(this, this.m_Window, 0, folders[i]);
                this.contentContainer.Add(view);
                view.DrawView();
            }
            
            var graphBases = FlowUtils.LoadAllAssets<FlowGraphBase>(FlowSetting.GraphRootPath, SearchOption.TopDirectoryOnly);
            for (int i = 0; i < graphBases.Count; i++)
            {
                var view = GraphItemView.CreateGraphView(this, this.m_Window, 0, graphBases[i]);
                this.contentContainer.Add(view);
                view.DrawView();
            }
        }

        private void DrawSearch(string searchkey)
        {
            List<GraphItemView> itemList = new List<GraphItemView>();
            for (int i = 0; i < this.m_GraphsList.Count; i++)
            {
                var graphItem = this.m_GraphsList[i];
                bool isTarget = Regex.IsMatch(graphItem.name, searchkey, RegexOptions.IgnoreCase);

                if (!isTarget)
                {
                    var eventNode = graphItem.GetNode<EventFlagNode>();
                    foreach (var node in eventNode)
                    {
                        if (Regex.IsMatch(node.EventID.ToString(), searchkey, RegexOptions.IgnoreCase))
                            isTarget = true;
                        if (Regex.IsMatch(node.EventName, searchkey, RegexOptions.IgnoreCase))
                            isTarget = true;
                        if (Regex.IsMatch(node.Text, searchkey, RegexOptions.IgnoreCase))
                            isTarget = true;
                    }

                    bool nodetype = searchkey.StartsWith("n:");
                    if (nodetype)
                    {
                        string nodename = searchkey.Split("n:")[1];
                        if (nodename.IsNullOrEmpty()) return;
                        var nodes = graphItem.GetNode<FlowNodeBase>();
                        foreach (var node in nodes)
                        {
                            if (Regex.IsMatch(node.name, nodename, RegexOptions.IgnoreCase))
                            {
                                isTarget = true;
                                break;
                            }
                            if (Regex.IsMatch(node.GetType().Name, nodename, RegexOptions.IgnoreCase))
                            {
                                isTarget = true;
                                break;
                            }
                        }
                    }
                }
                
                if (!isTarget)
                {
                    var eventNode = graphItem.GetNode<InteractItemNode>();
                    foreach (var node in eventNode)
                    {
                        if (Regex.IsMatch(node.Text, searchkey, RegexOptions.IgnoreCase))
                            isTarget = true;
                    }
                }
                
                if (!isTarget) continue;

                var graphItemView = GraphItemView.CreateGraphView(this, this.m_Window, 0, graphItem);
                itemList.Add(graphItemView);
            }
            
            itemList.Sort((x,y)=>string.Compare(x.name, y.name, StringComparison.Ordinal));
            itemList.ForEach(x =>
            {
                Add(x);
                x.DrawView();
            });
        }
        
        #endregion
        
        #region 公开

        public bool IsGraphExist(FlowGraphBase graph)
        {
            if (this.m_GraphsList.Contains(graph) || this.m_GraphsList.Exists(x=>x.name == graph.name))
            {
                return true;
            }

            return false;
        }
        
        public bool IsGraphExist(string graphName)
        {
            if (this.m_GraphsList.Exists(x=>x.name == graphName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 重绘面板
        /// </summary>
        public void Repaint()
        {
            SetScrollOffset();
            DrawView();
        }
        
        /// <summary>
        /// 滑动条定位
        /// </summary>
        public void SetScrollOffset()
        {
            m_Offset = this.m_Scroll.scrollOffset;
        }

        public void SetSelected(GraphItemView view)
        {
            if (this.m_CurSelected == view && this.m_SelectedList.Count == 1) return;
            
            if (this.m_CurSelected != null  && !m_Ctrl && !m_Shift)
            {
                ClearSelected();
            }

            if (view == null) return;
            
            if (this.m_CurSelected != null && m_Ctrl && !m_Shift)
            {
                AddSelected(view);
            }
            else if (this.m_CurSelected != null && !m_Ctrl && m_Shift)
            {
                var curIndex = IndexOf(this.m_CurSelected);
                var newIndex = IndexOf(view);
                if (newIndex > curIndex)
                {
                    for (int i = curIndex + 1; i <= newIndex; i++)
                    {
                        if (this.contentContainer[i] is GraphItemView itemView)
                        {
                            AddSelected(itemView);
                        }
                    }
                }
                else
                {
                    for (int i = newIndex; i <= curIndex; i++)
                    {
                        if (this.contentContainer[i] is GraphItemView itemView)
                        {
                            AddSelected(itemView);
                        }
                    }
                }
                
            }
            else
            {
                AddSelected(view);
            }
            
            this.m_CurSelected = view;
        }

        public void AddSelected(GraphItemView view)
        {
            if (!this.m_SelectedList.Contains(view))
            {
                view.SetSelect(true);
                this.m_SelectedList.Add(view);
            }
        }

        public void RemoveSelected(GraphItemView view)
        {
            if (this.m_SelectedList.Contains(view))
            {
                view.SetSelect(false);
                this.m_SelectedList.Remove(view);
            }
        }

        public void ClearSelected()
        {
            for (int i = this.m_SelectedList.Count - 1; i >= 0; i--)
            {
                var view = this.m_SelectedList[i];
                view.SetSelect(false);
                this.m_SelectedList.Remove(view);
            }
        }
        
        public void LocateSelect()
        {
            if (this.m_CurSelected == null) return;

            var folders = FlowUtils.GetParentFolders(this.m_CurSelected.m_Data.FlowPath);
            foreach (var folder in folders)
            {
                Cookie.SetPublic($"FlowEvent_Collapse_{folder}", 1);
            }

            m_SearchView.ClearText();
        }
        
        #endregion
        
        #region 菜单方法
        
        public void MenuPopulate(GraphItemView view, ContextualMenuPopulateEvent evt)
        {
            evt.menu.MenuItems().Clear();

            if (this.m_SelectedList.Count <= 1)
            {
                if (view.m_Data.IsFolder)
                {
                    evt.menu.AppendAction("导出当前文件夹", action=>
                    {
                        FolderExport(view);
                    });
                    
                    evt.menu.AppendAction("导出当前文件夹(包含子文件夹)", action=>
                    {
                        FolderExport(view, false);
                    });
                
                    evt.menu.AppendAction("新建事件", action=>
                    {
                        AddEvent(view);
                    });
                
                    evt.menu.AppendAction("新建文件夹", action=>
                    {
                        AddFolder(view);
                    });
                
                    evt.menu.AppendAction("新建文件夹(根目录)", action=>
                    {
                        AddFolder(view, true);
                    });
                    
                    evt.menu.AppendAction("复制名称", action =>
                    {
                        CopyTextToClipboard(view);
                    });

                    evt.menu.AppendAction("重命名", action=>
                    {
                        RenameFolder(view);
                    });
                
                    evt.menu.AppendAction("文件资源管理器中显示", action=>
                    {
                        ShowInExplorer(view);
                    });
                
                    evt.menu.AppendAction("删除文件夹", action=>
                    {
                        DeleteFolder(view);
                    });
                }
                else
                {
                    // 添加自定义的右键菜单项
                    evt.menu.AppendAction("导出", action =>
                    {
                        Export(view);
                    });
                    
                    evt.menu.AppendAction("复制名称", action =>
                    {
                        CopyTextToClipboard(view);
                    });
                
                    evt.menu.AppendAction("重命名", action =>
                    {
                        RenameGraph(view);
                    });
                
                    evt.menu.AppendAction("文件资源管理器中显示", action=>
                    {
                        ShowInExplorer(view);
                    });
                
                    evt.menu.AppendAction("删除", action =>
                    {
                        DeleteGraph(view);
                    });
                }
            }
            else
            {
                evt.menu.AppendAction("批量导出", action =>
                {
                    BatchExport();
                });
                
                evt.menu.AppendAction("批量导出(包含子文件夹)", action =>
                {
                    BatchExport(false);
                });
                
                evt.menu.AppendAction("批量删除", action =>
                {
                    BatchDelete();
                });
            }
            
            evt.StopPropagation();
        }

        private void OnAddBtnClick(Blackboard blackboard)
        {
            CustomGameEventWindow.OpenWindow("创建新事件", "New GameEvent", (CreateName) =>
            {
                var graph = ScriptableObject.CreateInstance<FlowGraphBase>();
                var path = FlowSetting.GetFlowAssetPath(CreateName);
                if (!IsGraphExist(CreateName))
                {
                    AssetDatabase.CreateAsset(graph, path);
                    AssetDatabase.Refresh();
                    Repaint();
                    CustomGameEventWindow.CloseWindow();
                }
                else
                {
                    EditorUtility.DisplayDialog("提示", "该配置已存在!", "确定");
                }
            });
        }

        private void ShowInExplorer(GraphItemView view)
        {
            var path =  view.m_Data.DiskPath;
            path = path.Replace("/", "\\");
            if (view.m_Data.IsFolder)
            {
                Process.Start("explorer.exe", path);
            }
            else
            {
                Process.Start("explorer.exe", "/select," + path);
            }
        }
        
        // private void ShowConfigPath(GraphItemView view)
        // {
        //     string path = Application.dataPath + $"/{view.m_Data.Name}.json";
        //     path = path.Replace("/", "\\");
        //     Process.Start("explorer.exe", "/select," + path);
        // }
        
        private void CopyTextToClipboard(GraphItemView view)
        {
            GUIUtility.systemCopyBuffer = view.m_Data.Name;
        }
        
        private void AddEvent(GraphItemView view)
        {
            CustomGameEventWindow.OpenWindow("创建新事件", $"New GameEvent {FlowGraphWindow.GraphBases.Count+1}", (CreateName) =>
            {
                var graph = ScriptableObject.CreateInstance<FlowGraphBase>();
                var path = Path.Combine(view.m_Data.Path, $"{CreateName}.asset");
                if (!IsGraphExist(CreateName))
                {
                    AssetDatabase.CreateAsset(graph, path);
                    AssetDatabase.Refresh();
                    CustomGameEventWindow.CloseWindow();
                    Repaint();
                }
                else
                {
                    EditorUtility.DisplayDialog("提示", "该配置已存在!", "确定");
                }
            });
        }

        private void AddFolder(GraphItemView view, bool root = false)
        {
            CustomGameEventWindow.OpenWindow("创建文件夹", "New Folder", (CreateName) =>
            {
                var path = root ? Path.Combine(Application.dataPath, "Editor/FlowGraphs") : view.m_Data.DiskPath;
                path = Path.Combine(path, $"{CreateName}");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    CustomGameEventWindow.CloseWindow();
                    AssetDatabase.Refresh();
                    Repaint();
                }
                else
                {
                    EditorUtility.DisplayDialog("提示", "该文件夹已存在!", "确定");
                }
            });
        }

        private void RenameFolder(GraphItemView view)
        {
            CustomGameEventWindow.OpenWindow("重命名文件夹", view.m_Data.Name, (NewName) =>
            {
                var path = view.m_Data.DiskPath;
                var newPath = path.Replace(view.m_Data.Name, NewName);
                if (!Directory.Exists(newPath))
                {
                    //移动新文件夹下
                    Directory.Move(path, newPath);
                    AssetDatabase.Refresh();
                    //删除原来的文件夹、
                    if (Directory.Exists(path))
                    {
                        //删除文件夹的meta文件
                        if (File.Exists($"{path}.meta"))
                        {
                            File.Delete($"{path}.meta");
                        }

                        Directory.Delete(path, true);
                    }
                    AssetDatabase.Refresh();
                    CustomGameEventWindow.CloseWindow();
                    Repaint();
                }
                else
                {
                    EditorUtility.DisplayDialog("提示", "该文件夹已存在!", "确定");
                }
            });
        }

        private void DeleteFolder(GraphItemView view, bool tip = true, bool refresh = true)
        {
            var path = view.m_Data.DiskPath;
            void execute()
            {
                if (Directory.Exists(path))
                {
                    //先删除文件夹的meta文件
                    if (File.Exists($"{path}.meta"))
                    {
                        File.Delete($"{path}.meta");
                    }
                    Directory.Delete(path, true);
                    CustomGameEventWindow.CloseWindow();
                    if (refresh) AssetDatabase.Refresh();
                    Repaint();
                }
                else
                {
                    EditorUtility.DisplayDialog("提示", "文件夹不存在!", "确定");
                }
            }
            
            if (tip)
            {
                if (EditorUtility.DisplayDialog("警告", $"确定删除{ view.m_Data.Name}?", "确定", "取消"))
                {
                    execute();
                }
            }
            else
            {
                execute();
            }
        }
        
        private void Export(GraphItemView view)
        {
            // if (FlowExportUtils.ExportGraph(view.m_Graph))
            // {
            //     EditorUtility.DisplayDialog("提示", $"导出{view.m_Graph.name}成功", "确定");
            // }
            // else
            // {
            //     EditorUtility.DisplayDialog("提示", $"导出{view.m_Graph.name}失败，请根据报错日志修改！", "确定");
            // }
        }

        private void FolderExport(GraphItemView view, bool top = true)
        {
            // var graphBases =FlowEditorUtils.LoadAllAssets<FlowGraphBase>(view.m_Data.DiskPath, top ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
            // bool success = true;
            // for (int i = 0; i < graphBases.Count; i++)
            // {
            //     var graph = graphBases[i];
            //     EditorUtility.DisplayProgressBar("批量导出",
            //         $"正在导出{graph.name}({i + 1}/{graphBases.Count})",
            //         (i + 1) / (float)graphBases.Count);
            //     if (!GameEventExportUtils.ExportGraphNonRefresh(graph))
            //     {
            //         success = false;
            //         EditorUtility.DisplayDialog("提示", $"导出{graph.name}失败，请根据报错日志修改！", "确定");
            //         break;
            //     }
            // }
            //
            // EditorUtility.ClearProgressBar();
            // AssetDatabase.Refresh();
            // if (success)
            // {
            //     EditorUtility.DisplayDialog("提示", $"批量导出成功, 数量: {graphBases.Count}", "确定");
            // }
        }
        
        private void RenameGraph(GraphItemView view)
        {
            CustomGameEventWindow.OpenWindow("重命名", view.m_Graph.name, (NewName) =>
            {
                var oldePath = view.m_Data.Path;
                if (IsGraphExist(NewName))
                {
                    EditorUtility.DisplayDialog("提示", "该配置已存在!", "确定");
                }
                else
                {
                    AssetDatabase.RenameAsset(oldePath, NewName);
                    AssetDatabase.Refresh();
                    Repaint();
                    CustomGameEventWindow.CloseWindow();
                }
            });
        }
        
        private void DeleteGraph(GraphItemView view, bool tip = true, bool refresh = true)
        {
            void execute()
            {
                bool isSame = view.m_Graph == this.m_Window.Graph;
                var path = AssetDatabase.GetAssetPath(view.m_Graph);
                AssetDatabase.DeleteAsset(path);

                if (refresh) AssetDatabase.Refresh();
                this.m_Window.InitializeGraph(isSame ? FlowGraphWindow.GraphBases[0] : this.m_Window.Graph);
                Repaint();
            }
            
            if (tip)
            {
                if (EditorUtility.DisplayDialog("警告", $"确定删除{view.m_Graph.name}?", "确定", "取消"))
                {
                    execute();
                }
            }
            else
            {
                execute();
            }
        }
        
        private void BatchExport(bool top = true)
        {
            // var fileList = m_SelectedList.FindAll(x => !x.m_Data.IsFolder);
            // var folderList = m_SelectedList.FindAll(x => x.m_Data.IsFolder);
            //
            // List<FlowGraphBase> exportList = new();
            // foreach (var item in fileList)
            // {
            //     exportList.Add(item.m_Graph);
            // }
            //
            // foreach (var folder in folderList)
            // {
            //     var graphList = FlowEditorUtils.LoadAllAssets<FlowGraphBase>(folder.m_Data.DiskPath,
            //         top ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
            //     foreach (var graph in graphList)
            //     {
            //         if (exportList.Contains(graph) || exportList.Exists(x=>x.name == graph.name)) continue;
            //        
            //         exportList.Add(graph);
            //     }
            // }
            //
            // bool success = true;
            // for (int i = 0; i < exportList.Count; i++)
            // {
            //     var graph = exportList[i];
            //     EditorUtility.DisplayProgressBar("批量导出",
            //         $"正在导出{graph.name}({i + 1}/{exportList.Count})",
            //         (i + 1) / (float)exportList.Count);
            //     if (!GameEventExportUtils.ExportGraphNonRefresh(graph))
            //     {
            //         success = false;
            //         EditorUtility.DisplayDialog("提示", $"导出{graph.name}失败，请根据报错日志修改！", "确定");
            //         break;
            //     }
            // }
            //
            // EditorUtility.ClearProgressBar();
            // AssetDatabase.Refresh();
            // if (success)
            // {
            //     EditorUtility.DisplayDialog("提示", $"批量导出成功, 数量: {fileList.Count}", "确定");
            // }

        }

        private void BatchDelete()
        {
            var deleteList = m_SelectedList.FindAll(x => !x.m_Data.IsFolder);
            if (EditorUtility.DisplayDialog("警告", $"确定删除所选文件?数量:{deleteList.Count}", "确定", "取消"))
            {
                for (int i = deleteList.Count - 1; i >= 0 ; i--)
                {
                    var view = deleteList[i];
                    RemoveSelected(view);
                    if (view.m_Data.IsFolder)
                    {
                        DeleteFolder(view);
                    }
                    else
                    {
                        DeleteGraph(view);
                    }
                }
                AssetDatabase.Refresh();
                Repaint();
            }
        }

        #endregion
        
        #region Editor

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            var rect = this.GetPosition();
            Cookie.SetPublic(WIDTH, rect.width);
            Cookie.SetPublic(HEIGHT, rect.height);
            SetPosition(new Rect(0, 23, rect.width, rect.height));
        }

        #endregion
        
    }
}