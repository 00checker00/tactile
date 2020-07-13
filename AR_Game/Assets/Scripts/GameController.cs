using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Verwaltung des Spielzustandes:
// Level, Timer, Texteinblendungen, Gridzustände, Gridauswertung
public class GameController : MonoBehaviour
{
    private JSONReader reader;      // Variable zum Auslesen der Motiv-Daten aus der JSON-Datei
    private Motives motives;        // Motive und ihre Eigenschaften
    public Timer timer;             // Timer für Anzeigedauer und Spieldauer
    private int currentLevel;       // Aktuelles Level
    private int currentMotive;      // Aktuelles Motiv

    void Start() {
        // Motiv-Daten abholen
        motives = reader.readJSONMotives();
        timer = timer.GetComponent<Timer>();
        timer.startDisplayTimer();
        timer.displayTimerRunning = true;
        timer.buildTimerRunning = false;
    }

    void Update() {
        
    }

    // Level aktualisieren
    void updateLevel() {

    }

    // Timer aktualisieren
    void updateTimer() {

    }

    // Hinweistext anpassen
    void updateNotes() {

    }
}
