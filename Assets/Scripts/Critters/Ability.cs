using System;
using UnityEngine;

namespace AbilityProperties
{
    public abstract class Ability
    {
        protected AbilityParameters parameters;

        public void SetParameters(AbilityParameters param)
        {
            parameters = param;
        }

        public abstract void Use();
    }
}