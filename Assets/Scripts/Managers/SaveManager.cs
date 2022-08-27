using System.Collections.Generic;
using Commands;
using Data.ValueObject;
using Enums;
using UnityEngine;
using UnityObject;

namespace Assets.Scripts.Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        //private LoadGameCommand _loadGameCommand;
        //private SaveGameCommand _saveGameCommand;
        [SerializeField] private CD_CityScriptableObject cityData;
        [SerializeField] private List<CD_Structure> structureData;

        #endregion

        #endregion

        private void Awake()
        {

            //_loadGameCommand = new LoadGameCommand();
            // _saveGameCommand = new SaveGameCommand();

            //if there is no save file created
            GetDatas();
            InitizilizeSaveFile();
            foreach (var structure in structureData)
            {
                print(LoadGameCommand<CD_Structure>.OnLoadGameData(SaveStates.BuildingType,structure.StructureData));
            }
        }
       
        #region Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            //CoreGameSignals.Instance.onSaveGameData += _saveGameCommand.OnSaveGameData;
            //CoreGameSignals.Instance.onLoadGameData += _loadGameCommand.OnLoadGameData;
        }

        private void UnSubscribe()
        {
          // CoreGameSignals.Instance.onSaveGameData -= _saveGameCommand.OnSaveGameData;
           // CoreGameSignals.Instance.onLoadGameData -= _loadGameCommand.OnLoadGameData;
        }

        #endregion]

        private void GetDatas()
        {
            cityData = Resources.Load<CD_CityScriptableObject>("Data/Idle/City");
            foreach(CD_Structure cityScriptableObject in cityData.CityScriptableObject)
            {
                structureData.Add(cityScriptableObject);
            }
        }
        
        private void InitizilizeSaveFile()
        {
            foreach(var buildingScriptableObject in structureData)
            {
                ES3.Save(buildingScriptableObject.name, buildingScriptableObject);
            }
        }
        
        
    }
}
