using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class UpdateIndicator : MonoBehaviour
{
    private ARRaycastManager arRaycaster;

    //object prefab
    [SerializeField]
    public GameObject placementIndicator;
    private Pose placementPose;
    public bool placementPoseIsValid;

    void Start()
    {
        arRaycaster = FindObjectOfType<ARRaycastManager>();
    }

    //## UPDATE POSITION AND ROTATION OF THE INDICATOR AND SPAWN OBJECTS ##
    public void UpdatePlacementIndicator()
    {
        //enable the indicator if there is a valid spot for the user to place a object
        //else it will disable the object
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    public void UpdatePlacementPose()
    {
        //check if there is a valid position in world space for a object to spawn
        //calculate the position of the indicator and rotate it to make it rotate with the device

        //screen center with distance of 9
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 9));
        //make a list of ar hits
        var rayHits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, rayHits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        placementPoseIsValid = rayHits.Count > 0;
        //find valid surface in world
        if (placementPoseIsValid)
        {
            placementPose = rayHits[0].pose;

            var camForward = Camera.current.transform.forward;
            var camBearing = new Vector3(camForward.x, 0, camForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(camBearing);
        }
    }
}
