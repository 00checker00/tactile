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
    public Color cellColor;
    [HideInInspector] public bool isMotiveReady;
    [HideInInspector] public bool isBrickCorrect;
    [HideInInspector] public Dictionary<string, Color> currentMotiveCells;
    [HideInInspector] public Dictionary<string, Color> completedCells;

    void Awake() {
        ClearGrid();
        currentMotiveCells = new Dictionary<string, Color>();
        completedCells = new Dictionary<string, Color>();
    }

    public void EvaluateColorForCells(List<GameObject> listOfCells, Color color) {
        int counter = 0;
        foreach(GameObject currentCell in listOfCells) {  
            KeyValuePair<string, Color> pair = new KeyValuePair<string, Color>(currentCell.name, color);
            if(currentMotiveCells.Contains(pair)) 
                counter++;
            if(!completedCells.Contains(pair)) 
                completedCells.Add(pair.Key, pair.Value);
            
        }   
        // Auswertung, ob die Farben des Steins mit den Farben des Motivs an der Position übereinstimmen
        if(listOfCells.Count == counter) {
            SetColorForCells(listOfCells, color);
            Debug.Log("Richtig!");
        }
        else {
            ManoEvents.Instance.DisplayLogMessage("Das war falsch!");
        }
    }

    public void SetColorForCells(List<GameObject> listOfCells, Color color) {
        foreach(GameObject cell in listOfCells) 
            cell.GetComponent<Image>().color = color;
    }

    public void ClearGrid() {
        foreach(Transform child in transform) {
            //child.gameObject.GetComponent<Image>().color = cellColor;
            Color color = new Color();
            color = child.gameObject.GetComponent<Image>().color;
            color.a = 0.5f;
            child.gameObject.GetComponent<Image>().color = color;
        }
    }

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
