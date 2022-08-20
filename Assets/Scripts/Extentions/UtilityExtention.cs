using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Extentions
{
    public static class UtilityExtention
    {
        public static void SetAlpha(this Material material, float alpha)
        {
            Color _color = material.color;
            _color.a = alpha;
            material.color = _color;
        }
        public static void SetRainBowShader(this Material material, Material _material)
        {
            material = _material;
            material.DOOffset(new Vector2(0, 100f * Time.deltaTime), "_AlbedoMap", 0.2f).SetLoops(-1);
        }
    }
}
