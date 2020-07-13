using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Klasse zur Steuerung der Zeitanzeige
public class Timer : MonoBehaviour
{
    // Timer für das Anzeigen und Merken des Motives
    private double displayTime = 10.0;
    public bool displayTimerRunning = false;

    // Timer für das Nachbauen des Motivs
    private double buildTime = 60.0;
    public bool buildTimerRunning = false;

    void Update() {
        if(displayTimerRunning) {
            displayTime -= Time.deltaTime;
            this.gameObject.GetComponent<Text>().text = displayTime.ToString();
        }
        if(displayTime < 0) {
            stopDisplayTimer();
            startBuildTimer();
            resetTimer();
            // Motiv ausblenden
            // Spiel beginnen
        }

        if(buildTimerRunning) {
            buildTime -= Time.deltaTime;
            this.gameObject.GetComponent<Text>().text = buildTime.ToString();
        }
        if(buildTime < 0) {
            stopDisplayTimer();
            startBuildTimer();
            resetTimer();
            // Zeit abgelaufen
            // Nächstes Motiv
        }
    }

    // Methoden zum Starten, Anhalten und Zurücksetzen der Timer über den GameController
    public void startDisplayTimer() {
        displayTimerRunning = true;
        buildTimerRunning = false;
    }

    public void stopDisplayTimer() {
        displayTimerRunning = false;
        resetTimer();
    }

    public void startBuildTimer() {
        buildTimerRunning = true;
        displayTimerRunning = false;
    }

    public void stopBuildTimer() {
        buildTimerRunning = false;
        resetTimer();
    }

    public void resetTimer() {
        displayTime = 10.0;
        buildTime = 60.0;
    }
}
