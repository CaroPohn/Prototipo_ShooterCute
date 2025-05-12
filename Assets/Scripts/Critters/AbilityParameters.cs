using UnityEngine;

namespace AbilityProperties
{
    [CreateAssetMenu(fileName = "AbilityParameters", menuName = "Ability/New Ability")]
    public class AbilityParameters : ScriptableObject
    {
        public new string name;
        public float cooldown;
        public float duration;
    }

}