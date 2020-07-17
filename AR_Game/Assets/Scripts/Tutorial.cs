using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [Space(10)]
    [Header("Introduction")]
    public List<GameObject> introductionText = new List<GameObject>();
    [Space(10)]
    public float textDisplayTime = 4;

    [Space(10)]
    [Header("Explaining Pinch")]
    public GameObject pinchText;
    public GameObject pinchSprite;
    public float pinchDisplayTime = 10;

    // TODO: Sprite animation for pinch

    [Space(10)]
    [Header("Explaining Click")]
    public GameObject clickText;
    public GameObject clickSprite;
    public float clickDisplayTime = 10;

    // TODO: Sprite animation for click

    // TODO: Gesture for reset/undo?

    [Space(10)]
    [Header("Loading Game")]
    public Object gameScene;

    // TODO: Button to skip tutorial

    private void Start()
    {
        // TODO: Deactivate all tutorial items first
        StartCoroutine(StartIntroduction());
    }

    private IEnumerator StartIntroduction()
    {
        int countIntroText = 0;

        // Loading introduction text elements one by one.
        foreach (GameObject obj in introductionText)
        {
            obj.SetActive(true);
            countIntroText++;

            yield return new WaitForSeconds(textDisplayTime);

            // TODO: Adjust wait time after welcome
        }

        // All introduction text has been displayed. Hiding text now.
        if (countIntroText == introductionText.Count)
        {
            foreach (GameObject obj in introductionText)
            {
                obj.SetActive(false);
            }

            StartCoroutine(ExplainPinch());

            // TODO: Add wait time after every level of the tutorial
        }
    }

    private IEnumerator ExplainPinch()
    {
        Debug.Log("Explaining pinch.");
        pinchText.SetActive(true);
        pinchSprite.SetActive(true);

        yield return new WaitForSeconds(pinchDisplayTime);

        pinchText.SetActive(false);
        pinchSprite.SetActive(false);

        StartCoroutine(ExplainClick());
    }

    private IEnumerator ExplainClick()
    {
        Debug.Log("Explaining click.");
        clickText.SetActive(true);
        clickSprite.SetActive(true);

        yield return new WaitForSeconds(clickDisplayTime);

        clickText.SetActive(false);
        clickSprite.SetActive(false);

        StartCoroutine(EndTutorialAsync());
        //EndTutorial();

    }

    // End tutorial and load scene with game

    private void EndTutorial()
    {
        // Loading next scene based on build index
        SceneManager.LoadScene(1);

        // Loading next scene based on name
        //if (gameScene != null)
        //{
        //    SceneManager.LoadScene(gameScene.name);
        //}
    }

    // End tutorial and load scene with game asynchronously in the background
    private IEnumerator EndTutorialAsync()
    {
        // Loading next scene based on build index
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncSceneLoad.isDone)
        {
            yield return null;
        }

        // Loading next scene based on name
        //Debug.Log("Scene name: " + gameScene.name);
        //if (gameScene != null)
        //{
        //    AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(gameScene.name);

        //    while (!asyncSceneLoad.isDone)
        //    {
        //        yield return null;
        //    }
        //}
    }
}
