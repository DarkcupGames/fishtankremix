using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class OpenSceneEditor : MonoBehaviour
{
    [MenuItem("Project/Open Login #a", priority = 5000)]
    public static void OpenLoading()
    {
        OpenScene("Login");
        //EditorApplication.EnterPlaymode();
    }

    [MenuItem("Project/Open Scene - Game Play %&w", false, 5000)]
    public static void OpenMain()
    {
        OpenScene("Gameplay");
    }

    private static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/_Game/Scenes/" + sceneName + ".unity");
        }
    }

    private static void OpenNewScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/New/" + sceneName + ".unity");
        }
    }

    //[MenuItem("Project/Open Scene - Battle %&e", false, 5000)]
    //public static void OpenGamePlaySCene()
    //{
    //    OpenScene("Battle");
    //}

    //[MenuItem("Project/Open Scene - BlackHole", false, 5000)]
    //public static void OpenGamePlaySCenffe()
    //{
    //    OpenScene("BlackHole");
    //}

    //[MenuItem("Project/Open Scene - Explore", false, 5000)]
    //public static void OpenGamePlaySfdsfCenffe()
    //{
    //    OpenScene("Explore");
    //}

    //[MenuItem("Project/Open Scene - MeteorBelt", false, 5000)]
    //public static void OpenGamePlaySfdsfCenffeff()
    //{
    //    OpenScene("MeteorBelt");
    //}

    //[MenuItem("Server/Run Data On Server", false, 5000)]
    //public static void RunOnServer()
    //{
    //    ServerConstants.BASE_URL = "https://universe-master-test2.herokuapp.com/";
    //}

    //[MenuItem("Server/Run Data On Client", false, 5000)]
    //public static void RunOnClient()
    //{
    //    ServerConstants.BASE_URL = "http://localhost:8080/";
    //}


}
