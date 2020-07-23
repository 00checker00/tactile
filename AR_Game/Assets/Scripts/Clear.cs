using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Klasse zum Clearen des Grids
public class Clear : MonoBehaviour
{
    [HideInInspector] public bool isMoving = false;     

    Vector3 gridPosition = new Vector3();             
    
    GridManager gridManager;

    // Initialisierung
    void Start() {
        gridPosition = transform.position; 

        gridManager = GetComponent<GridManager>();

    }

    void Update() {

        // Trigger-Gesten PICK, DROP und CLICK erkennen
        DetectGesture(ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger, ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info);
        
        // Grid ist dann im Nirvana kein Bug
        if(isMoving) {
            transform.position = Camera.main.ViewportToScreenPoint(gridPosition);
        }
     
        if(transform.position.x == 0) {
            isMoving = false;
            transform.position = gridPosition;
        }
    }
    
 
    void DetectGesture(ManoGestureTrigger triggerGesture, TrackingInfo trackingInfo)
    {
        
        // Grid greifen 
        if (triggerGesture == ManoGestureTrigger.GRAB_GESTURE) {
            isMoving = true;
        }
        // Grid loslassen
        if (triggerGesture == ManoGestureTrigger.RELEASE_GESTURE && isMoving) {
            isMoving = false;
            transform.position = gridPosition;

           
             //Ob die Grab-Geste an der Kante ist
            if(ManomotionManager.Instance.Hand_infos[0].hand_info.warning == Warning.WARNING_APPROACHING_RIGHT_EDGE)
            {
                
                gridManager.ClearGrid();
                gridManager.HideUnusedCells();

            }
            
           
        }
      
    }
}


