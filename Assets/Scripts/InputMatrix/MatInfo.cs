using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class MatInfo
    {
        private ReadFile readFile;
        //�ṹ���������
        public int width = 0;
        //�ṹ���������
        public int length = 0;
        //�ṹ����, ��СΪ width * length
        public int[,] s_mat;
        //�о��󣬴�СΪ 1 * length
        public float[] r_mat;
        //�о��󣬴�СΪ width * 1
        public float[] c_mat;

        public MatInfo()
        {
            readFile = ReadFile.Instance;
            InitBasicInfo();
        }

        void InitBasicInfo()
        {
            this.width = readFile.Width;
            this.length = readFile.Length;
            this.s_mat = readFile.S_mat;
            this.r_mat = readFile.R_mat;
            this.c_mat = readFile.C_mat;
        }

    }

}
