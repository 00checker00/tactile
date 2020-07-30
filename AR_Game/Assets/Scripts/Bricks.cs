using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Klasse zum Manipulieren der Bausteine
public class Bricks : MonoBehaviour
{
    [HideInInspector] public bool isMoving = false;     // Wird der Baustein gerade bewegt?
    [HideInInspector] public List<GameObject> cells;    // Liste mit Zellen, die aktuell mit dem Baustein kollidieren              
    Vector3 poiPosition = new Vector3();                // Position des Point of Interest
    Vector3 brickPosition = new Vector3();              // Position des Bausteins 
    
    // Initialisierung
    void Start() {
        brickPosition = transform.position; 
    }

    void Update() {
        // Position des POI
        poiPosition = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.poi;

        // Trigger-Gesten PICK, DROP und CLICK erkennen
        DetectGesture(ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger, ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info);
        
        // Position des Bausteins aktualisieren und der Hand folgen
        if(isMoving) {
            transform.position = Camera.main.ViewportToScreenPoint(poiPosition);
        }
        // Wenn die Position des POI auf 0 springt, dann an den Startpunkt setzen
        if(transform.position.x == 0) {
            isMoving = false;
            transform.position = brickPosition;
        }
    }
    
    // Funktion zur Erkennung des Greifens und des Loslassens eines Bausteins (PICK, DROP, CLICK, RELEASE)
    void DetectGesture(ManoGestureTrigger triggerGesture, TrackingInfo trackingInfo)
    {
        // Test, ob der POI im Bereich des Bausteins liegt
        bool rectContainsPOI = RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Camera.main.ViewportToScreenPoint(poiPosition)); 

        // Baustein greifen 
        if (triggerGesture == ManoGestureTrigger.PICK && rectContainsPOI) {
            isMoving = true;
        }
        // Baustein loslassen
        if (triggerGesture == ManoGestureTrigger.DROP && isMoving) {
            isMoving = false;
            transform.position = brickPosition;
            // Prüfen, ob die Farben des abgelegten Bausteins zum Motiv passen
            GameObject.Find("Grid").GetComponent<GridManager>().EvaluateColorForCells(cells, gameObject.transform.GetChild(0).GetComponent<Image>().color);
            // Zellen entfernen für den nächsten Spielzug
            cells.Clear();
        }
        // Baustein rotieren (Bei jedem Click 90° um die eigene Achse Z)
        if (triggerGesture == ManoGestureTrigger.CLICK && rectContainsPOI) {
            transform.Rotate(0, 0, 90, Space.Self);
        }
    }
}


