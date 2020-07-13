using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Verwaltung des Grids:
// - Kacheln ein- und ausblenden
// - Farben anpassen
public class GridManager : MonoBehaviour
{
    void Start() {
        GameObject[] children;
        children = GetComponentsInChildren<GameObject>();
        foreach (GameObject child in children){
            child.SetActive(false);
        } 
    }

    void Update() {
        
    }

    // Anzahl der eingeblendeten Kacheln im Grid aktualisieren, basierend auf dem aktuellen Level 
    // Level 0: 2x2
    // Level 1: 
    // Level 2: 5x5
    // Level 3: 7x7
    void updateGrid(int level) {
        
    }

    // Motiv zum Merken anzeigen
    void displayMotiveInGrid() {

    }

    // Motiv ausblenden
    void hideMotiveInGrid() {

    }
}
