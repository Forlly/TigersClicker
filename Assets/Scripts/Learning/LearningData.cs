using UnityEngine;

namespace DefaultNamespace.UI
{
    [CreateAssetMenu(fileName = "LearningData", menuName = "ScriptableObjects/LearningData")]
    public class LearningData : ScriptableObject
    {
        public Sprite Hero;
        public string Text;
    }
}