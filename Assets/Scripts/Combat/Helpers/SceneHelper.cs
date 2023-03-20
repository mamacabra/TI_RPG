using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    public void BackToMap()
    {
        if (MapManager.Instance) MapManager.Instance.UnloadScenes();
        else  SceneManager.LoadScene("SampleMap");
    }
}
