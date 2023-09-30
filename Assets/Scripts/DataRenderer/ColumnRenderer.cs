using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class ColumnRenderer 
        : MonoBehaviour, IVisualRenderer<ColumnData, ColumnRenderer>
    {
        //连接的地块数据
        ColumnData columnData;


        public void OnConnect(ColumnData data)
        {
            columnData = data;
            if (columnData != null)
            {
                transform.name = columnData.ToString();
                UpdateLocalPosition();
                UpdateActiveState();
            }
        }

        private void UpdateActiveState()
        {
            gameObject.SetActive(columnData.isShow);

        }

        private void UpdateLocalPosition()
        {
            if (columnData != null)
            {
                transform.position = columnData.localPosition;
            }
        }

        public void OnDisconnect()
        {
            columnData = null;
            transform.SetUnused(false);
        }
    }
}

