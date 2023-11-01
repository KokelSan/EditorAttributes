using System;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

namespace EditorAttributes.Editor
{
    [CustomPropertyDrawer(typeof(DropdownAttribute))]
    public class DropdownDrawer : PropertyDrawerBase
    {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var dropdownAttribute = attribute as DropdownAttribute;

			var arrayProperty = property.serializedObject.FindProperty(dropdownAttribute.ArrayName);
			var memberInfo = GetValidMemberInfo(dropdownAttribute.ArrayName, property);
			var stringArray = GetArrayValues(arrayProperty, property.serializedObject, memberInfo);

			int selectedIndex = 0;

			for (int i = 0; i < stringArray.Length; i++)
			{
				if (stringArray[i] == GetPropertyValueAsString(property)) selectedIndex = i;
			}

			selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, stringArray);

			if (selectedIndex >= 0 && selectedIndex < stringArray.Length) SetProperyValueAsString(stringArray[selectedIndex], ref property);
		}

		public string[] GetArrayValues(SerializedProperty arrayProperty, SerializedObject serializedObject, MemberInfo memberInfo)
		{
			var stringList = new List<string>();

			try
			{
				var memberInfoType = GetMemberInfoType(memberInfo);

				if (memberInfoType.IsArray)
				{
					var memberInfoValue = GetMemberInfoValue(memberInfo, serializedObject.targetObject);

					if (memberInfoValue is Array array)
					{
						foreach (var item in array) stringList.Add(item.ToString());
					}

					return stringList.ToArray();
				}
			}
			catch (NullReferenceException)
			{
				if (arrayProperty != null && arrayProperty.isArray)
				{
					for (int i = 0; i < arrayProperty.arraySize; i++)
					{
						var arrayElementProperty = arrayProperty.GetArrayElementAtIndex(i);
						stringList.Add(GetPropertyValueAsString(arrayElementProperty));
					}

					return stringList.ToArray();
				}
			}

			EditorGUILayout.HelpBox("Could not find the array or the attached property is not valid", MessageType.Error);

			return new string[0];
		}
	}
}
