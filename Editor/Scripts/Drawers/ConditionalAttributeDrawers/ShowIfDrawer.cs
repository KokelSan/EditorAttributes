using UnityEditor;
using UnityEngine.UIElements;
using EditorAttributes.Editor.Utility;

namespace EditorAttributes.Editor
{
	[CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfDrawer : PropertyDrawerBase
    {
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			var showAttribute = attribute as ShowIfAttribute;
			var conditionalProperty = ReflectionUtility.GetValidMemberInfo(showAttribute.ConditionName, property);

			var root = new VisualElement();
			var errorBox = new HelpBox();

			var propertyField = DrawProperty(property);

			UpdateVisualElement(() =>
			{
				if (GetConditionValue(conditionalProperty, showAttribute, property, errorBox))
				{
					root.Add(propertyField);
				}
				else
				{
					RemoveElement(root, propertyField);
				}

				DisplayErrorBox(root, errorBox);
			});

			root.Add(propertyField);

			return root;
		}
	}
}
