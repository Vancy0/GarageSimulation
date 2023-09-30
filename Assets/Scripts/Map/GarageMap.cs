using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class GarageMap
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

        //类型矩阵
        public FloorType[,] type_matrix;

        //地图格子信息
        public FloorData[,] floorDatas;

        //车位格子
        List<FloorData> parkingGrids = new List<FloorData>();

        //道路格子
        List<FloorData> laneGrids = new List<FloorData>();

        //障碍物格子
        List<FloorData> blockGrids = new List<FloorData>();

        //战场中铺设格子
        public void Generate(MatInfo matInfo)
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

            //生成格子数组
            floorDatas = new FloorData[mapWidth, mapLength];

            //将每一个格子分好类FloorType
            type_matrix = ClassifyGrid();
            //生成格子
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapLength; j++)
                {
                    FloorData floorData = new FloorData(i, j);
                    //设定地块的位置，目前为默认
                    floorData.localPosition = new Vector3(
                        i * ConstVal.DEFALUT_WIDTH,
                        0,
                        j * ConstVal.DEFALUT_LENGTH
                    );
                    floorData.type = type_matrix[i, j];
                    floorDatas[i, j] = floorData;
                }
            }
            Debug.Log("Garage Map initialized!");
        }

        private FloorType[,] ClassifyGrid()
        {
            if (mapWidth <= 0 || mapLength <= 0)
            {
                return null;
            }

            if (s_matrix == null)
            {
                return null;
            }

            FloorType[,] type_mat = new FloorType[mapWidth, mapLength];
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapLength; j++)
                {
                    ClassifyByType(i, j, type_mat);
                }
            }
            return type_mat;
        }

        private void ClassifyByType(int row, int col, FloorType[,] type_mat)
        {
            switch (s_matrix[row, col])
            {
                case -2:        //墙面
                    ClassifyWallGrid(row, col, type_mat);
                    break;
                case -1:        //障碍物地块
                    ClassifyBlockGrid(row, col, type_mat);
                    break;
                case 0:         //车位地块
                    ClassifyParkingGrid(row, col, type_mat);
                    break;
                case 1:         //道路地块
                    ClassifyLaneGrid(row, col, type_mat);
                    break;
                case 2:         //出入口道路地块
                    ClassifyLaneGrid(row, col, type_mat);
                    break;
            }
        }
        private void ClassifyWallGrid(int row, int col, FloorType[,] type_mat)
        {
            int[] arroundGrids = CheckValue(row, col);
            if (arroundGrids[ConstVal.OBSTACLE_NUMBER_BIT] == 4)
            {
                type_mat[row, col] = FloorType.None;
                Debug.LogError("A wall can not be adjecent to 4 another walls or obstacles!");
            }
            if (arroundGrids[ConstVal.OBSTACLE_NUMBER_BIT] == 0)
            {
                Debug.LogError("A wall can not be adjecent to 0 wall or obstacle!");
            }
            //一面墙，周围有三个障碍物
            if (arroundGrids[ConstVal.OBSTACLE_NUMBER_BIT] == 3)
            {
                if (arroundGrids[0] > ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_down;
                }
                if (arroundGrids[1] > ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_left;
                }
                if (arroundGrids[2] > ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_top;
                }
                if (arroundGrids[3] > ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_right;
                }
            }
            //两面墙，周围有两个障碍物
            if (arroundGrids[ConstVal.OBSTACLE_NUMBER_BIT] == 2)
            {
                //如果不是拐角
                if (arroundGrids[0] > ConstVal.NOT_BLOCK_FALG_ && arroundGrids[2] > ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_2_across_horizontal;
                }
                if (arroundGrids[1] > ConstVal.NOT_BLOCK_FALG_ && arroundGrids[3] > ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_2_across_vertical;
                }
                //是拐角
                if (arroundGrids[0] > ConstVal.NOT_BLOCK_FALG_ && arroundGrids[1] > ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_2_Outside_dl;
                }
                if (arroundGrids[1] > ConstVal.NOT_BLOCK_FALG_ && arroundGrids[2] > ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_2_Outside_lt;

                }
                if (arroundGrids[2] > ConstVal.NOT_BLOCK_FALG_ && arroundGrids[3] > ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_2_Outside_tr;

                }
                if (arroundGrids[3] > ConstVal.NOT_BLOCK_FALG_ && arroundGrids[0] > ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_2_Outside_rd;

                }
            }
            //三面墙，周围只有一个障碍物
            if (arroundGrids[ConstVal.OBSTACLE_NUMBER_BIT] == 1)
            {
                if (arroundGrids[0] <= ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_3_Outside_down;
                }
                if (arroundGrids[1] <= ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_3_Outside_left;
                }
                if (arroundGrids[2] <= ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_3_Outside_top;
                }
                if (arroundGrids[3] <= ConstVal.NOT_BLOCK_FALG_)
                {
                    type_mat[row, col] = FloorType.Wall_3_Outside_right;
                }
            }

        }

        private void ClassifyBlockGrid(int row, int col, FloorType[,] type_mat)
        {
            int[] arroundGrids = CheckValue(row, col);
            if (arroundGrids[ConstVal.OBSTACLE_NUMBER_BIT] == 4)
            {
                type_mat[row, col] = FloorType.None;
            }
            else
            {
                type_mat[row, col] = FloorType.Obstacle;
            }
        }

        private void ClassifyParkingGrid(int row, int col, FloorType[,] type_mat)
        {
            int[] arroundGrids = CheckValue(row, col);
            if (arroundGrids[ConstVal.LANE_NUMBER_BIT] >= 3)
            {
                type_mat[row, col] = FloorType.Park_Straight_horizontal;
                if (arroundGrids[0] != ConstVal.LANE_ || arroundGrids[2] != ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Park_Straight_vertical;
                }
            }
            if (arroundGrids[ConstVal.LANE_NUMBER_BIT] == 2)
            {
                //如果不是拐角
                if (arroundGrids[0] == ConstVal.LANE_ && arroundGrids[2] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Park_Straight_horizontal;
                }
                if (arroundGrids[1] == ConstVal.LANE_ && arroundGrids[3] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Park_Straight_vertical;
                }
                //是拐角
                if (arroundGrids[0] == ConstVal.LANE_ && arroundGrids[1] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Park_Corner_dl;
                }
                if (arroundGrids[1] == ConstVal.LANE_ && arroundGrids[2] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Park_Corner_lt;

                }
                if (arroundGrids[2] == ConstVal.LANE_ && arroundGrids[3] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Park_Corner_tr;

                }
                if (arroundGrids[3] == ConstVal.LANE_ && arroundGrids[0] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Park_Corner_rd;

                }
            }
            if (arroundGrids[ConstVal.LANE_NUMBER_BIT] == 1)
            {
                if (arroundGrids[0] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Park_Side_down;
                }
                if (arroundGrids[1] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Park_Side_left;
                }
                if (arroundGrids[2] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Park_Side_top;
                }
                if (arroundGrids[3] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Park_Side_right;
                }
            }
            if (arroundGrids[ConstVal.LANE_NUMBER_BIT] == 0)
            {
                type_mat[row, col] = FloorType.Park_None;
            }
        }

        private void ClassifyLaneGrid(int row, int col, FloorType[,] type_mat)
        {
            int[] arroundGrids = CheckValue(row, col);
            if (arroundGrids[ConstVal.LANE_NUMBER_BIT] == 4)
            {
                type_mat[row, col] = FloorType.Lane_Cross;
            }
            if (arroundGrids[ConstVal.LANE_NUMBER_BIT] == 3)
            {
                if (arroundGrids[0] != ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Lane_Trident_down;
                }
                if (arroundGrids[1] != ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Lane_Trident_left;
                }
                if (arroundGrids[2] != ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Lane_Trident_top;
                }
                if (arroundGrids[3] != ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Lane_Trident_right;
                }
            }
            if (arroundGrids[ConstVal.LANE_NUMBER_BIT] == 2)
            {
                //如果不是拐角
                if (arroundGrids[0] == ConstVal.LANE_ && arroundGrids[2] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Lane_Straight_vertical;
                }
                if (arroundGrids[1] == ConstVal.LANE_ && arroundGrids[3] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Lane_Straight_horizontal;
                }
                //是拐角
                if (arroundGrids[0] == ConstVal.LANE_ && arroundGrids[1] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Lane_Corner_tr;
                }
                if (arroundGrids[1] == ConstVal.LANE_ && arroundGrids[2] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Lane_Corner_rd;

                }
                if (arroundGrids[2] == ConstVal.LANE_ && arroundGrids[3] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Lane_Corner_dl;

                }
                if (arroundGrids[3] == ConstVal.LANE_ && arroundGrids[0] == ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Lane_Corner_lt;

                }
            }
            if (arroundGrids[ConstVal.LANE_NUMBER_BIT] == 1)
            {
                type_mat[row, col] = FloorType.Lane_Straight_vertical;
                if (arroundGrids[0] != ConstVal.LANE_ && arroundGrids[2] != ConstVal.LANE_)
                {
                    type_mat[row, col] = FloorType.Lane_Straight_horizontal;
                }
            }
            if (arroundGrids[ConstVal.LANE_NUMBER_BIT] <= 0)
            {
                Debug.LogWarning(string.Format("There is a lane grid with no connection->width:{0},height:{1}",
                   row, col));
            }
        }

        private int[] CheckValue(int row, int col)
        {
            int[] width_offset = { 1, 0, -1, 0 };
            int[] length_offset = { 0, -1, 0, 1 };
            int dir_num = width_offset.Length;

            //bottom, left, top, right, number of parking space, number of lane, number of obstacles
            int[] result = { -1, -1, -1, -1, 0, 0, 0 };
            for (int i = 0; i < dir_num; i++)
            {
                //得到要检查的四个方向的位置
                int check_width = width_offset[i] + row;
                int check_length = length_offset[i] + col;
                bool isValidPosition = check_width >= 0 && check_width < mapWidth
                    && check_length >= 0 && check_length < mapLength;
                if (isValidPosition)
                {
                    result[i] = s_matrix[check_width, check_length];
                    if (result[i] == 0)
                    {
                        result[4]++;
                    }
                    if (result[i] == 1)
                    {
                        result[5]++;
                    }
                    if (result[i] < 0)
                    {
                        result[6]++;
                    }
                }
                else
                {
                    result[6]++;
                }

            }
            return result;
        }
    }
}

