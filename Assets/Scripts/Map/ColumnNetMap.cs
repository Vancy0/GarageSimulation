using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class ColumnNetMap
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
        public ColumnData[,] columnDatas;

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

            //�������鳤��
            int netWidth = matInfo.width+1;
            int netLength = matInfo.length+1;

            //������������
            columnDatas = new ColumnData[netWidth, netLength];

            //���;���
            FloorType[,] type_matrix = garageMap.type_matrix;

            //���ɸ���
            for (int i = 0; i < netWidth; i++)
            {
                for (int j = 0; j < netLength; j++)
                {
                    ColumnData columnData = new ColumnData(i, j);
                    //�趨�ؿ��λ�ã�ĿǰΪĬ��
                    columnData.localPosition = new Vector3(
                        i * ConstVal.DEFALUT_WIDTH - ConstVal.DEFALUT_COLUMN_OFFSET,
                        0,
                        j * ConstVal.DEFALUT_LENGTH - ConstVal.DEFALUT_COLUMN_OFFSET
                    );
                    if (i != mapWidth && j != mapLength)
                    {
                        columnData.isShow = type_matrix[i, j] >= FloorType.Obstacle;
                    }
                    columnDatas[i, j] = columnData;
                }
            }
            Debug.Log("Column Net Map initialized!");
        }
    }

}
