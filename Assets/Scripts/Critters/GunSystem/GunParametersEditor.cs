using UnityEditor;
using UnityEngine;

namespace GunProperties
{
    [CustomEditor(typeof(GunParameters))]
    public class GunParametersEditor : Editor
    {
        private SerializedProperty gunTypeProp;
        private SerializedProperty nameProp;
        private SerializedProperty shotPrefabProp;
        private SerializedProperty fireRateProp;
        private SerializedProperty reloadTimeProp;
        private SerializedProperty bulletsPerReloadProp;
        private SerializedProperty mayInstantiateBulletProp;

        // Laser properties
        private SerializedProperty maxEnergyChargeProp;
        private SerializedProperty energyDrainRateProp;

        // Traditional properties
        private SerializedProperty baseDamageProp;
        private SerializedProperty projectileCountProp;
        private SerializedProperty dispersionAngleProp;
        private SerializedProperty dispersionDistanceProp;
        private SerializedProperty magazineProp;
        private SerializedProperty bulletDropProp;
        private SerializedProperty shotTypeProp;

        // Charged properties
        private SerializedProperty minChargingTimeProp;
        private SerializedProperty maxChargingTimeProp;
        private SerializedProperty minDamageProp;
        private SerializedProperty maxDamageProp;

        // Description
        private SerializedProperty weaponDescriptionProp;

        private void OnEnable()
        {
            gunTypeProp = serializedObject.FindProperty("gunType");
            nameProp = serializedObject.FindProperty("name");
            shotPrefabProp = serializedObject.FindProperty("shotPrefab");
            fireRateProp = serializedObject.FindProperty("fireRate");
            reloadTimeProp = serializedObject.FindProperty("reloadTime");
            bulletsPerReloadProp = serializedObject.FindProperty("bulletsPerReload");
            mayInstantiateBulletProp = serializedObject.FindProperty("mayInstantiateBullet");

            maxEnergyChargeProp = serializedObject.FindProperty("maxEnergyCharge");
            energyDrainRateProp = serializedObject.FindProperty("energyDrainRate");

            baseDamageProp = serializedObject.FindProperty("baseDamage");
            projectileCountProp = serializedObject.FindProperty("projectileCount");
            dispersionAngleProp = serializedObject.FindProperty("dispersionAngle");
            dispersionDistanceProp = serializedObject.FindProperty("dispersionDistance");
            magazineProp = serializedObject.FindProperty("magazine");
            bulletDropProp = serializedObject.FindProperty("bulletDrop");
            shotTypeProp = serializedObject.FindProperty("shotType");

            minChargingTimeProp = serializedObject.FindProperty("minChargingTime");
            maxChargingTimeProp = serializedObject.FindProperty("maxChargingTime");
            minDamageProp = serializedObject.FindProperty("minDamage");
            maxDamageProp = serializedObject.FindProperty("maxDamage");

            weaponDescriptionProp = serializedObject.FindProperty("_weaponDescription");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GunParameters gunParams = (GunParameters)target;
            GunType gunType = (GunType)gunTypeProp.enumValueIndex;

            EditorGUILayout.PropertyField(gunTypeProp);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(nameProp);
            EditorGUILayout.PropertyField(shotPrefabProp);

            // Solo mostrar estas propiedades si NO es un arma cargada
            if (gunType != GunType.Charged)
            {
                EditorGUILayout.PropertyField(fireRateProp);
                EditorGUILayout.PropertyField(reloadTimeProp);
                EditorGUILayout.PropertyField(bulletsPerReloadProp);
            }

            EditorGUILayout.PropertyField(mayInstantiateBulletProp);

            EditorGUILayout.Space();

            switch (gunType)
            {
                case GunType.Traditional:
                    DrawTraditionalProperties();
                    break;

                case GunType.Laser:
                    DrawLaserProperties();
                    break;

                case GunType.Charged:
                    DrawChargedProperties();
                    break;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Description", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(weaponDescriptionProp.stringValue, MessageType.None);

            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
            {
                UpdateWeaponDescription(gunParams, gunType);
            }
        }

        private void DrawTraditionalProperties()
        {
            EditorGUILayout.LabelField("Traditional Properties", EditorStyles.boldLabel);
            EditorGUILayout.Space(-15);
            EditorGUILayout.PropertyField(baseDamageProp);
            EditorGUILayout.PropertyField(projectileCountProp);
            EditorGUILayout.PropertyField(dispersionAngleProp);
            EditorGUILayout.PropertyField(dispersionDistanceProp);
            EditorGUILayout.PropertyField(magazineProp);
            EditorGUILayout.PropertyField(bulletDropProp);
            EditorGUILayout.PropertyField(shotTypeProp);
        }

        private void DrawLaserProperties()
        {
            EditorGUILayout.LabelField("Laser Properties", EditorStyles.boldLabel);
            EditorGUILayout.Space(-15);
            EditorGUILayout.PropertyField(maxEnergyChargeProp);
            EditorGUILayout.PropertyField(energyDrainRateProp);
        }

        private void DrawChargedProperties()
        {
            EditorGUILayout.LabelField("Charged Shot Properties", EditorStyles.boldLabel);
            EditorGUILayout.Space(-15);
            EditorGUILayout.PropertyField(minChargingTimeProp);
            EditorGUILayout.PropertyField(maxChargingTimeProp);
            EditorGUILayout.PropertyField(minDamageProp);
            EditorGUILayout.PropertyField(maxDamageProp);
        }

        private void UpdateWeaponDescription(GunParameters gunParams, GunType gunType)
        {
            switch (gunType)
            {
                case GunType.Traditional:
                    gunParams._weaponDescription = $"Arma: {gunParams.name}\n" +
                                                $"Daño por Disparo: {gunParams.baseDamage}\n" +
                                                $"Tamaño de cargador: {gunParams.magazine}\n" +
                                                $"Daño total por cargador: {gunParams.baseDamage * gunParams.magazine}\n" +
                                                $"Cadencia: {(1 / gunParams.fireRate):0.###} segundos entre disparos\n" +
                                                $"Balas por disparo: {gunParams.projectileCount}";
                    break;

                case GunType.Laser:
                    gunParams._weaponDescription = $"Arma: {gunParams.name}\n" +
                                                 $"Tipo: Láser\n" +
                                                 $"Carga máxima: {gunParams.maxEnergyCharge}\n" +
                                                 $"Consumo: {gunParams.energyDrainRate}/segundo\n" +
                                                 $"Duración máxima: {gunParams.maxEnergyCharge / gunParams.energyDrainRate:0.##} segundos\n" +
                                                 $"Cadencia: {(1 / gunParams.fireRate):0.###} segundos entre disparos";
                    break;

                case GunType.Charged:
                    gunParams._weaponDescription = $"Arma: {gunParams.name}\n" +
                                                $"Tipo: Cargado\n" +
                                                $"Daño mínimo: {gunParams.minDamage}\n" +
                                                $"Daño máximo: {gunParams.maxDamage}\n" +
                                                $"Tiempo carga mínima: {gunParams.minChargingTime}s\n" +
                                                $"Tiempo carga máxima: {gunParams.maxChargingTime}s";
                    // Eliminado "Cadencia" de la descripción para armas cargadas
                    break;
            }
        }
    }
}