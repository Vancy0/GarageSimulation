using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class CeilingRenderer
       : MonoBehaviour, IVisualRenderer<CeilingData, CeilingRenderer>
    {
        //连接的地块数据
        CeilingData ceilingData;


        public void OnConnect(CeilingData data)
        {
            ceilingData = data;
            if (ceilingData != null)
            {
                transform.name = ceilingData.ToString();
                UpdateLocalPosition();
                UpdateActiveState();

            }
        }

        private void UpdateActiveState()
        {
            gameObject.SetActive(ceilingData.isShow);
        }

        private void UpdateLocalPosition()
        {
            if (ceilingData != null)
            {
                transform.position = ceilingData.localPosition;
            }
        }

        public void OnDisconnect()
        {
            ceilingData = null;
            transform.SetUnused(false);
        }
    }
}

