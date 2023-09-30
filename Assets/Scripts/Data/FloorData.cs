using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public enum FloorType
    {
        None,                            //NONE
        Wall_left,                       //Wall
        Wall_top,                        //Wall
        Wall_right,                      //Wall
        Wall_down,                       //Wall
        Wall_2_Outside_lt,               //Wall
        Wall_2_Outside_tr,               //Wall
        Wall_2_Outside_rd,               //Wall
        Wall_2_Outside_dl,               //Wall
        Wall_2_across_vertical,          //Wall
        Wall_2_across_horizontal,        //Wall
        Wall_3_Outside_left,             //Wall
        Wall_3_Outside_top,              //Wall
        Wall_3_Outside_right,            //Wall
        Wall_3_Outside_down,             //Wall
        //障碍物类型
        Obstacle,                        //BLOCK
        //出入口类型
        Entrance_Exit,                   //EX
        //道路类型
        Lane_Cross,                      //3
        Lane_Trident_left,               //4
        Lane_Trident_top,                //5
        Lane_Trident_right,              //6
        Lane_Trident_down,               //7
        Lane_Corner_lt,                  //8
        Lane_Corner_tr,                  //9
        Lane_Corner_rd,                  //10
        Lane_Corner_dl,                  //11
        Lane_Straight_vertical,          //12
        Lane_Straight_horizontal,        //13
        //车位类型
        Park_None,                       //14
        Park_Side_left,                  //15
        Park_Side_top,                   //16
        Park_Side_right,                 //17
        Park_Side_down,                  //18
        Park_Corner_lt,                  //19
        Park_Corner_tr,                  //20
        Park_Corner_rd,                  //21
        Park_Corner_dl,                  //22
        Park_Straight_vertical,          //23
        Park_Straight_horizontal,        //24
    }
    public class FloorData
        : IVisualData<FloorData,FloorRenderer>
    {
        //连接的渲染器
        FloorRenderer floorRenderer;

        //所处行列
        int r_index;
        int c_index;

        //地块所在空间位置
        public Vector3 localPosition;

        //地块类型
        public FloorType type;

        public FloorData(int row, int col)
        {
            this.type = FloorType.None;
            this.r_index = row;
            this.c_index = col;
        }

        public void ConnectRenderer(FloorRenderer renderer)
        {
            if (renderer == null)
            {
                Debug.LogError("Floor Data connect renderer failed. RD is null");
                return;
            }

            if (floorRenderer != null)
                DisconnectRenderer();

            floorRenderer = renderer;
            floorRenderer.OnConnect(this);
        }

        public void DisconnectRenderer()
        {
            if (floorRenderer != null)
            {
                floorRenderer.OnDisconnect();
                floorRenderer = null;
            }
        }

        public override string ToString()
        {
            return string.Format("[grid->({0},{1})]", r_index, c_index);
        }
    }
}

