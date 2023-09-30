using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class ColumnData
         : IVisualData<ColumnData,ColumnRenderer>
    {
        //���ӵ���Ⱦ��
        ColumnRenderer columnRenderer;

        //��������
        int r_index;
        int c_index;

        //�Ƿ���ʾ
        public bool isShow = false;

        //�ؿ����ڿռ�λ��
        public Vector3 localPosition;

        public ColumnData(int row, int col)
        {
            this.r_index = row;
            this.c_index = col;
        }

        public void ConnectRenderer(ColumnRenderer renderer)
        {
            if (renderer == null)
            {
                Debug.LogError("Column Data connect renderer failed. RD is null");
                return;
            }

            if (columnRenderer != null)
                DisconnectRenderer();

            columnRenderer = renderer;
            columnRenderer.OnConnect(this);
        }

        public void DisconnectRenderer()
        {
            if (columnRenderer != null)
            {
               columnRenderer.OnDisconnect();
               columnRenderer = null;
            }
        }

        public override string ToString()
        {
            return string.Format("[column->({0},{1})]", r_index, c_index);
        }
    }

}
