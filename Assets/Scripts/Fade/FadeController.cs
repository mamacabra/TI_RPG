using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
public class FadeController : MonoBehaviour
{

    public int levelIndex;

    public string levelName;

    public Image backgroundImage;

    public Animator anim;

    //public Button fadeButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ButtonFade()
    {
        StartCoroutine(FadeManager());
    }

    IEnumerator FadeManager()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(()=>backgroundImage.color.a==1.0f);
        //SceneManager.LoadScene(levelName);
        //SceneManager.LoadScene(levelIndex);
    }
}
