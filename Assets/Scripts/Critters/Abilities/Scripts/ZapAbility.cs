using UnityEngine;

namespace AbilityProperties
{
    [System.Serializable]
    public class ZapAbility : Ability
    {
        public override void Use()
        {
            Debug.Log($"Usada la habilidad {parameters.name} (Cooldown: {parameters.cooldown}s)");
            // L�gica de la habilidad ac�
        }
    }
}