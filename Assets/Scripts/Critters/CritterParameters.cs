using UnityEngine;
using GunProperties;
using AbilityProperties;

namespace CritterProperties
{
    [CreateAssetMenu(fileName = "CritterParameters", menuName = "Critter/New Critter")]
    public class CritterParameters : ScriptableObject
    {
        public new string name;
        public GunParameters gunParameters;

        [SerializeReference] public Ability ability;
        public AbilityParameters abilityParameters;
    }

}