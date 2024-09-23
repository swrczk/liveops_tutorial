namespace Editor
{
#if UNITY_EDITOR
    using System;

    [Serializable]
    public class WindowTypeSelector
    {
        // Przechowuje pełną nazwę typu wybranego okna
        public string windowTypeName;

        // Funkcja pomocnicza, która zwraca typ na podstawie zapisanej nazwy
        public Type GetSelectedWindowType()
        {
            if (string.IsNullOrEmpty(windowTypeName))
                return null;

            return typeof(UnityEditor.Editor).Assembly.GetType(windowTypeName);
        }
    }
#endif
}