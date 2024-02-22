using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.TigersMechanic
{
    public class TigersObjectPool : MonoBehaviour
    {
        [SerializeField] private int amountPool = 32;
        [SerializeField] private TigerControls _tigersPrefab;
        [SerializeField] private Transform _poolTransform;
        [SerializeField] private Transform _centerPoint;
    
        [SerializeField]private List<TigerControls> _poolObjects = new List<TigerControls>();
        private bool isFull = false;

        public void Init()
        {
            for (int i = 0; i < amountPool; i++)
            {
                TigerControls tmpObj = Instantiate(_tigersPrefab,_poolTransform);
                tmpObj.SetCenterPoint(_centerPoint);
                tmpObj.gameObject.SetActive(false);
                tmpObj.gameObject.SetActive(true);
                tmpObj.gameObject.SetActive(false);
                _poolObjects.Add(tmpObj);
            }
        }


        public TigerControls GetPooledObject()
        {
            for (int i = 0; i < amountPool; i++)
            {
               
                if (!_poolObjects[i].gameObject.activeInHierarchy)
                {
                    _poolObjects[i].gameObject.SetActive(true);
                    return _poolObjects[i];
                }

                isFull = true;
            }

            return isFull ? CreateNewObject() : null;
        }
        
        public  void TurnOfObject(TigerControls obj)
        {
            for (int i = 0; i < amountPool; i++)
            {
                if (obj == _poolObjects[i])
                {
                    _poolObjects[i].gameObject.SetActive(false);
                }
            
            }
        }

        private TigerControls CreateNewObject()
        {
            TigerControls tmpObj;
            tmpObj = Instantiate(_tigersPrefab, _poolTransform);
            tmpObj.SetCenterPoint(_centerPoint);
            tmpObj.gameObject.SetActive(true);
            _poolObjects.Add(tmpObj);
            return tmpObj;
        }
    }
}