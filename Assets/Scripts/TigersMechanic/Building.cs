using UnityEngine;

namespace DefaultNamespace.TigersMechanic
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private BuildingType _type;
        [SerializeField] private BonusData _data;

        public BuildingType GetType()
        {
            return _type;
        }
        public BonusData GetData()
        {
            return _data;
        }
    }

    public enum BuildingType
    {
        Bank, 
        Butchery
    }
}