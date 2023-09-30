using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public class Controller : MonoBehaviour
    {
        MatInfo matInfo;
        GarageData garageData;
        bool garageRendererReady = false;
        bool fileReadReady = false;
        void Start()
        {
            ReadFile.Instance.Init(OnFileReadReady);
            while (!fileReadReady)
            {
                //wait for read file
            }
            matInfo = new MatInfo();
            GarageRenderer.Instance.Init(OnGarageReady);
        }

        void Update()
        {
            if (!garageRendererReady) return;
            if (garageData == null)
            {
                CreateGarage();
            }
        }

        public void CreateGarage()
        {
            garageData = new GarageData(matInfo);
            garageData.Generate();
            garageData.ConnectRenderer(GarageRenderer.Instance);
        }

        private void OnGarageReady()
        {
            garageRendererReady = true;
            Debug.Log("Garage renderer ready.");
        }

        private void OnFileReadReady()
        {
            fileReadReady = true;
            Debug.Log("File Read ready.");
        }
    }
}

