using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Verwaltung des Spielzustandes:
// Level, Timer, Texteinblendungen, Gridzustände, Gridauswertung
public class GameController : MonoBehaviour
{
    private GameObject reader;      // Variable zum Auslesen der Motiv-Daten aus der JSON-Datei
    private GameObject grid;        // Grid  
    private Timer timer;            // Timer  
    private Motives jsonMotives;    // Motive und ihre Eigenschaften
    private int currentLevel;       // Aktuelles Level
    private int currentMotive;      // Index des aktuellen Motiv

    void Start() {
        reader = GameObject.Find("JSONReader");
        grid = GameObject.Find("Grid");
        timer = GameObject.Find("TimerLabel").GetComponent<Timer>();
        currentMotive = 0;
        displayMotive(0);
    }

    void Update() {
        
        if(timer.displayTimerExpired) {
            timer.displayTimerExpired = false;
            hideMotive(currentMotive);
        }
        if(timer.buildTimerExpired) {
            timer.buildTimerExpired = false;
            currentMotive++;
            displayMotive(currentMotive);
        }
    }

    // Aktuelles Motiv aus den JSON-Daten holen und anzeigen
    void displayMotive(int index) {
        currentMotive = index;
        jsonMotives = reader.GetComponent<JSONReader>().motivesInJson;
        Motive motive = jsonMotives.motives[index];
        foreach(Cell cell in motive.cells) {
            foreach(string cellid in cell.cellids) {
                GameObject.Find(cellid).GetComponent<Image>().color = new Color32(
                                                                        (byte)cell.cellcolor[0], 
                                                                        (byte)cell.cellcolor[1], 
                                                                        (byte)cell.cellcolor[2], 255);
            }
        }
        updateNotes("Motiv " + motive.name + " merken!");
        updateLevel(motive.level);
        //swapBricksForMotive(index);
    }
    
    // Aktuelles Motiv verschwinden lassen
    void hideMotive(int index) {
        jsonMotives = reader.GetComponent<JSONReader>().motivesInJson;
        Motive motive = jsonMotives.motives[index];
        foreach(Cell cell in motive.cells) {
            foreach(string cellid in cell.cellids) {
                GameObject.Find(cellid).GetComponent<Image>().color = new Color32(0, 0, 0, 100);
            }
        }
        updateNotes("Motiv " + motive.name + " bauen!");
    }

    void swapBricksForMotive(int index) {
        Motive motive = jsonMotives.motives[index];
        foreach(Brick brick in motive.bricks) {
            Debug.Log("BrickID: " + brick.brickid + "width: " + brick.width + "height: " + brick.height);
            foreach(int color in brick.brickcolor) 
                Debug.Log("Color: " + color);
        }
    }

    // Level aktualisieren
    void updateLevel(int level) {
        GameObject.Find("LevelLabel").GetComponent<Text>().text = "Level " + level;
    }

    // Hinweistext anpassen
    void updateNotes(string notes) {
        GameObject.Find("NotesLabel").GetComponent<Text>().text = notes;
    }
}
