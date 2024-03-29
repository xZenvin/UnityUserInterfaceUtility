using UnityEditor;
using UnityEngine;
using Zenvin.UI.Layout;

namespace Zenvin.UI {
	[CustomPropertyDrawer (typeof (ColumnDefinition))]
	[CustomPropertyDrawer (typeof (RowDefinition))]
	public class ColumnOrRowDefinitionDrawer : PropertyDrawer {

		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
			position = EditorGUI.PrefixLabel (position, label);
			position.width *= 0.5f;

			float height = position.height - EditorGUIUtility.singleLineHeight;
			position.height = EditorGUIUtility.singleLineHeight;
			position.y += height * 0.5f;

			var valueProp = property.FindPropertyRelative ("width") ?? property.FindPropertyRelative ("height");
			var unitProp = property.FindPropertyRelative ("unit");

			FloatField (position, valueProp);
			position.x += position.width + EditorGUIUtility.standardVerticalSpacing;
			position.width -= EditorGUIUtility.standardVerticalSpacing;
			EditorGUI.PropertyField (position, unitProp, GUIContent.none);
		}

		private static void FloatField (Rect rect, SerializedProperty property) {
			if (property == null || property.propertyType != SerializedPropertyType.Float) {
				return;
			}
			float value = EditorGUI.FloatField (rect, property.floatValue);
			property.floatValue = value;
		}

	}
}