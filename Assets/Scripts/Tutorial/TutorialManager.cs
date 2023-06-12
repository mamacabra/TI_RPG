using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public struct State
    {
        public bool pauseGame;
        public bool tutorial;
        public bool arrowActive;
        public bool buttonActive;
        public string text;
        public Vector3 arrowPosition;
        public Vector3 arrowRotation;
    }
    public static TutorialManager instance;

    [SerializeField] private RectTransform arrow;
    [SerializeField] private TextMeshProUGUI popUpTxt;
    [SerializeField] private Button confirmBtn;


    [SerializeField] private State[] texts;
    [SerializeField] private int index;

    public bool isTutorial = true;

    private void Awake()
    {
        instance = this;
        confirmBtn.onClick.AddListener(DoNextTutorial);
        index = -1;
        DoNextTutorial();
    }
    private void Update()
    {
        arrow.transform.position += new Vector3(0, Mathf.Sin(Time.time * 5.0f) * 0.1f, 0);
    }

    public void DoNextTutorial()
    {
        index++;
        if (index >= texts.Length)
        {
            isTutorial = texts[index].tutorial;
            this.gameObject.SetActive(false);
        }
        else if (texts[index].text == "")
        {
            isTutorial = texts[index].tutorial;
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
            isTutorial = texts[index].tutorial;
            Time.timeScale = texts[index].pauseGame ? 0.0f : 1.0f;
            arrow.transform.gameObject.SetActive(texts[index].arrowActive);
            confirmBtn.gameObject.SetActive(texts[index].buttonActive);
            arrow.anchoredPosition3D = texts[index].arrowPosition;
            arrow.rotation = Quaternion.Euler(texts[index].arrowRotation);
            popUpTxt.text = texts[index].text;
        }
    }
    public void IgnoreTutorial()
    {
        index = texts.Length;
        DoNextTutorial();
    }
}
