using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class CeilingMap
    {
        //生成地库的id
        public int mapID;
        //根据矩阵特征生成的HashID
        public long mapHashID;

        //结构矩阵的行数
        public int mapWidth = -1;
        //结构矩阵的列数
        public int mapLength = -1;
        //结构矩阵, 大小为 width * length
        public int[,] s_matrix;
        //行矩阵，大小为 1 * length
        public float[] r_matrix;
        //列矩阵，大小为 width * 1
        public float[] c_matrix;

        //地图格子信息
        public CeilingData[,] ceilingDatas;

        //战场中铺设格子
        public void Generate(MatInfo matInfo, GarageMap garageMap)
        {

            if (matInfo.width <= 0 || matInfo.length <= 0)
                return;

            //记录地图宽高以及矩阵
            mapWidth = matInfo.width;
            mapLength = matInfo.length;
            s_matrix = matInfo.s_mat;
            //目前为默认没有用到
            r_matrix = matInfo.r_mat;
            c_matrix = matInfo.c_mat;


            //生成天花板数组
            ceilingDatas = new CeilingData[mapWidth, mapLength];

            //类型矩阵
            FloorType[,] type_matrix = garageMap.type_matrix;

            //生成格子
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapLength; j++)
                {
                    CeilingData ceilingData = new CeilingData(i, j);
                    //设定地块的位置，目前为默认
                    ceilingData.localPosition = new Vector3(
                        i * ConstVal.DEFALUT_WIDTH,
                        ConstVal.DEFALUT_HEIGHT,
                        j * ConstVal.DEFALUT_LENGTH
                    );
                    ceilingData.isShow = type_matrix[i, j] >= FloorType.Obstacle;
                    ceilingDatas[i, j] = ceilingData;
                }
            }
            Debug.Log("Ceiling Map initialized!");
        }
    }
}

