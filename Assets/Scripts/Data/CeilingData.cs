using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class CeilingData
         : IVisualData<CeilingData, CeilingRenderer>
    {
        //���ӵ���Ⱦ��
        CeilingRenderer ceilingRenderer;

        //��������
        int r_index;
        int c_index;

        //�Ƿ���ʾ
        public bool isShow = false;

        //�ؿ����ڿռ�λ��
        public Vector3 localPosition;

        public CeilingData(int row, int col)
        {
            this.r_index = row;
            this.c_index = col;
        }

        public void ConnectRenderer(CeilingRenderer renderer)
        {
            if (renderer == null)
            {
                Debug.LogError("Ceiling Data connect renderer failed. RD is null");
                return;
            }

            if (ceilingRenderer != null)
                DisconnectRenderer();

            ceilingRenderer = renderer;
            ceilingRenderer.OnConnect(this);
        }

        public void DisconnectRenderer()
        {
            if (ceilingRenderer != null)
            {
                ceilingRenderer.OnDisconnect();
                ceilingRenderer = null;
            }
        }

        public override string ToString()
        {
            return string.Format("[ceiling->({0},{1})]", r_index, c_index);
        }
    }
}

