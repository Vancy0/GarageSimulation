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
        //���ӵĳ�������
        public GarageData garageData;

        //�ؿ�أ����������ɳ����ĵؿ�
        List<FloorRenderer> floorPool = new List<FloorRenderer>();

        //�����أ����������ɳ����ĵؿ�
        List<ColumnRenderer> columnPool = new List<ColumnRenderer>();

        //�컨��أ����������ɳ����ĵؿ�
        List<CeilingRenderer> ceilingPool = new List<CeilingRenderer>();

        //���еؿ�ĸ��ڵ�
        [SerializeField] public Transform floorRoot;
        //�������ӵĸ��ڵ�
        [SerializeField] public Transform columnRoot;
        //�����컨��ĸ��ڵ�
        [SerializeField] public Transform ceilingRoot;
        //��ʼ���ɵ��컨��ģ��
        [SerializeField] public CeilingRenderer ceilingModel;
        //��ʼ���ɵ�����ģ��
        [SerializeField] public ColumnRenderer columnModel;
        //��ʼ���ɵؿ��ģ��
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
            //����һ�������ĵؿ���Ⱦ������������ʹ��
            InitGridUnitRenderer(10);
            InitColumnRenderer(10);
            InitCeilingRenderer(10);
            Debug.Log("Garage renderer inited.");

            //��ʼ����ɣ�֪ͨ�ص�
            if (initedCallback != null)
            {
                initedCallback();
            }

        }


        // ��ʼ���ؿ����Ԫ��
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

        // �컨��Ĵ����ͻ�ȡ
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

        //�����Ĵ����ͻ�ȡ
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

        // �ؿ�Ĵ����ͻ�ȡ
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
                //ǽ��
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
                //��·
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
                //ͣ��λ
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

        // ���ӵؿ����ݶ�
        public void OnConnect(GarageData data)
        {
            this.garageData = data;
            //���صؿ����Ԫ��
            RefreshMapGrids();
            RefreshColumnNet();
            // RefreshCeilings();
        }

        public void OnDisconnect()
        {
            garageData = null;
            transform.SetUnused(false);
        }

        // ���ݴ�����ϣ�׼�����صؿ��컨��
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
                    //����(����)һ��������ʾ�ĸ��Ӷ���
                    CeilingRenderer ceilingRenderer = GetUnusedCeilingRenderer();

                    if (ceilingRenderer != null)
                        item.ConnectRenderer(ceilingRenderer);
                }
            }
        }

        // ���ݴ�����ϣ�׼�����صؿ�����
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
                    //����(����)һ��������ʾ�ĸ��Ӷ���
                    ColumnRenderer columnRenderer = GetUnusedColumnRenderer();

                    if (columnRenderer != null)
                        item.ConnectRenderer(columnRenderer);
                }
            }
        }

        //�����Ѿ�������ϣ�׼�����صؿ��ͼ
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
                    //����(����)һ��������ʾ�ĸ��Ӷ���
                    FloorRenderer floorRenderer = GetUnusedGridUnitRenderer(item.type);

                    if (floorRenderer != null)
                        item.ConnectRenderer(floorRenderer);
                }
            }
        }
    }
}

