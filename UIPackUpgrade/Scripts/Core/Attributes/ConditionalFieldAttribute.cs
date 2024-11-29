/*
 * Author: DevDaoSi
 * @2024
 */
using System;
using UnityEditor;
using UnityEngine;

namespace Konzit.Core.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public class ConditionalFieldAttribute : PropertyAttribute
    {
        public string ConditionFieldName { get; private set; }
        public bool ExpectedResult { get; private set; }

        public ConditionalFieldAttribute(string conditionFieldName, bool expectedResult)
        {
            ConditionFieldName = conditionFieldName;
            ExpectedResult = expectedResult;
        }
    }

    [CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
    public class CheckDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionalFieldAttribute conditionalFieldAttribute = (ConditionalFieldAttribute) attribute;
            Debug.Log("conditional field attribute: " + conditionalFieldAttribute);
            SerializedProperty conditionProperty = property.serializedObject.FindProperty(conditionalFieldAttribute.ConditionFieldName);

            if (conditionProperty != null && conditionalFieldAttribute.ExpectedResult == conditionProperty.boolValue)
            {
                EditorGUI.PropertyField(position, conditionProperty, label);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ConditionalFieldAttribute conditionalFieldAttribute = (ConditionalFieldAttribute)attribute;
            SerializedProperty conditionProperty = property.serializedObject.FindProperty(conditionalFieldAttribute.ConditionFieldName);

            if (conditionProperty != null && conditionProperty.boolValue == conditionalFieldAttribute.ExpectedResult)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }

            return -EditorGUIUtility.standardVerticalSpacing; 
        }
    }
}
