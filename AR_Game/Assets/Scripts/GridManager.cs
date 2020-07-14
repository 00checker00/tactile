using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Verwaltung des Grids:
// - Kacheln ein- und ausblenden
// - Farben anpassen
public class GridManager : MonoBehaviour
{
    public Color cellColor;
    void Start() {
        foreach(Transform child in transform) {
            child.gameObject.GetComponent<Image>().color = cellColor;
        }
    }

    public void hideGrid() {
        foreach (RectTransform child in transform)
            child.gameObject.SetActive(false);
    }
    void updateGrid(int level) {
        
    }

    // Motiv zum Merken anzeigen
    void displayMotiveInGrid() {

    }

    // Motiv ausblenden
    void hideMotiveInGrid() {

    }
}
