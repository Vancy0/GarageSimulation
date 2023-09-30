using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace generation
{
    public class FloorRenderer
        : MonoBehaviour, IVisualRenderer<FloorData, FloorRenderer>
    {
        //连接的地块数据
        FloorData floorData;


        public void OnConnect(FloorData data)
        {
            floorData = data;
            if (floorData != null)
            {
                transform.name = floorData.ToString();
                RefreshRotation(data.type);
                UpdateLocalPosition();
                UpdateActiveState();
            }
        }

        private void UpdateActiveState()
        {
            if (floorData.type != FloorType.None)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void RefreshRotation(FloorType type)
        {
            switch (type)
            {
                //墙面旋转
                case FloorType.Wall_3_Outside_left:
                    transform.Rotate(0f, 90f, 0f);
                    break;

                case FloorType.Wall_3_Outside_top:
                    transform.Rotate(0f, 180f, 0f);
                    break;

                case FloorType.Wall_3_Outside_right:
                    transform.Rotate(0f, 270f, 0f);
                    break;

                case FloorType.Wall_3_Outside_down:
                    break;

                case FloorType.Wall_2_Outside_lt:
                    transform.Rotate(0f, 90f, 0f);
                    break;

                case FloorType.Wall_2_Outside_tr:
                    transform.Rotate(0f, 180f, 0f);
                    break;

                case FloorType.Wall_2_Outside_rd:
                    transform.Rotate(0f, 270f, 0f);
                    break;

                case FloorType.Wall_2_Outside_dl:
                    break;

                case FloorType.Wall_2_across_vertical:
                    transform.Rotate(0f, 90f, 0f);
                    break;

                case FloorType.Wall_2_across_horizontal:
                    break;

                case FloorType.Wall_left:
                    transform.Rotate(0f, 180f, 0f);
                    break;

                case FloorType.Wall_top:
                    transform.Rotate(0f, 270f, 0f);
                    break;

                case FloorType.Wall_right:
                    break;

                case FloorType.Wall_down:
                    transform.Rotate(0f, 90f, 0f);
                    break;

                //车道旋转
                case FloorType.Lane_Trident_left:
                    transform.Rotate(0f, 90f, 0f);
                    break;

                case FloorType.Lane_Trident_top:
                    transform.Rotate(0f, 180f, 0f);
                    break;

                case FloorType.Lane_Trident_right:
                    transform.Rotate(0f, 270f, 0f);
                    break;

                case FloorType.Lane_Trident_down:
                    break;

                case FloorType.Lane_Corner_lt:
                    transform.Rotate(0f, 90f, 0f);
                    break;

                case FloorType.Lane_Corner_tr:
                    transform.Rotate(0f, 180f, 0f);
                    break;

                case FloorType.Lane_Corner_rd:
                    transform.Rotate(0f, 270f, 0f);
                    break;

                case FloorType.Lane_Corner_dl:
                    break;

                case FloorType.Lane_Straight_vertical:
                    transform.Rotate(0f, 90f, 0f);
                    break;

                case FloorType.Lane_Straight_horizontal:
                    break;

                //车位旋转
                case FloorType.Park_Side_left:
                    transform.Rotate(0f, 90f, 0f);
                    break;

                case FloorType.Park_Side_top:
                    transform.Rotate(0f, 180f, 0f);
                    break;

                case FloorType.Park_Side_right:
                    transform.Rotate(0f, 270f, 0f);
                    break;

                case FloorType.Park_Side_down:
                    break;

                case FloorType.Park_Corner_lt:
                    transform.Rotate(0f, 90f, 0f);
                    break;

                case FloorType.Park_Corner_tr:
                    transform.Rotate(0f, 180f, 0f);
                    break;

                case FloorType.Park_Corner_rd:
                    transform.Rotate(0f, 270f, 0f);
                    break;

                case FloorType.Park_Corner_dl:
                    break;

                case FloorType.Park_Straight_vertical:
                    transform.Rotate(0f, 90f, 0f);
                    break;

                case FloorType.Park_Straight_horizontal:
                    break;
            }
        }

        private void UpdateLocalPosition()
        {
            if (floorData != null)
            {
                transform.position = floorData.localPosition;
                /*  if (floorData.type == FloorType.Park_Corner_lt || floorData.type == FloorType.Park_Corner_tr
                      || floorData.type == FloorType.Park_Corner_rd || floorData.type == FloorType.Park_Corner_dl)
                  {
                      floorData.localPosition.x += 9.0f;
                      floorData.localPosition.z += 9.0f;
                      transform.position = floorData.localPosition;
                  }*/
            }
        }

        public void OnDisconnect()
        {
            floorData = null;
            transform.SetUnused(false);
        }
    }
}

