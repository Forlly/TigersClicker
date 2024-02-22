using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.TigersMechanic
{
    public class ResourceUIObjectPool : MonoBehaviour
    {
        [SerializeField] private int amountPool = 32;
        [SerializeField] private ResourceUI _resourcePrefab;
        [SerializeField] private Transform _poolTransform;
    
        [SerializeField]private List<ResourceUI> _poolObjects = new List<ResourceUI>();
        private bool isFull = false;

        public void Init()
        {
            for (int i = 0; i < amountPool; i++)
            {
                ResourceUI tmpObj = Instantiate(_resourcePrefab,_poolTransform);
                tmpObj.gameObject.SetActive(false);
                _poolObjects.Add(tmpObj);
            }
        }


        public ResourceUI GetPooledObject()
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
        
        public  void TurnOfObject(ResourceUI obj)
        {
            for (int i = 0; i < amountPool; i++)
            {
                if (obj == _poolObjects[i])
                {
                    _poolObjects[i].gameObject.SetActive(false);
                }
            
            }
        }

        private ResourceUI CreateNewObject()
        {
            ResourceUI tmpObj;
            tmpObj = Instantiate(_resourcePrefab, _poolTransform);
            tmpObj.gameObject.SetActive(true);
            _poolObjects.Add(tmpObj);
            return tmpObj;
        }
    }
}