using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class CeilingMap
    {
        //���ɵؿ��id
        public int mapID;
        //���ݾ����������ɵ�HashID
        public long mapHashID;

        //�ṹ���������
        public int mapWidth = -1;
        //�ṹ���������
        public int mapLength = -1;
        //�ṹ����, ��СΪ width * length
        public int[,] s_matrix;
        //�о��󣬴�СΪ 1 * length
        public float[] r_matrix;
        //�о��󣬴�СΪ width * 1
        public float[] c_matrix;

        //��ͼ������Ϣ
        public CeilingData[,] ceilingDatas;

        //ս�����������
        public void Generate(MatInfo matInfo, GarageMap garageMap)
        {

            if (matInfo.width <= 0 || matInfo.length <= 0)
                return;

            //��¼��ͼ����Լ�����
            mapWidth = matInfo.width;
            mapLength = matInfo.length;
            s_matrix = matInfo.s_mat;
            //ĿǰΪĬ��û���õ�
            r_matrix = matInfo.r_mat;
            c_matrix = matInfo.c_mat;


            //�����컨������
            ceilingDatas = new CeilingData[mapWidth, mapLength];

            //���;���
            FloorType[,] type_matrix = garageMap.type_matrix;

            //���ɸ���
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapLength; j++)
                {
                    CeilingData ceilingData = new CeilingData(i, j);
                    //�趨�ؿ��λ�ã�ĿǰΪĬ��
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

