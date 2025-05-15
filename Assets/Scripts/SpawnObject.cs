using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SpawnObject : MonoBehaviour
{
    public ARPlaneManager PlaneManager;

    public GameObject obj;
    private UpdateIndicator UpdateIndicator;
    public float offset = 0.25f;
    public MenuManager menuMan;

    private bool IsPlacing = true;
    private bool isReset = false;

    private void Start()
    {
        UpdateIndicator = FindObjectOfType<UpdateIndicator>();
        menuMan = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MenuManager>();

        obj = GameObject.FindGameObjectWithTag("Project");
        obj.SetActive(false);
    }

    void Update()
    {
        //if the user presses the screen place the object
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && UpdateIndicator.placementPoseIsValid && IsPlacing && !menuMan.IsOpen)
        {
            SpawnObjects();
        }

        //if the user hasnt placed the object yet (or when reset and then hasnt placed it yet) update indicator position
        if(IsPlacing)
        {
            UpdateIndicator.UpdatePlacementIndicator();
            UpdateIndicator.UpdatePlacementPose();
        }
        else if(!IsPlacing && !isReset)
        {
            //if object is placed disable plane generation and disable all the existing planes
            SetAllPlanesActive(false);
            isReset = true;
        }
    }

    public void SpawnObjects()
    {
        IsPlacing = false;
        isReset = false;

        //set the object pos to where the indicator position is
        Vector3 offsetPosition = new Vector3(UpdateIndicator.placementIndicator.transform.position.x, UpdateIndicator.placementIndicator.transform.position.y + offset, UpdateIndicator.placementIndicator.transform.position.z);
        obj.transform.SetPositionAndRotation(offsetPosition, UpdateIndicator.placementIndicator.transform.rotation);
        obj.SetActive(true);
        UpdateIndicator.placementIndicator.SetActive(false);
    }

    public void ResetObjects()
    {
        //if reset button is pressed reset the bool that allowes the user to place the object again
        if (!IsPlacing && isReset == true)
        {
            SetAllPlanesActive(true);

            Debug.Log("Reset objects");
            IsPlacing = true;
            obj.SetActive(false);

            isReset = false;
        }
    }

    //set the planes to active or not active depending on the input given

    void SetAllPlanesActive(bool value)
    {
        foreach (var plane in PlaneManager.trackables)
            plane.gameObject.SetActive(value);

        PlaneManager.enabled = value;
    }
}
