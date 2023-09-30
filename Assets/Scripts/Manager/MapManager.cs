using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class MapManager : MonoBehaviourSingleton<MapManager>
    {
        public GarageMap CreateMap(MatInfo matInfo)
        {
            GarageMap mapData = new GarageMap();
            //����Ū�����ӣ�Ŀǰ������Ϊ0������ֱ��newһ��
            int mapID = 0;

            //����ṹ���������
            int width = matInfo.width;
            //����ṹ���������
            int length = matInfo.length;

            if (matInfo != null)
            {
                mapData.mapID = mapID;
                mapData.Generate(matInfo);
            }
            else
            {
                Debug.LogError(string.Format("Create map failed->width:{0},height:{1}",
                    width, length));
            }
            return mapData;
        }
    }
}

