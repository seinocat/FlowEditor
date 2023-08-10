using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace FlowEditor.Editor
{
    public class OrderOptionView : VisualElement
    {
        private FlowGraphView m_View;
        private ToolbarMenu m_SortDrop;
        private Button m_OrderBtn;
        
        public OrderOptionView(FlowGraphView view)
        {
            this.m_View = view;
            this.style.flexDirection = FlexDirection.Row;
            this.style.justifyContent = Justify.FlexStart;
            this.DrawView();
        }

        private void DrawView()
        {
            this.m_SortDrop = new ToolbarMenu();
            this.m_SortDrop.text = this.m_View.SortType.GetSortTypeName();
            this.m_SortDrop.style.flexGrow = 1;
            this.m_SortDrop.style.width = 100f;
            this.m_SortDrop.style.height = 20f;
            this.m_SortDrop.style.marginRight = 20f;

            foreach (var type in Enum.GetValues(typeof(SortType)))
            {
                if (type is SortType sortType)
                {
                    this.m_SortDrop.menu.AppendAction(sortType.GetSortTypeName(), action => {this.SetSort(sortType);});
                }
                
            }
            this.Add(this.m_SortDrop);
            
            this.m_OrderBtn = new Button(SetOrder);
            this.m_OrderBtn.text = this.m_View.OrderPositive ? "↓" : "↑";
            this.m_OrderBtn.style.alignSelf = Align.FlexEnd;
            this.Add(this.m_OrderBtn);
        }


        private void SetSort(SortType sortType)
        {
            this.m_View.SetSortType(sortType);
            this.m_SortDrop.text = this.m_View.SortType.GetSortTypeName();
        }
        
        private void SetOrder()
        {
            this.m_View.SetOrderType(!this.m_View.OrderPositive);
            this.m_OrderBtn.text = this.m_View.OrderPositive ? "↓" : "↑";
        }
        
        
    }
}