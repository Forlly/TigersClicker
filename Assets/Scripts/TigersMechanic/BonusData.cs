using UnityEngine;

namespace DefaultNamespace.TigersMechanic
{
    [CreateAssetMenu(fileName = "BonusData", menuName = "ScriptableObjects/BonusData")]
    public class BonusData :ScriptableObject
    {
        public PaymentCategory PaymentCategory;
        public int RewardCount;
    }
}