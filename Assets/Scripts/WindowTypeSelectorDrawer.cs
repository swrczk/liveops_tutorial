namespace Editor
{
#if UNITY_EDITOR
    using System;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(WindowTypeSelector))]
    public class WindowTypeSelectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Znajdź pole przechowujące nazwę typu okna
            var windowTypeNameProperty = property.FindPropertyRelative("windowTypeName");

            // Pobierz wszystkie typy dziedziczące po EditorWindow
            var editorWindowTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(EditorWindow).IsAssignableFrom(type) && !type.IsAbstract).ToArray();

            // Pobierz pełne nazwy typów okien do wyświetlenia w liście wyboru
            var typeNames = editorWindowTypes.Select(t => t.FullName).ToArray();
            var currentIndex = Array.IndexOf(typeNames, windowTypeNameProperty.stringValue);

            // Wyświetl listę rozwijaną z typami
            currentIndex = EditorGUI.Popup(position, label.text, currentIndex, typeNames);

            // Zapisz wybraną nazwę typu
            if (currentIndex >= 0)
            {
                windowTypeNameProperty.stringValue = typeNames[currentIndex];
            }

            EditorGUI.EndProperty();
        }
    }
#endif
}