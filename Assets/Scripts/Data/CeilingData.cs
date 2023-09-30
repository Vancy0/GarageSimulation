using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class CeilingData
         : IVisualData<CeilingData, CeilingRenderer>
    {
        //连接的渲染器
        CeilingRenderer ceilingRenderer;

        //所处行列
        int r_index;
        int c_index;

        //是否显示
        public bool isShow = false;

        //地块所在空间位置
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

