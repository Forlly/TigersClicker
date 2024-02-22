using DefaultNamespace.TigersMechanic;
using UnityEngine;

namespace DefaultNamespace.Events
{
    public struct OnGetBonusFromBuilding
    {
        public BonusData BonusData;
        public Transform BuildingTransform;

        public OnGetBonusFromBuilding(BonusData data, Transform transform)
        {
            BonusData = data;
            BuildingTransform = transform;
        }
    }
}