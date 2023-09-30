using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class MatInfo
    {
        private ReadFile readFile;
        //结构矩阵的行数
        public int width = 0;
        //结构矩阵的列数
        public int length = 0;
        //结构矩阵, 大小为 width * length
        public int[,] s_mat;
        //行矩阵，大小为 1 * length
        public float[] r_mat;
        //列矩阵，大小为 width * 1
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
