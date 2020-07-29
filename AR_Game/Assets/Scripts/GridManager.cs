using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

// Klasse zur Verwaltung des Grids:
// - Kacheln ein- und ausblenden
// - Farben anktualisieren
// - Farbe auf den Ursprungszustand zurücksetzen
public class GridManager : MonoBehaviour
{
    public Color cellColor;                                                     // Ausgangsfarbe der Zellen im Grid
    
    public Color defaultColor;
    [HideInInspector] public bool isMotiveReady;                                // Wurde das Motiv komplett nachgebaut?
    [HideInInspector] public Dictionary<string, Color> currentMotiveCells = new Dictionary<string, Color>();     // Alle Zellen des Grids, die für das aktuelle Motiv benötigt werden
    [HideInInspector] public Dictionary<string, Color> completedCells = new Dictionary<string, Color>();        // Fertig gestellte Teile (Zellen) des Motivs

    // Initialisierung der Variablen
    void Awake() {
    
        ClearGrid();
        isMotiveReady = false;

        defaultColor = new Color();
        defaultColor.a = GameObject.Find("1_1").GetComponent<Image>().color.a;
        defaultColor.r = GameObject.Find("1_1").GetComponent<Image>().color.r;
        defaultColor.g = GameObject.Find("1_1").GetComponent<Image>().color.g;
        defaultColor.b = GameObject.Find("1_1").GetComponent<Image>().color.b;
    }

    // Abfrage, ob das Motiv fertiggestellt wurde
    // Nach Fertigstellung wird eine Nachricht anzeigt
    void Update() {
        if(currentMotiveCells.Count == completedCells.Count) {
            isMotiveReady = true;
            ManoEvents.Instance.DisplayLogMessage("Richtig!");
        }
    }

    // Prüfen, ob die Farbe des gelegten Bausteins mit der Farbe des Motivs übereinstimmt
    public void EvaluateColorForCells(List<GameObject> listOfCells, Color color) {
        // Wurde ein Stein auf einen anderen Stein gelegt?
        foreach(GameObject currentCell in listOfCells) { 
            if(!currentCell.GetComponent<Image>().color.Equals(cellColor)) 
                return;
        }

        // Wenn der abgelegte Baustein zum Motiv gehört, dann speichere ihn für die Überwachung des Fortschritts
        int counter = 0;
        foreach(GameObject currentCell in listOfCells) {  
            KeyValuePair<string, Color> pair = new KeyValuePair<string, Color>(currentCell.name, color);
            if(currentMotiveCells.Contains(pair)) {
                counter++;
                if(!completedCells.Contains(pair))
                    completedCells.Add(pair.Key, pair.Value);
            }
        }

        // Wenn die Farben des Bausteins zu den Kacheln im Grid passen, dann färbe das Grid um
        // Ansonsten färbe nicht um und zeige dem User einen Hinweis 
        if(listOfCells.Count == counter) {
            SetColorForCells(listOfCells, color);
        }
        else {
            ManoEvents.Instance.DisplayLogMessage("Falsch!");
        }
    }

    // Farbe der Zellen im Grid nach Ablegen eines Bausteins in die Farbe des Bausteins ändern
    public void SetColorForCells(List<GameObject> listOfCells, Color color) {
        foreach(GameObject cell in listOfCells) 
            cell.GetComponent<Image>().color = color;
    }

    // Farbe aller Zellen im Grid zurücksetzen
    public void ClearGrid() {
        foreach(Transform child in transform) {
            defaultColor.a = 0.5f;
            child.gameObject.GetComponent<Image>().color = defaultColor;

        }

        completedCells.Clear();
    }

    // Zellen des Grids dynamisch an das aktuelle Motiv anpassen
    // Nur die notwendigen Zellen einblenden und alle anderen verbergen 
    public void HideUnusedCells() {
        foreach(Transform child in transform) {
            if(!currentMotiveCells.ContainsKey(child.gameObject.name)) {
                Color color = new Color();
                color = child.gameObject.GetComponent<Image>().color;
                color.a = 0.0f;
                child.gameObject.GetComponent<Image>().color = color;
            }
            else {
                child.gameObject.GetComponent<Image>().color = cellColor;
            }
        }
    }
}
