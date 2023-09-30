using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class CeilingManager : MonoBehaviourSingleton<CeilingManager>
    {
        public CeilingMap CreateCeilingMap(MatInfo matInfo, GarageMap garageMap)
        {
            CeilingMap mapData = new CeilingMap();
            //可以弄个池子，目前就设置为0，并且直接new一个
            int mapID = 0;

            //输入结构矩阵的行数
            int width = matInfo.width;
            //输入结构矩阵的列数
            int length = matInfo.length;

            if (matInfo != null)
            {
                mapData.mapID = mapID;
                mapData.Generate(matInfo, garageMap);
            }
            else
            {
                Debug.LogError(string.Format("Create ceiling failed->width:{0},height:{1}",
                    width, length));
            }
            return mapData;
        }
    }
}

