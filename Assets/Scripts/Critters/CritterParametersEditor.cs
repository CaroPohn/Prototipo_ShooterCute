#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using AbilityProperties;

namespace CritterProperties
{

    [CustomEditor(typeof(CritterParameters))]
    public class CritterParametersEditor : Editor
    {
        private List<System.Type> abilityTypes;
        private int selectedAbilityIndex = 0;
        private string[] abilityTypeNames;

        private void OnEnable()
        {
            // Obtener todos los tipos que heredan de Ability
            abilityTypes = System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(Ability)))
                .ToList();

            // Crear un array de nombres para el dropdown
            abilityTypeNames = abilityTypes.Select(t => t.Name).ToArray();
        }

        public override void OnInspectorGUI()
        {
            // Dibujar las propiedades por defecto (name, gunParameters, abilityParameters)
            DrawDefaultInspector();

            CritterParameters critter = (CritterParameters)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Ability Configuration", EditorStyles.boldLabel);

            // Mostrar dropdown para seleccionar el tipo de habilidad
            selectedAbilityIndex = EditorGUILayout.Popup("Ability Type", selectedAbilityIndex, abilityTypeNames);

            // Botón para asignar la habilidad seleccionada
            if (GUILayout.Button("Add/Change Ability"))
            {
                if (selectedAbilityIndex >= 0 && selectedAbilityIndex < abilityTypes.Count)
                {
                    // Crear una instancia de la habilidad seleccionada
                    critter.ability = (Ability)System.Activator.CreateInstance(abilityTypes[selectedAbilityIndex]);
                    critter.ability.SetParameters(critter.abilityParameters);
                    Debug.Log($"Asignada habilidad: {abilityTypeNames[selectedAbilityIndex]}");
                }
            }

            // Mostrar propiedades específicas de la habilidad actual (si existe)
            if (critter.ability != null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Current Ability Settings", EditorStyles.boldLabel);
                DrawAbilityProperties(critter.ability);
            }
        }

        private void DrawAbilityProperties(Ability ability)
        {
            if (ability == null) return;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Ability Settings", EditorStyles.boldLabel);

            // Obtener todos los campos públicos de la habilidad
            System.Type abilityType = ability.GetType();
            var fields = abilityType.GetFields(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance
            );

            foreach (var field in fields)
            { 
                // Omitir campos que no queremos mostrar (ej: "parameters")
                if (field.Name == "parameters") continue;

                // Obtener el valor actual del campo
                object value = field.GetValue(ability);
                string fieldName = ObjectNames.NicifyVariableName(field.Name); // Formatea "dashSpeed" como "Dash Speed"

                // Dibujar el campo según su tipo
                if (field.FieldType == typeof(float))
                {
                    value = EditorGUILayout.FloatField(fieldName, (float)value);
                }
                else if (field.FieldType == typeof(int))
                {
                    value = EditorGUILayout.IntField(fieldName, (int)value);
                }
                else if (field.FieldType == typeof(string))
                {
                    value = EditorGUILayout.TextField(fieldName, (string)value);
                }
                // Añadir más tipos según sea necesario (bool, Vector3, etc.)

                // Guardar el valor modificado
                field.SetValue(ability, value);
            }
        }
    }
}
#endif