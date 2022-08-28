using Enums;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityObject;
using ValueObject;

public class Npc : MonoBehaviour
{
    public Material material;
    public ColorType currentColorType;
    private ColorData _colorData;
    public void SetCollectableMatarial(Material material)
    {
        material.color = material.color;
    }
    public void Test() => Debug.Log("npc start move");
  //  private void GetColorData() => _colorData = Resources.Load<CD_ColorData>("Data/CD_ColorData").Colors[(int)manager.currentColorType];
}
