using System;
using System.Collections;
using Managers;
using MK.Toon;
using DG.Tweening;
using Enums;
using UnityObject;
using ValueObject;
using UnityEngine;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region SelfVariables
        #region Serialize Variavles

        [SerializeField] CollectableManager manager;

        #endregion
        
        #region Private Variavles

        private ColorData _colorData;
        private Material _material;

        #endregion
        #endregion

        private void Awake()
        {
            _material = GetComponent<SkinnedMeshRenderer>().material;
            GetColorData();
        }

        private void Start()
        {
            _material.color = _colorData.Color;
        }

        public void ChangeMaterialColor()
        {
            GetColorData(); //Hatali kullanim sekli datayi basta alacagiz.
            _material.color = _colorData.Color;
        }

        private void GetColorData() => _colorData = Resources.Load<CD_ColorData>("Data/CD_ColorData").Colors[(int)manager.CurrentColorType];

        public void SetCollectableMaterial(Material material)
        {
            _material.color = material.color;
        }

        public void ActivateOutlineTransition(OutlineType type)
        {
            switch (type)
            {
                case OutlineType.NonOutline:
                    _material.DOFloat(0, "_OutlineSize", 1f);
                    break;
                case OutlineType.Outline:
                    _material.DOFloat(100, "_OutlineSize", 1f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}