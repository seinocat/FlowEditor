using GraphProcessor;
using FlowEditor.Runtime;
using UnityEngine;

namespace FlowEditor.Editor
{
    public class FlowToolBarView : ToolbarView
    {
        private FlowGraphView m_GraphView;
        private const string SHOW_OPERATION = "FlowEditor_ShowOperation";
        private const string SHOW_FILELIST = "FlowEditor_ShowFileList";
        private const string SHOW_MINIMAP = "FlowEditor_MiniMap";
        
        public FlowToolBarView(BaseGraphView graphView) : base(graphView)
        {
            this.m_GraphView = graphView as FlowGraphView;
            this.ShowOperation(Cookie.GetPublic(SHOW_OPERATION, true));
            this.ShowFileList(Cookie.GetPublic(SHOW_FILELIST, true));
            this.ShowMiniMap(Cookie.GetPublic(SHOW_MINIMAP, false));
        }

        protected override void AddButtons()
        {
            AddToggle(new GUIContent("导出面板"), Cookie.GetPublic(SHOW_OPERATION, true), ShowOperation);
            AddToggle(new GUIContent("配置列表"), Cookie.GetPublic(SHOW_FILELIST, true), ShowFileList);
            AddToggle(new GUIContent("小地图"), Cookie.GetPublic(SHOW_MINIMAP, true), ShowMiniMap);
            AddButton(new GUIContent("计算Order"), ()=> {m_GraphView.Window.Graph.ComputeGraphOrder();});
            AddButton(new GUIContent("刷新"), Refresh);
            AddButton(new GUIContent("导出当前"), Export, false);
            AddButton(new GUIContent("一键导出"), OneKeyExport, false);
        }

        public void ShowOperation(bool show)
        {
            if (show)
            {
                Cookie.SetPublic(SHOW_OPERATION, true);
                this.m_GraphView.DrawOperationView();
            }
            else
            {
                Cookie.SetPublic(SHOW_OPERATION, false);
                this.m_GraphView.DelOperationView();
            }
        }
        
        public void ShowFileList(bool show)
        {
            if (show)
            {
                Cookie.SetPublic(SHOW_FILELIST, true);
                this.m_GraphView.DrawFileListView();
            }
            else
            {
                Cookie.SetPublic(SHOW_FILELIST, false);
                this.m_GraphView.DelFileListView();
            }
        }

        public void ShowMiniMap(bool show)
        {
            if (show)
            {
                Cookie.SetPublic(SHOW_MINIMAP, true);
                this.m_GraphView.DrawMiniMapView();
            }
            else
            {
                Cookie.SetPublic(SHOW_MINIMAP, false);
                this.m_GraphView.DelMiniMapView();
            }
        }

        public void Refresh()
        {
            this.m_GraphView.FileView.Repaint();
        }

        public void Export()
        {
            // if (FlowExportUtils.ExportGraph(this.m_GraphView.Window.Graph))
            // {
            //     EditorUtility.DisplayDialog("提示", "导出客户端配置成功", "确定");
            // }
            // else
            // {
            //     EditorUtility.DisplayDialog("提示", "导出失败，请根据报错日志修改！", "确定");
            // }
        }

        public void OneKeyExport()
        {
            // if (FlowExportUtils.ExportAllGraph() && FlowExportUtils.ExportServer())
            // {
            //     EditorUtility.DisplayDialog("提示", "导出客户端和服务器配置成功", "确定");
            // }
            // else
            // {
            //     EditorUtility.DisplayDialog("提示", "导出失败，请根据报错日志修改！", "确定");
            // }
        }
        
    }
}