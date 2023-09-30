using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace generation
{
    public class GarageRenderer
        : MonoBehaviourSingleton<GarageRenderer>,
            IVisualRenderer<GarageData, GarageRenderer>
    {
        //连接的车库数据
        public GarageData garageData;

        //地块池，用来存生成出来的地块
        List<FloorRenderer> floorPool = new List<FloorRenderer>();

        //立柱池，用来存生成出来的地块
        List<ColumnRenderer> columnPool = new List<ColumnRenderer>();

        //天花板池，用来存生成出来的地块
        List<CeilingRenderer> ceilingPool = new List<CeilingRenderer>();

        //所有地块的父节点
        [SerializeField] public Transform floorRoot;
        //所有柱子的父节点
        [SerializeField] public Transform columnRoot;
        //所有天花板的父节点
        [SerializeField] public Transform ceilingRoot;
        //初始生成的天花板模版
        [SerializeField] public CeilingRenderer ceilingModel;
        //初始生成的立柱模版
        [SerializeField] public ColumnRenderer columnModel;
        //初始生成地块的模版
        [SerializeField] public FloorRenderer floorUnitModel;
        [SerializeField] public FloorRenderer Obstacle;
        [SerializeField] public FloorRenderer Wall;
        [SerializeField] public FloorRenderer Wall_2_Outside;
        [SerializeField] public FloorRenderer Wall_2_across;
        [SerializeField] public FloorRenderer Wall_3_Outside;
        [SerializeField] public FloorRenderer Park_None;
        [SerializeField] public FloorRenderer Park_Side;
        [SerializeField] public FloorRenderer Park_Corner;
        [SerializeField] public FloorRenderer Park_Straight;
        [SerializeField] public FloorRenderer Lane_Cross;
        [SerializeField] public FloorRenderer Lane_Trident;
        [SerializeField] public FloorRenderer Lane_Corner;
        [SerializeField] public FloorRenderer Lane_Straight;


        public void Init(System.Action initedCallback)
        {
            //创建一定数量的地块渲染器，留作后面使用
            InitGridUnitRenderer(10);
            InitColumnRenderer(10);
            InitCeilingRenderer(10);
            Debug.Log("Garage renderer inited.");

            //初始化完成，通知回调
            if (initedCallback != null)
            {
                initedCallback();
            }

        }


        // 初始化地库各项元素
        private void InitGridUnitRenderer(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                CreateGridUnitRenderer();
            }
        }

        private void InitColumnRenderer(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                CreateColumnRenderer();
            }
        }

        private void InitCeilingRenderer(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                CreateCeilingRenderer();
            }
        }

        // 天花板的创建和获取
        private CeilingRenderer CreateCeilingRenderer()
        {
            var clone = Instantiate<CeilingRenderer>(ceilingModel);
            clone.transform.SetParent(ceilingRoot);
            clone.transform.SetUnused(false);
            ceilingPool.Add(clone);
            return clone;
        }

        private CeilingRenderer GetUnusedCeilingRenderer()
        {
            for (int i = 0; i < 10; i++)
            {
                if (!ceilingPool[i].gameObject.activeSelf)
                    return ceilingPool[i];
            }
            return CreateCeilingRenderer();
        }

        //柱网的创建和获取
        private ColumnRenderer CreateColumnRenderer()
        {
            var clone = Instantiate<ColumnRenderer>(columnModel);
            clone.transform.SetParent(columnRoot);
            clone.transform.SetUnused(false);
            columnPool.Add(clone);
            return clone;
        }

        private ColumnRenderer GetUnusedColumnRenderer()
        {
            for (int i = 0; i < 10; i++)
            {
                if (!columnPool[i].gameObject.activeSelf)
                    return columnPool[i];
            }
            return CreateColumnRenderer();
        }

        // 地块的创建和获取
        private FloorRenderer CreateGridUnitRenderer()
        {
            var clone = Instantiate<FloorRenderer>(floorUnitModel);
            clone.transform.SetParent(floorRoot);
            clone.transform.SetUnused(false);
            floorPool.Add(clone);
            return clone;
        }

        private FloorRenderer GetUnusedGridUnitRenderer()
        {
            for (int i = 0; i < 10; i++)
            {
                if (!floorPool[i].gameObject.activeSelf)
                    return floorPool[i];
            }
            return CreateGridUnitRenderer();
        }

        private FloorRenderer CreateGridUnitRenderer(FloorType type)
        {
            var clone = CreateDefiniteGridType(type);
            clone.transform.SetParent(floorRoot);
            clone.transform.SetUnused(false);
            floorPool.Add(clone);
            return clone;
        }

        private FloorRenderer CreateDefiniteGridType(FloorType type)
        {
            FloorRenderer clone;
            switch (type)
            {
                case FloorType.Obstacle:
                    clone = Instantiate<FloorRenderer>(Obstacle);
                    return clone;
                //墙面
                case FloorType.Wall_left:
                    clone = Instantiate<FloorRenderer>(Wall);
                    return clone;
                case FloorType.Wall_top:
                    clone = Instantiate<FloorRenderer>(Wall);
                    return clone;
                case FloorType.Wall_right:
                    clone = Instantiate<FloorRenderer>(Wall);
                    return clone;
                case FloorType.Wall_down:
                    clone = Instantiate<FloorRenderer>(Wall);
                    return clone;
                case FloorType.Wall_2_Outside_lt:
                    clone = Instantiate<FloorRenderer>(Wall_2_Outside);
                    return clone;
                case FloorType.Wall_2_Outside_tr:
                    clone = Instantiate<FloorRenderer>(Wall_2_Outside);
                    return clone;
                case FloorType.Wall_2_Outside_rd:
                    clone = Instantiate<FloorRenderer>(Wall_2_Outside);
                    return clone;
                case FloorType.Wall_2_Outside_dl:
                    clone = Instantiate<FloorRenderer>(Wall_2_Outside);
                    return clone;
                case FloorType.Wall_2_across_vertical:
                    clone = Instantiate<FloorRenderer>(Wall_2_across);
                    return clone;
                case FloorType.Wall_2_across_horizontal:
                    clone = Instantiate<FloorRenderer>(Wall_2_across);
                    return clone;
                case FloorType.Wall_3_Outside_left:
                    clone = Instantiate<FloorRenderer>(Wall_3_Outside);
                    return clone;
                case FloorType.Wall_3_Outside_top:
                    clone = Instantiate<FloorRenderer>(Wall_3_Outside);
                    return clone;
                case FloorType.Wall_3_Outside_right:
                    clone = Instantiate<FloorRenderer>(Wall_3_Outside);
                    return clone;
                case FloorType.Wall_3_Outside_down:
                    clone = Instantiate<FloorRenderer>(Wall_3_Outside);
                    return clone;
                //道路
                case FloorType.Lane_Cross:
                    clone = Instantiate<FloorRenderer>(Lane_Cross);
                    return clone;
                case FloorType.Lane_Trident_left:
                    clone = Instantiate<FloorRenderer>(Lane_Trident);
                    return clone;
                case FloorType.Lane_Trident_top:
                    clone = Instantiate<FloorRenderer>(Lane_Trident);
                    return clone;
                case FloorType.Lane_Trident_right:
                    clone = Instantiate<FloorRenderer>(Lane_Trident);
                    return clone;
                case FloorType.Lane_Trident_down:
                    clone = Instantiate<FloorRenderer>(Lane_Trident);
                    return clone;
                case FloorType.Lane_Corner_lt:
                    clone = Instantiate<FloorRenderer>(Lane_Corner);
                    return clone;
                case FloorType.Lane_Corner_tr:
                    clone = Instantiate<FloorRenderer>(Lane_Corner);
                    return clone;
                case FloorType.Lane_Corner_rd:
                    clone = Instantiate<FloorRenderer>(Lane_Corner);
                    return clone;
                case FloorType.Lane_Corner_dl:
                    clone = Instantiate<FloorRenderer>(Lane_Corner);
                    return clone;
                case FloorType.Lane_Straight_vertical:
                    clone = Instantiate<FloorRenderer>(Lane_Straight);
                    return clone;
                case FloorType.Lane_Straight_horizontal:
                    clone = Instantiate<FloorRenderer>(Lane_Straight);
                    return clone;
                //停车位
                case FloorType.Park_None:
                    clone = Instantiate<FloorRenderer>(Park_None);
                    return clone;
                case FloorType.Park_Side_left:
                    clone = Instantiate<FloorRenderer>(Park_Side);
                    return clone;
                case FloorType.Park_Side_top:
                    clone = Instantiate<FloorRenderer>(Park_Side);
                    return clone;
                case FloorType.Park_Side_right:
                    clone = Instantiate<FloorRenderer>(Park_Side);
                    return clone;
                case FloorType.Park_Side_down:
                    clone = Instantiate<FloorRenderer>(Park_Side);
                    return clone;
                case FloorType.Park_Corner_lt:
                    clone = Instantiate<FloorRenderer>(Park_Corner);
                    return clone;
                case FloorType.Park_Corner_tr:
                    clone = Instantiate<FloorRenderer>(Park_Corner);
                    return clone;
                case FloorType.Park_Corner_rd:
                    clone = Instantiate<FloorRenderer>(Park_Corner);
                    return clone;
                case FloorType.Park_Corner_dl:
                    clone = Instantiate<FloorRenderer>(Park_Corner);
                    return clone;
                case FloorType.Park_Straight_vertical:
                    clone = Instantiate<FloorRenderer>(Park_Straight);
                    return clone;
                case FloorType.Park_Straight_horizontal:
                    clone = Instantiate<FloorRenderer>(Park_Straight);
                    return clone;
                default:
                    clone = Instantiate<FloorRenderer>(Obstacle);
                    return clone;
            }
        }

        private FloorRenderer GetUnusedGridUnitRenderer(FloorType type)
        {
            /*if (type == FloorType.Lane_Straight_horizontal || type == FloorType.Lane_Straight_vertical)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (!floorPool[i].gameObject.activeSelf)
                        return floorPool[i];
                }
            }*/
            return CreateGridUnitRenderer(type);
        }

        // 连接地库数据段
        public void OnConnect(GarageData data)
        {
            this.garageData = data;
            //加载地库各项元素
            RefreshMapGrids();
            RefreshColumnNet();
            // RefreshCeilings();
        }

        public void OnDisconnect()
        {
            garageData = null;
            transform.SetUnused(false);
        }

        // 数据处理完毕，准备加载地库天花板
        private void RefreshCeilings()
        {
            if (garageData == null)
            {
                Debug.LogError("Prepare ceiling failed. No garage data.");
                return;
            }
            foreach (var item in garageData.ceilingMap.ceilingDatas)
            {
                if (item != null)
                {
                    //创建(分配)一个用于显示的格子对象
                    CeilingRenderer ceilingRenderer = GetUnusedCeilingRenderer();

                    if (ceilingRenderer != null)
                        item.ConnectRenderer(ceilingRenderer);
                }
            }
        }

        // 数据处理完毕，准备加载地库柱网
        private void RefreshColumnNet()
        {
            if (garageData == null)
            {
                Debug.LogError("Prepare column net failed. No garage data.");
                return;
            }
            foreach (var item in garageData.columnNetMap.columnDatas)
            {
                if (item != null)
                {
                    //创建(分配)一个用于显示的格子对象
                    ColumnRenderer columnRenderer = GetUnusedColumnRenderer();

                    if (columnRenderer != null)
                        item.ConnectRenderer(columnRenderer);
                }
            }
        }

        //数据已经处理完毕，准备加载地库地图
        private void RefreshMapGrids()
        {
            if (garageData == null)
            {
                Debug.LogError("Prepare map failed. No garage data.");
                return;
            }
            foreach (var item in garageData.garageMap.floorDatas)
            {
                if (item != null)
                {
                    //创建(分配)一个用于显示的格子对象
                    FloorRenderer floorRenderer = GetUnusedGridUnitRenderer(item.type);

                    if (floorRenderer != null)
                        item.ConnectRenderer(floorRenderer);
                }
            }
        }
    }
}

