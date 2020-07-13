using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Verwaltung des Grids:
// - Kacheln ein- und ausblenden
// - Farben anpassen
public class GridManager : MonoBehaviour
{
    void Start() {
        
    }

    public void hideGrid() {
        foreach (RectTransform child in transform)
            child.gameObject.SetActive(false);
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
