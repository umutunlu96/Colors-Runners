using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityObject;
using UnityEngine;

public class ColorTest : MonoBehaviour
{
    [SerializeField] CD_ColorData allColors;
    [SerializeField] Material material;
    [SerializeField] private float speed;
    private void Awake()
    {
        allColors = Resources.Load<CD_ColorData>("Data/CD_ColorData");
        material = GetComponent<Renderer>().material;
        material.DOOffset(new Vector2(0, speed * Time.deltaTime), "_AlbedoMap", 0.2f).SetLoops(-1);
    }
}