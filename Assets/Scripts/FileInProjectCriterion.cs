using System.IO;
using UnityEngine;
using UnityEditor;
using Unity.Tutorials.Core.Editor;

public class FileInProjectCriterion : Criterion
{
    // Ścieżka do pliku, który ma być sprawdzony
    public string filePath;

    // Sprawdzenie, czy kryterium jest spełnione
    protected override bool EvaluateCompletion()
    {
        Debug.Log("EvaluateCompletion called for FileInProjectCriterion");
        return IsFileInProject(filePath);
    }
    public override void StartTesting()
    {
        base.StartTesting();
        Debug.Log("StartTesting called for FileInProjectCriterion");
        UpdateCompletion();
    }

    public override bool AutoComplete()
    {
        return false;
    }

    // Metoda, która sprawdza obecność pliku w projekcie
    private bool IsFileInProject(string relativePath)
    {
        string fullPath = Path.Combine(Application.dataPath, relativePath); 
        var isFileInProject = AssetDatabase.LoadAssetAtPath<Object>(relativePath) != null;
        Debug.Log($"File {relativePath} is in project: {isFileInProject}");
        return isFileInProject;
    } 
}