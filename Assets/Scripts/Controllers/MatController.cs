using Enums;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityObject;
using ValueObject;

namespace Controllers
{
    public class MatController : MonoBehaviour
    {
        #region SelfVariables

        #region Public Variables

        public ColorData ColorData;
        public ColorType currentColorType;

        #endregion

        #region Serialize Variavles


        #endregion
        #region Private Variavles

        private Material _material;
        private BoxCollider _boxCollider;
        #endregion

        #endregion


        private void Awake()
        {
            GetReferances();
        }

        private void GetReferances()
        {
            _material = GetComponent<MeshRenderer>().material;
            _boxCollider = GetComponent<BoxCollider>();
        }
        
        public void GetColorData(ColorType colorType) => ColorData = Resources.Load<CD_ColorData>("Data/CD_ColorData").Colors[(int)colorType];

        public void SetColorData(ColorType colorType)
        {
            currentColorType = colorType;
            _material.color = ColorData.Color;
        }

        public void DisableBoxCollider()
        {
            _boxCollider.enabled = false;
        }
        
        public void CloseMat()
        {
            transform.DOScaleZ(.25f, .5f).OnComplete(() => { transform.DOScaleX(0, .5f).OnComplete(() =>
            { transform.DOScaleZ(0, .1f);});});
        }
    }
}