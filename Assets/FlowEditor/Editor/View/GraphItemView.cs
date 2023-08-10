using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FlowEditor.Editor
{
    public class GraphItemView : VisualElement
    {
        private FlowGraphWindow m_Window;
        private FlowGraphBase m_Graph;

        public FlowGraphBase Graph => this.m_Graph;
        
        public GraphItemView(FlowGraphWindow window, FlowGraphBase graph)
        {
            this.m_Window = window;
            this.m_Graph = graph;
            
            this.style.flexDirection = FlexDirection.Row;
            this.style.justifyContent = Justify.FlexStart;
            
            this.DrawView();
        }

        public void DrawView()
        {
            Label graphName = new Label();
            graphName.text = this.m_Graph.name;
            graphName.style.flexGrow = 1;
            if (this.m_Window.Graph == this.m_Graph)
            {
                graphName.style.color = Color.cyan;
            }
            Add(graphName);
            
            Button openBtn = new Button(OnOpenBtnClick);
            openBtn.text = "打开";
            openBtn.style.alignSelf = Align.FlexEnd;
            Add(openBtn);
            
            Button exportBtn = new Button(OnExportBtnClick);
            exportBtn.text = "导出";
            exportBtn.style.alignSelf = Align.FlexEnd;
            Add(exportBtn);
            
            Button renameBtn = new Button(OnRenameBtnClick);
            renameBtn.text = "重命名";
            renameBtn.style.alignSelf = Align.FlexEnd;
            Add(renameBtn);
            
            Button delBtn = new Button(OnDelClick);
            delBtn.text = "删除";
            delBtn.style.alignSelf = Align.FlexEnd;
            Add(delBtn);
        }

        private void OnOpenBtnClick()
        {
            if (this.m_Window.Graph != this.m_Graph)
            {
                this.m_Graph.OpenCount++;
                this.m_Window.GraphView.FileView.SetScrollOffset();
                this.m_Window.InitializeGraph(this.m_Graph);
            }
        }

        private void OnExportBtnClick()
        {
            // if (FlowExportUtils.ExportGraph(this.m_Graph))
            // {
            //     EditorUtility.DisplayDialog("提示", $"导出{this.m_Graph.name}成功", "确定");
            // }
            // else
            // {
            //     EditorUtility.DisplayDialog("提示", $"导出{this.m_Graph.name}失败，请根据报错日志修改！", "确定");
            // }
        }
        
        public void OnRenameBtnClick()
        {
            CustomGameEventWindow.OpenWindow("重命名", this.m_Graph.name, (NewName) =>
            {
                var oldePath = AssetDatabase.GetAssetPath(this.m_Graph);
                var path = $"Assets/Editor/FlowGraphs/{NewName}.asset";
                if (AssetDatabase.LoadAssetAtPath<FlowGraphBase>(path) != null)
                {
                    EditorUtility.DisplayDialog("提示", "该配置已存在!", "确定");
                }
                else
                {
                    AssetDatabase.RenameAsset(oldePath, NewName);
                    AssetDatabase.Refresh();
                    this.m_Window.GraphView.FileView.SetDirty();
                    CustomGameEventWindow.CloseWindow();
                }
               
            });
        }
        
        private void OnDelClick()
        {
            if (EditorUtility.DisplayDialog("警告", $"确定删除{this.m_Graph.name}?", "确定", "取消"))
            {
                bool isSame = this.m_Graph == this.m_Window.Graph;
                var path = AssetDatabase.GetAssetPath(this.m_Graph);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.Refresh();

                this.m_Window.InitializeGraph(isSame ? FlowGraphWindow.GraphBases[0] : this.m_Window.Graph);
                this.m_Window.GraphView.FileView.SetDirty();
            }
        }
    }
}