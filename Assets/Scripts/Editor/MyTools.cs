using UnityEngine;
using UnityEditor;

public static class MyTools
{
    [MenuItem("My Tools/Add To Report %F1")]
    static void DEV_AppendToReport()
    {
        CSVManager.AppendToReport(
            new string[2]
            {
                "1",
                "18"
            }
            );
        EditorApplication.Beep();
        Debug.Log("<color=green>Report uppdated successfully</color>");
    }
    
    [MenuItem("My Tools/Reset Report %F12")]
    static void DEV_ResetReport()
    {
        CSVManager.CreateReport();
        EditorApplication.Beep();
        Debug.Log("<color=orange>Report has been reset...</color>");
    }
}
