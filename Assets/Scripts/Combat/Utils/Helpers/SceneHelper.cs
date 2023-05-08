using UnityEngine;
using UnityEngine.SceneManagement;

namespace Combat.Helpers
{
     
    public class SceneHelper : MonoBehaviour
    {
        public void BackToMap()
        {
            if (MapManager.Instance) MapManager.Instance.UnloadScenes();
            else MapManager.Instance.CallScreenFade(); SceneManager.LoadScene("SampleMap");
        }
    }
}