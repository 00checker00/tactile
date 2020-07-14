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
    void Awake() {
        clearGrid();
    }

    //fixen
    public void hideGrid() {
        foreach (RectTransform child in transform)
            child.gameObject.SetActive(false);
    }

    public void clearGrid() {
        foreach(Transform child in transform) {
            child.gameObject.GetComponent<Image>().color = cellColor;
        }
    }
    // Motiv zum Merken anzeigen
    void displayMotiveInGrid() {

    }

    // Motiv ausblenden
    void hideMotiveInGrid() {

    }
}
