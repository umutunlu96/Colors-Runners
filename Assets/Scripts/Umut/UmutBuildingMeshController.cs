using System;
using Enums;
using Extentions;
using UnityEngine;

namespace Umut
{
    public class UmutBuildingMeshController : MonoBehaviour
    {
        #region Self Variables
        [SerializeField] private UmutBuildingManager manager;
        [SerializeField] private MeshRenderer mainMeshRenderer;
        [SerializeField] private MeshRenderer sideMeshRenderer;
        
        [SerializeField] private BuildingComplateState _mainBuildingComplate;
        [SerializeField] private BuildingComplateState _sideBuildingComplate;
        
        private Material _mainMaterial;
        private Material _sideMaterial;
        #endregion

        private void Awake()
        {
            _mainMaterial = mainMeshRenderer.GetComponent<MeshRenderer>().material;
            _sideMaterial = sideMeshRenderer.GetComponent<MeshRenderer>().material;
        }

        public void SetData(BuildingComplateState mainComplateState, BuildingComplateState sideComplateState)
        {
            _mainBuildingComplate = mainComplateState;
            _sideBuildingComplate = sideComplateState;
        }

        private void Start()
        {
            CheckData();
        }

        public void CheckData()
        {
            switch (_mainBuildingComplate)
            {
                case BuildingComplateState.Completed:
                    _mainMaterial.SetAlpha(1);
                    break;
                case BuildingComplateState.Uncompleted:
                    _mainMaterial.SetAlpha(0);
                    break;
            }

            switch (_sideBuildingComplate)
            {
                case BuildingComplateState.Completed:
                    _sideMaterial.SetAlpha(1);
                    break;
                case BuildingComplateState.Uncompleted:
                    _sideMaterial.SetAlpha(0);
                    break;
            }
        }
    }
}