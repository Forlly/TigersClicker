using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.TigersMechanic
{
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "ScriptableObjects/UpgradeData")]
    public class UpgradeData : ScriptableObject
    {
        public PaymentCategory PaymentCategory;
        public List<int> Price = new List<int>();
        public int UpgradeIndex = 0;
    }
    public enum PaymentCategory
    {
        Coin, 
        Meat
    }
}