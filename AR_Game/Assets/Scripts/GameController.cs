using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controller-Klasse für die Verwaltung des Spielzustandes
// Level, Timer, Texteinblendungen, Gridzustände, Gridauswertung
public class GameController : MonoBehaviour
{
    public List<GameObject> prefabs;    // Prefabs der Bausteine zum Erstellen von Instanzen
    public List<GameObject> brickList;  // Liste zum Verwalten der sichtbaren Bausteine
    private GameObject reader;          // Variable für den Zugriff auf die Motiv-Daten
    private GameObject grid;            // Variable für Zugriff auf das Gitter und dessen Zellen
    private Timer timer;                // Timer für die Steuerung der Zeitanzeige
    private Motives jsonMotives;        // Motive und ihre Eigenschaften
    private int currentLevel;           // Aktuelles Level
    private int currentMotive;          // Index des aktuellen Motiv

    void Start() {
        brickList = new List<GameObject>();
        reader = GameObject.Find("JSONReader");
        grid = GameObject.Find("Grid");
        timer = GameObject.Find("TimerLabel").GetComponent<Timer>();
        currentMotive = 0;
        displayMotive(0);
    }

    void Update() {
        // Timer für das Merken des Motivs überwachen
        if(timer.displayTimerExpired) {
            timer.displayTimerExpired = false;
            hideMotive(currentMotive);
            swapBricksForMotive(currentMotive);
        }
        // Timer für das Bauen des Motivs überwachen
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
                GameObject.Find(cellid).GetComponent<Image>().color = new Color32((byte)cell.cellcolor[0], 
                                                                                  (byte)cell.cellcolor[1], 
                                                                                  (byte)cell.cellcolor[2], 255);
            }
        }
        clearBrickList(brickList);
        updateNotes("Motiv " + motive.name + " merken!");
        updateLevel(motive.level);
    }
    
    // Aktuelles Motiv verschwinden lassen
    void hideMotive(int index) {
        jsonMotives = reader.GetComponent<JSONReader>().motivesInJson;
        Motive motive = jsonMotives.motives[index];
        foreach(Cell cell in motive.cells) 
            foreach(string cellid in cell.cellids)
                GameObject.Find(cellid).GetComponent<Image>().color = grid.GetComponent<GridManager>().cellColor;

        updateNotes("Motiv " + motive.name + " bauen!");
    }

    // Steine für aktuelles Motiv instanziieren und anzeigen lassen
    void swapBricksForMotive(int index) {
        Motive motive = jsonMotives.motives[index];
        for(int i = 0; i < motive.bricks.Count; i++) {
            GameObject obj = prefabs.Find((x) => x.name == motive.bricks[i].brickid);
            GameObject prefab = Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
            
            foreach(Transform child in prefab.transform) 
                child.GetComponent<Image>().color = new Color32((byte)motive.bricks[i].brickcolor[0], 
                                                                (byte)motive.bricks[i].brickcolor[1], 
                                                                (byte)motive.bricks[i].brickcolor[2], 255);                                               
            brickList.Add(prefab);
        }  
    }
    
    // Level-Label aktualisieren
    void updateLevel(int level) {
        GameObject.Find("LevelLabel").GetComponent<Text>().text = "Level " + level;
    }

    // Hinweis-Label aktualisieren
    void updateNotes(string notes) {
        GameObject.Find("NotesLabel").GetComponent<Text>().text = notes;
    }

    void clearBrickList(List<GameObject> list) {
        foreach(GameObject obj in list) 
            GameObject.Destroy(obj);

        brickList.Clear();
    }
}
