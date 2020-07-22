﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [Space(10)]
    [Header("General")]
    public float waitBetweenSteps = 0.5f;
    public GameObject skipTutorialButton;

    [Space(10)]
    [Header("Introduction")]
    public List<GameObject> introductionText = new List<GameObject>();
    [Space(10)]
    public float welcomeDisplayTime = 2;
    public float textDisplayTime = 4;

    [Space(10)]
    [Header("Explaining Pinch")]
    public GameObject pinchText;
    public GameObject pinchSprite;
    public float pinchDisplayTime = 6.9f;

    [Space(10)]
    [Header("Explaining Click")]
    public GameObject clickText;
    public GameObject clickSprite;
    public float clickDisplayTime = 7;

    [Space(10)]
    [Header("GameObjects for Gamestart")]
    public List<GameObject> gameObjects;


    private void Start()
    {
        // Deactivate all tutorial items (children of tutorial master) first
        // so they will only be activated once we need them
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(false);
        }

        StartCoroutine(StartIntroduction());
    }

    private IEnumerator StartIntroduction()
    {
        int countIntroText = 0;

        skipTutorialButton.SetActive(true);

        // Loading introduction text elements one by one.
        foreach (GameObject obj in introductionText)
        {
            obj.SetActive(true);
            countIntroText++;

            // Waiting time after "welcome" will be shorter than
            // after the other intro text elements
            if (countIntroText == 1)
            {
                yield return new WaitForSeconds(welcomeDisplayTime);
            }
            else
            {
                yield return new WaitForSeconds(textDisplayTime);
            }
        }

        // All introduction text has been displayed. Hiding text now.
        if (countIntroText == introductionText.Count)
        {
            foreach (GameObject obj in introductionText)
            {
                obj.SetActive(false);
            }

            yield return new WaitForSeconds(waitBetweenSteps);
            StartCoroutine(ExplainPinch());
        }
    }

    private IEnumerator ExplainPinch()
    {
        pinchText.SetActive(true);
        pinchSprite.SetActive(true);

        yield return new WaitForSeconds(pinchDisplayTime);

        pinchText.SetActive(false);
        pinchSprite.SetActive(false);

        yield return new WaitForSeconds(waitBetweenSteps);
        StartCoroutine(ExplainClick());
    }

    private IEnumerator ExplainClick()
    {
        clickText.SetActive(true);
        clickSprite.SetActive(true);

        yield return new WaitForSeconds(clickDisplayTime);

        clickText.SetActive(false);
        clickSprite.SetActive(false);

        // End tutorial and load game scene either in the background or let the tutorial freeze.
        // It doesn't really matter if the tutorial freezes since nothing actually happens...
        // ...from this point on anyway.
        yield return new WaitForSeconds(waitBetweenSteps);
        EndTutorialAsync();
    }

    public void SkipTutorial()
    {
        // Deactivate all tutorial items (children of tutorial master)
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(false);
        }

        EndTutorialAsync();
    }

    // End tutorial and load scene with game 
    // *currently not in use*
    private void EndTutorial()
    {
        skipTutorialButton.SetActive(false);

        foreach(GameObject obj in gameObjects) {
            obj.SetActiveRecursively(true);
        } 
        transform.gameObject.SetActiveRecursively(false);
    }

    // End tutorial and load scene with game asynchronously in the background
    private void EndTutorialAsync()
    {
        skipTutorialButton.SetActive(false);

        foreach(GameObject obj in gameObjects) {
            obj.SetActiveRecursively(true);
        } 
        transform.gameObject.SetActiveRecursively(false);
    }
}