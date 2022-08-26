using Signals;
using System.Collections;
using System.Collections.Generic;
using Umut;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        //private LoadGameCommand _loadGameCommand;
        //private SaveGameCommand _saveGameCommand;
        [SerializeField] private CD_CityScriptableObject _cityScriptableObject;
        [SerializeField] private List<StructureScriptableObject> _buildingScriptableObjects;

        #endregion

        #endregion

        private void Awake()
        {

            //_loadGameCommand = new LoadGameCommand();
            // _saveGameCommand = new SaveGameCommand();

            //if there is no save file created
            GetDatas();
            InitizilizeSaveFile();
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
            _cityScriptableObject = Resources.Load<CD_CityScriptableObject>("Data/City");
            foreach(StructureScriptableObject cityScriptableObject in _cityScriptableObject.CityScriptableObject)
            {
                _buildingScriptableObjects.Add(cityScriptableObject);
            }
        }
        
        private void InitizilizeSaveFile()
        {
            foreach(var buildingScriptableObject in _buildingScriptableObjects)
            {
                ES3.Save(buildingScriptableObject.MainName, buildingScriptableObject);
            }
        }

       
    }

}
