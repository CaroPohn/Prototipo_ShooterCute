using System;
using System.Collections.Generic;
using UnityEngine;

namespace GunProperties
{
    [Serializable]
    public enum ShotType
    {
        FullAuto,
        SemiAuto,
        Single
    }

    [Serializable]
    public enum GunType
    {
        Traditional,
        Laser,
        Charged
    }


    [CreateAssetMenu(fileName = "GunParameters", menuName = "Gun Parameters/New Gun")]
    public class GunParameters : ScriptableObject
    {
        [Header("General")]
        public new string name = "None";
        public float fireRate = 1.0f; //Balas por segundo 
        public float reloadTime = 1.0f; //Tiempo que tardas en recargar
        public int bulletsPerReload = 1; //A veces, no queres cargar todo el cargador, sino bala a bala.
        public bool mayInstantiateBullet = false; //Instancia balas o no
        public GameObject shotPrefab = null;
        public GunType gunType;

        [Header("Laser")]
        public float maxEnergyCharge = 100.0f; //Si es un lazer, tiene carga que debe ser descargada, y no balas.
        public float energyDrainRate = 15.0f; //Cantidad de energia usada por segundo.

        [Header("Traditional")]
        public int baseDamage = 1; //Cantidad de daño base por disparo (no por bala).
        public int projectileCount = 1; //Cantidad de balas disparadas por disparo
        public float dispersionAngle = 0.0f; //Angulo de dispersion a partir de cierta unidad de distancia
        public float dispersionDistance = 0.0f; //Distancia para el calculo de dispersion
        public int magazine = 7; //Cantidad de balas en el cargador.
        [Range(0, 100)] public float bulletDrop = 0.0f; //Caida de bala. Se le aplica una caida para un disparo con trayectoria curva como un lanzagranadas.
        public List<ShotType> shotType = new List<ShotType>();

        [Header("Charged Shot")]
        public float minChargingTime = 1.0f; //Tiempo que debe estar presionado para empezar a hacer daño
        public float maxChargingTime = 1.0f; //Tiempo que debe estar presionado para obtener el maximo daño
        public int minDamage = 1; //Daño que se hace al soltar en el minChargeTime
        public int maxDamage = 1; //Daño que se logra al soltar en el maxChargeTime (independientemente que se pase)

        [Header("Description")]
        [TextArea(3, 10)]
        public string _weaponDescription;

        private void OnValidate()
        {
            float totalDamage = (float)baseDamage * (float)magazine;
            float bulletDamage = (float)baseDamage / (float)magazine;

            // Actualizar el texto descriptivo
            _weaponDescription = $"Arma: {name}\n" +
                              $"Daño por Disparo: {baseDamage}\n" +
                              $"Tamaño de cargador: {magazine}\n" +
                              $"Daño total por bala: {bulletDamage}\n" +
                              $"Daño total por cargador: {totalDamage}\n" +
                              $"Cadencia: {(1 / fireRate):0.###} segundos entre disparos";

            if (gunType == GunType.Charged)
            {
                fireRate = 0f;
                reloadTime = 0f;
                bulletsPerReload = 0;
            }
        }
    }

}