using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skript zur Bestimmung der Kollision eines Bausteins mit einer Zelle

public class CellCollisionDetector : MonoBehaviour
{
    // Aktuelle Zelle des Grids in der Liste des Bausteins zwischenspeichern, 
    // sobald eine Kollision mit dem Baustein erfolgt
    void OnTriggerEnter(Collider other) {  
        transform.parent.GetComponent<Bricks>().cells.Add(other.gameObject);
    }

    // Aktuelle Zelle des Grids aus der Liste des Bausteins entfernen,
    // sobald keine Kollision mehr mit dem Baustein besteht
    void OnTriggerExit(Collider other) {
        transform.parent.GetComponent<Bricks>().cells.Remove(other.gameObject);
    }
}
