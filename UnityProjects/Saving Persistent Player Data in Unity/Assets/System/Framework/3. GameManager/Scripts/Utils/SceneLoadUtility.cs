using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadUtility : MonoBehaviour
{
    public string sceneToLoad = "Main";

    void Awake()
    {
        GameManager.Instance.SetDefaultLevel(sceneToLoad);
    }

    IEnumerator Load(string sceneName)
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.LoadLevel(sceneToLoad);
    }
}
