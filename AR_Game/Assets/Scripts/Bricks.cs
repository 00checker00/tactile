using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Klasse zum Manipulieren der Bausteine
public class Bricks : MonoBehaviour
{
    public bool isMoving = false;               // Wird der Baustein gerade bewegt?
    public bool dropped = false;                // Wurde der Baustein abgelegt?
    Vector3 poiPosition = new Vector3();        // Position des Point of Interest
    Vector3 brickPosition = new Vector3();      // Position des Bausteins 
    public List<GameObject> cells;              // Liste mit Zellen, die aktuell mit dem Baustein kollidieren
    
    // Initialisierung
    void Start() {
        brickPosition = transform.position; 
    }

    void Update() {
        // Position des POI
        poiPosition = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.poi;

        // PICK und DROP Gesten erkennen
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
    
    // Funktion zur Erkennung des Greifens und des Loslassens eines Bausteins (PICK und DROP)
    void DetectGesture(ManoGestureTrigger triggerGesture, TrackingInfo trackingInfo)
    {
        // Test, ob der POI im Bereich des Bausteins liegt
        bool rectContainsPOI = RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Camera.main.ViewportToScreenPoint(poiPosition)); 

        // Wurde eine Geste ausgeführt
        if (triggerGesture != ManoGestureTrigger.NO_GESTURE) {

            // Baustein greifen 
            if (triggerGesture == ManoGestureTrigger.PICK && rectContainsPOI) {
                isMoving = true;
            }
            // Baustein loslassen
            if (triggerGesture == ManoGestureTrigger.DROP && isMoving) {
                isMoving = false;
                transform.position = brickPosition;
                UpdateColorOfCells(cells);
            }
            // Baustein rotieren (Bei jedem Click 90° um die eigene Achse Z)
            if (triggerGesture == ManoGestureTrigger.CLICK && rectContainsPOI) {
                transform.Rotate(0, 0, 90, Space.Self);
            }
        }
    }

    // Farbe der Zellen im Grid aktualisieren, nachdem der Stein losgelassen wurde
    void UpdateColorOfCells(List<GameObject> listOfCells) {
        Color brickColor = gameObject.transform.GetChild(0).GetComponent<Image>().color;
        int count = 0;
        foreach(GameObject cell in listOfCells) {
            cell.GetComponent<Image>().color = brickColor;
            count++;
        }
        // Liste leeren
        cells.Clear();
    }
}


