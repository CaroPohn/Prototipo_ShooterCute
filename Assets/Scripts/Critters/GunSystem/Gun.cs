using UnityEngine;
using System.Collections;
using System;

namespace GunProperties
{

    public class Gun : MonoBehaviour
    {
        private GunParameters gunParameters;

        public Action<BulletProperties.BulletStats> OnShoot;

        private Coroutine shootCoroutine;

        private void Awake()
        {
            
        }

        public void SetGunParameters(GunParameters param)
        {
            gunParameters = param;
        }
        void StartShooting()
        {
            if (shootCoroutine != null)
            {
                StopCoroutine(shootCoroutine);
            }
            shootCoroutine = StartCoroutine(ShootRutine());
        }
        void StopShooting()
        {
            if (shootCoroutine != null)
            {
                StopCoroutine(shootCoroutine);
                shootCoroutine = null;
            }
        }

        public void Reload()
        {

        }

        public void SwitchShotMode()
        {

        }

        public void Aim() { }

        private IEnumerator ShootRutine()
        {
            while (true)
            {
                BulletProperties.BulletStats bulletStats = new BulletProperties.BulletStats();
                bulletStats.damage = gunParameters.baseDamage / gunParameters.projectileCount;
                bulletStats.position = transform.position;
                bulletStats.direction = transform.rotation;
                bulletStats.mayRenderize = gunParameters.mayInstantiateBullet;
                OnShoot?.Invoke(bulletStats);

                Debug.Log("Disparo");

                yield return new WaitForSeconds(1f / gunParameters.fireRate);
            }
        }

    }
}