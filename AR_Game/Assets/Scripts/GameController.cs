using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controller-Klasse für die Verwaltung des Spielzustandes
// Level, Timer, Texteinblendungen, Gridzustände, Gridauswertung
public class GameController : MonoBehaviour
{
    public List<GameObject> prefabs;                        // Prefabs der Bausteine zum Erstellen von Instanzen
    [HideInInspector] public List<GameObject> brickList;    // Liste zum Verwalten der sichtbaren Bausteine
    private GameObject reader;                              // Variable für den Zugriff auf die Motiv-Daten
    private GameObject grid;                                // Variable für Zugriff auf das Gitter und dessen Zellen
    private Timer timer;                                    // Timer für die Steuerung der Zeitanzeige
    private Motives jsonMotives;                            // Motive und ihre Eigenschaften
    private int currentLevel;                               // Aktuelles Level
    private int currentMotiveIndex;                         // Index des aktuellen Motivs

    void Start() {
        // Initialisierung der Variablen
        brickList = new List<GameObject>();
        reader = GameObject.Find("JSONReader");
        grid = GameObject.Find("Grid");
        timer = GameObject.Find("TimerLabel").GetComponent<Timer>();
        
        currentMotiveIndex = 0;

        // Erstes Motiv einblenden
        DisplayMotive(0);
    }

    void Update() {
        // Timer für das Merken des Motivs überwachen
        if(timer.displayTimerExpired) {
            timer.displayTimerExpired = false;
            HideMotive(currentMotiveIndex);
            SwapBricksForMotive(currentMotiveIndex);
        }
        // Timer für das Bauen des Motivs überwachen
        if(timer.buildTimerExpired) {
            timer.buildTimerExpired = false;

            // Grid zurücksetzen
            grid.GetComponent<GridManager>().currentMotiveCells.Clear();
            grid.GetComponent<GridManager>().completedCells.Clear();
            DisplayMotive(currentMotiveIndex);
        }

        // Timer zurücksetzen, nachdem das Motiv gelöst wurde
        if(grid.GetComponent<GridManager>().isMotiveReady) {
            currentMotiveIndex++;
            timer.stopBuildTimer();
            timer.startDisplayTimer();
            grid.GetComponent<GridManager>().completedCells.Clear();
            grid.GetComponent<GridManager>().isMotiveReady = false;
        }
    }

    // Aktuelles Motiv aus den JSON-Daten holen und anzeigen
    void DisplayMotive(int index) {

        // Baustein-Liste leeren
        ClearBrickList(brickList);
        // Grid zurücksetzen
        grid.GetComponent<GridManager>().ClearGrid();
        grid.GetComponent<GridManager>().HideUnusedCells();
        // Motivdaten abholen
        jsonMotives = reader.GetComponent<JSONReader>().motivesInJson;
        Motive motive = jsonMotives.motives[index];
        // Index des aktuellen Motivs setzen
        currentMotiveIndex = index;
        
        foreach(Cell cell in motive.cells) {
            foreach(string cellid in cell.cellids) {
                Image image = GameObject.Find(cellid).GetComponent<Image>();
                image.color = new Color32((byte)cell.cellcolor[0], (byte)cell.cellcolor[1], (byte)cell.cellcolor[2], 255);
                grid.GetComponent<GridManager>().currentMotiveCells.Add(cellid, image.color);                                                                  
            }
        }
        Debug.Log("index :" + index);
        Debug.Log("motives :" + jsonMotives.motives.Count);

        UpdateNotes("Merke dir das Motiv " + motive.name + "!");
        UpdateLevel(motive.level);
    }
    
    // Aktuelles Motiv verschwinden lassen
    void HideMotive(int index) {

        jsonMotives = reader.GetComponent<JSONReader>().motivesInJson;
        Motive motive = jsonMotives.motives[index];

        foreach(Cell cell in motive.cells) 
            foreach(string cellid in cell.cellids)
                GameObject.Find(cellid).GetComponent<Image>().color = grid.GetComponent<GridManager>().cellColor;

        UpdateNotes("Baue das Motiv " + motive.name + " nach!");
    }

    // Steine für aktuelles Motiv instanziieren und anzeigen lassen
    void SwapBricksForMotive(int index) {

        Motive motive = jsonMotives.motives[index];
        int yPosition = 170;

        for(int i = 0; i < motive.bricks.Count; i++) {
            GameObject obj = prefabs.Find((x) => x.name == motive.bricks[i].brickid);           
            GameObject prefab = Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);            
            RectTransform rt = prefab.GetComponent<RectTransform>();
            float height = GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height;
            rt.anchoredPosition = new Vector3(-355, yPosition, 0); 
            yPosition -= 110;
            foreach(Transform child in prefab.transform) 
                child.GetComponent<Image>().color = new Color32((byte)motive.bricks[i].brickcolor[0], 
                                                                (byte)motive.bricks[i].brickcolor[1], 
                                                                (byte)motive.bricks[i].brickcolor[2], 255);                                               
            brickList.Add(prefab);
        }  
    }
    
    // Level-Label aktualisieren
    void UpdateLevel(int level) {
        GameObject.Find("LevelLabel").GetComponent<Text>().text = "Level " + level;
    }

    // Hinweis-Label aktualisieren
    void UpdateNotes(string notes) {
        GameObject.Find("NotesLabel").GetComponent<Text>().text = notes;
    }

    // Liste mit den Bausteinen leeren für Bausteine des nächsten Motivs
    void ClearBrickList(List<GameObject> list) {
        foreach(GameObject obj in list) 
            GameObject.Destroy(obj);
        
        brickList.Clear();
    }
}
