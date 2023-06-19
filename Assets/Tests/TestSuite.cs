using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestSuite
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Menu");
    }
    [TearDown]
    public void TearDown()
    {
        UnityEditor.EditorApplication.ExitPlaymode();
    }

    [UnityTest]
    public IEnumerator ShipArrive()
    {
        yield return new WaitUntil(() => MapManager.Instance);
        yield return new WaitUntil(() => MapManager.Instance.shipArrived);
        Assert.True(MapManager.Instance.shipArrived);
    }
    [UnityTest]
    public IEnumerator TrasitionOver()
    {
        yield return new WaitUntil(() => Transition.instance);

        Assert.True(MapManager.Instance.shipArrived);
    }
}
