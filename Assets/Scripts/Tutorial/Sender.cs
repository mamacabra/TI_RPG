using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    private void OnMouseDown()
    {
        TutorialManager.instance?.DoNextTutorial();
    }
}
