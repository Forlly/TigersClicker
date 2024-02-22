using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using Project;
using UnityEngine;

namespace DefaultNamespace.TigersMechanic
{
    public class ResourceUISpawner : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> _spawnPoints;
        [SerializeField] private ResourceUIObjectPool _coinsObjectPool;
        [SerializeField] private ResourceUIObjectPool _meatObjectPool;
        [SerializeField] private RectTransform _canvasRect;
        [SerializeField] private Camera _camera;

        private void Awake()
        {
            _coinsObjectPool.Init();
            _meatObjectPool.Init();
            EventBus.Instance.AddListener<OnGetBonusFromBuilding>(SpawnResource);
        }

        private void SpawnResource(OnGetBonusFromBuilding e)
        {
            if (e.BonusData.PaymentCategory == PaymentCategory.Meat)
            {
                SpawnMeatUI(e.BonusData, e.BuildingTransform);
            }
            else
            {
                SpawnCoinsUI(e.BonusData, e.BuildingTransform);
            }
        }

        private void SpawnMeatUI(BonusData data, Transform transform)
        {
            ResourceUI newMeat = _meatObjectPool.GetPooledObject();
            RectTransform meatRect = newMeat.GetRectTransform();
            newMeat.SetText($"+{data.RewardCount.ToString()}");
            Vector3 screenPoint = _camera.WorldToScreenPoint(transform.position);
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, screenPoint, _camera, out localPoint))
            {
                meatRect.localPosition = localPoint;
            }

            StartCoroutine(MoveResourceUI(meatRect, newMeat, _meatObjectPool));
        }
        
        private void SpawnCoinsUI(BonusData data, Transform transform)
        {
            ResourceUI newCoin = _coinsObjectPool.GetPooledObject();
            RectTransform coinRect = newCoin.GetRectTransform();
            newCoin.SetText($"+{data.RewardCount.ToString()}");
            Vector3 screenPoint = _camera.WorldToScreenPoint(transform.position);
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, screenPoint, _camera, out localPoint))
            {
                coinRect.localPosition = localPoint;
            }
            StartCoroutine(MoveResourceUI(coinRect, newCoin, _coinsObjectPool));
        }
        private IEnumerator MoveResourceUI(RectTransform coinRect, ResourceUI resourceUI, ResourceUIObjectPool pool)
        {
            float duration = 2f; 
            Vector2 startPosition = coinRect.anchoredPosition;
            Vector2 endPosition = startPosition + new Vector2(0, 100);

            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsed / duration);
                coinRect.anchoredPosition = Vector2.Lerp(startPosition, endPosition, normalizedTime);
                yield return null;
            }
            
            pool.TurnOfObject(resourceUI);
        }
    }
}