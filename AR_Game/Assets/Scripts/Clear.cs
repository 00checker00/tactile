using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

// Klasse zum Clearen des Grids
public class Clear : MonoBehaviour
{
    [HideInInspector] public bool isMoving = false;    

    private long clearTimeStamp = 0;
    private long rightEdgeTimeStamp = 0;

    public GameObject TimerLabel; 

    Vector3 gridPosition = new Vector3();             
    
    GridManager gridManager;

    Timer timer;

    // Initialisierung
    void Start() {
        gridPosition = transform.position; 

        gridManager = GetComponent<GridManager>();

        timer =  TimerLabel.GetComponent<Timer>();

    }

    void Update() {

        // Trigger-Gesten PICK, DROP und CLICK erkennen
        //DetectGesture(ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger, ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info);
        DetectContinuous(ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous, ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info);



    }
    

    void DetectContinuous(ManoGestureContinuous continuous, TrackingInfo trackingInfo)
    {
        var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        if(ManomotionManager.Instance.Hand_infos[0].hand_info.warning == Warning.WARNING_APPROACHING_RIGHT_EDGE)
        {
            rightEdgeTimeStamp = now + 1000; 

        }

      
           // Grid greifen 
        if (continuous == ManoGestureContinuous.CLOSED_HAND_GESTURE  && !timer.displayTimerRunning) {
            
            clearTimeStamp = now + 900; 

            // Grid ist dann im Nirvana kein Bug
            transform.position = Camera.main.ViewportToScreenPoint(gridPosition);
            isMoving = true;
        }

        // Grid loslassen
        if (isMoving && clearTimeStamp <=  now) {
           


             //Ob die Grab-Geste an der Kante ist
            if(rightEdgeTimeStamp >= now)
            {
      
                gridManager.ClearGrid();
                gridManager.HideUnusedCells();

            }
         
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


