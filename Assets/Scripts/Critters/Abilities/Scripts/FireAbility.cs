using UnityEngine;

namespace AbilityProperties
{
    [System.Serializable]
    public class FireAbility : Ability
    {
        public override void Use()
        {
            Debug.Log($"Usada la habilidad {parameters.name} (Cooldown: {parameters.cooldown}s)");
            // Lógica de la habilidad acá
        }
    }
}