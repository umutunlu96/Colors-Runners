using Enums;
using System.Collections;
using UnityEngine;
using UnityObject;
using ValueObject;

namespace Controllers
{
    public class MatController : MonoBehaviour
    {
        #region SelfVariables

        #region Private Variavles

        private Material _material;
        public ColorData _colorData;

        #endregion

        #region Serialize Variavles


        #endregion

        #region Public Variables

        public ColorType currentColorType;

        #endregion

        #endregion

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            GetColorData();
            _material.color = _colorData.Color;
        }

        private void GetColorData() => _colorData = Resources.Load<CD_ColorData>("Data/CD_ColorData").Colors[(int)currentColorType];

    }
}