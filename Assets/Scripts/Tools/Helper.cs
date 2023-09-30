using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace generation
{
    public static class Helper
    {
        public static void Normalize(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }

        public static void Normalize(this RectTransform rectTransform)
        {
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = Vector3.one;
            rectTransform.offsetMax = Vector3.zero;
            rectTransform.offsetMin = Vector3.zero;

        }

        public static void SetUnused(this Transform transform, bool active)
        {
            transform.Normalize();
            transform.name = ConstVal.STR_unused;
            transform.gameObject.SetActive(active);
        }

    }
}

