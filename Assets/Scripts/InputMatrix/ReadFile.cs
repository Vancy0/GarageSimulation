using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class ReadFile : MonoBehaviourSingleton<ReadFile>
    {
        //结构矩阵的行数
        int width = 0;
        //结构矩阵的列数
        int length = 0;
        //结构矩阵, 大小为 width * length
        int[,] s_mat;
        //行矩阵，大小为 1 * length
        float[] r_mat;
        //列矩阵，大小为 width * 1
        float[] c_mat;
        //原始结构矩阵文件
        [SerializeField] public TextAsset s_mat_txt;
        //原始行列矩阵文件
        [SerializeField] public TextAsset r_c_mat_txt;

        public int Width { get => width;}
        public int Length { get => length;}
        public int[,] S_mat { get => s_mat;}
        public float[] R_mat { get => r_mat;}
        public float[] C_mat { get => c_mat;}

        public void Init(System.Action initedCallback)
        {
            GetSize();
            if (InitArray())
            {
                LoadSMat();
                LoadRCMat();
                Debug.Log("File Load Success!");
            }
            else
            {
                Debug.LogError("File Load Fail!!!");
            }

            //初始化完成，通知回调
            if (initedCallback != null)
            {
                initedCallback();
            }
        }


        public void GetSize()
        {
            //获取载入的结构矩阵获取行列数
            if (this.s_mat_txt == null)
            {
                Debug.Log("S_Matrix is null, now use defalut setting");
                this.s_mat_txt = Resources.Load("input_matrixes/test") as TextAsset;
                //this.s_mat_txt = Resources.Load("output") as TextAsset;
            }
            if (this.r_c_mat_txt == null)
            {
                Debug.Log("R_C_Matrix is null, now use defalut setting");
                this.r_c_mat_txt = Resources.Load("input_matrixes/test_r_c_mat") as TextAsset;
                //this.r_c_mat_txt = Resources.Load("r_c_mat") as TextAsset;
            }
            string[] rows = s_mat_txt.text.Split('\n');
            this.width = rows.Length;
            string[] column = rows[0].Split(',');
            this.length = column.Length;
            Debug.Log("Get input Matrix Size!");
            Debug.Log(string.Format("Width is {0}, Length is {1}!", this.width, this.length));

        }

        public bool InitArray()
        {
            //初始化类成员变量
            if (this.Width != 0 && this.Length != 0)
            {
                this.s_mat = new int[this.Width, this.Length];
                this.r_mat = new float[this.Width];
                this.c_mat = new float[this.Length];
                return true;
            }
            return false;
        }

        public void LoadSMat()
        {
            //载入结构矩阵
            string[] str = this.s_mat_txt.text.Split('\n');
            for (int i = 0; i < str.Length; i++)
            {
                string[] line = str[i].Split(',');
                for (int j = 0; j < line.Length; j++)
                {
                    //Debug.Log(line[j]);
                    s_mat[i, j] = int.Parse(line[j]);
                }
            }

        }
        public void LoadRCMat()
        {
            string[] str = this.r_c_mat_txt.text.Split('\n');
            //载入行列矩阵
            string[] r_mat_txt = str[0].Split(',');
            for (int i = 0; i < r_mat_txt.Length; i++)
            {
                r_mat[i] = float.Parse(r_mat_txt[i]);
            }
            string[] c_mat_txt = str[1].Split(',');
            for (int i = 0; i < c_mat_txt.Length; i++)
            {
                c_mat[i] = float.Parse(c_mat_txt[i]);
            }
        }
    }

}
