using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Klasse zum Manipulieren der Bausteine
public class Bricks : MonoBehaviour
{
    bool isMoving = false;                  // Wird der Baustein gerade bewegt?
    Vector3 poiPosition = new Vector3();    // Position des Point of Interest
    
    void Update() {
        // Position des POI
        poiPosition = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.poi;

        // PICK und DROP Gesten erkennen
        DetectGesture(ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger, ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info);
        
        // Position des Bausteins aktualisieren und der Hand folgen
        if(isMoving) {
            transform.position = Camera.main.ViewportToScreenPoint(poiPosition);
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
            if (triggerGesture == ManoGestureTrigger.DROP) {
                isMoving = false;
            }
        }
    }
}
