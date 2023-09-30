using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class GarageData
        : IVisualData<GarageData, GarageRenderer>
    {
        //连接的车库渲染器
        GarageRenderer garageRenderer;
        //输入的矩阵数据
        MatInfo matInfo;

        //车库地图
        public GarageMap garageMap;

        // 柱网地图
        public ColumnNetMap columnNetMap;

        // 天花板地图
        public CeilingMap ceilingMap;

        public GarageData(MatInfo matInfo)
        {
            this.matInfo = matInfo;
            if (matInfo == null) Debug.LogError("Garage Gernate Failed!!");
        }

        //todo:
        public void Generate()
        {
            // 生成车位规划图
            GenerateMap();
            // 生成对应的柱网图
            GenerateColumn(garageMap);
            // 生成对应的天花板图
            GenerateCeiling(garageMap);
        }

        public void GenerateCeiling(GarageMap garageMap)
        {
            ceilingMap = CeilingManager.Instance.CreateCeilingMap(matInfo, garageMap);
        }

        public void GenerateColumn(GarageMap garageMap)
        {
            columnNetMap = ColumnManager.Instance.CreateNetMap(matInfo, garageMap);
        }

        public void GenerateMap()
        {
            //根据输入结构创建对应的地图信息
            garageMap = MapManager.Instance.CreateMap(matInfo);
        }

        public void ConnectRenderer(GarageRenderer renderer)
        {
            if (renderer == null)
            {
                Debug.LogError("Garage Data connect renderer failed. RD is null");
                return;
            }

            if (garageRenderer != null)
                DisconnectRenderer();

            garageRenderer = renderer;
            garageRenderer.OnConnect(this);
        }

        public void DisconnectRenderer()
        {
            if (garageRenderer != null)
            {
                garageRenderer.OnDisconnect();
                garageRenderer = null;
            }
        }

    }
}

