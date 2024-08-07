﻿using GraphProcessor;
using SeinoCat.FlowEditor.Runtime;
using UnityEngine;

namespace SeinoCat.FlowEditor.Editor
{
    public class FlowToolBarView : ToolbarView
    {
        private FlowGraphView m_GraphView;
        
        public FlowToolBarView(BaseGraphView graphView) : base(graphView)
        {
            this.m_GraphView = graphView as FlowGraphView;
            this.ShowOperation(Cookie.GetPublic(FlowSetting.OperationSwitch, true));
            this.ShowFileList(Cookie.GetPublic(FlowSetting.FlieListViewSwitch, true));
            this.ShowMiniMap(Cookie.GetPublic(FlowSetting.MiniMapSwitch, false));
        }

        protected override void AddButtons()
        {
            AddToggle(new GUIContent("配置列表"), Cookie.GetPublic(FlowSetting.OperationSwitch, true), ShowFileList);
            AddToggle(new GUIContent("导出面板"), Cookie.GetPublic(FlowSetting.FlieListViewSwitch, true), ShowOperation);
            AddToggle(new GUIContent("小地图"), Cookie.GetPublic(FlowSetting.MiniMapSwitch, true), ShowMiniMap);
            AddButton(new GUIContent("定位"), LocateFile);
            // AddButton(new GUIContent("计算Order"), ()=> {m_GraphView.Window.Graph.ComputeGraphOrder();});
            AddButton(new GUIContent("刷新"), Refresh);
            AddButton(new GUIContent("导出当前"), Export, false);
            AddButton(new GUIContent("一键导出"), OneKeyExport, false);
        }

        public void ShowOperation(bool show)
        {
            if (show)
            {
                Cookie.SetPublic(FlowSetting.OperationSwitch, true);
                this.m_GraphView.DrawOperationView();
            }
            else
            {
                Cookie.SetPublic(FlowSetting.OperationSwitch, false);
                this.m_GraphView.DelOperationView();
            }
        }
        
        public void ShowFileList(bool show)
        {
            if (show)
            {
                Cookie.SetPublic(FlowSetting.FlieListViewSwitch, true);
                this.m_GraphView.DrawFileListView();
            }
            else
            {
                Cookie.SetPublic(FlowSetting.FlieListViewSwitch, false);
                this.m_GraphView.DelFileListView();
            }
        }

        public void ShowMiniMap(bool show)
        {
            if (show)
            {
                Cookie.SetPublic(FlowSetting.MiniMapSwitch, true);
                this.m_GraphView.DrawMiniMapView();
            }
            else
            {
                Cookie.SetPublic(FlowSetting.MiniMapSwitch, false);
                this.m_GraphView.DelMiniMapView();
            }
        }
        
        public void LocateFile()
        {
            this.m_GraphView.FileView.LocateSelect();
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