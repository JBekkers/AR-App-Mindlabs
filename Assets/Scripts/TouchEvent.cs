using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchEvent : MonoBehaviour
{
    EventSystem eventSystem;

    private void Start()
    {
        eventSystem = EventSystem.current;
    }
    void Update()
    {
        //EventSystemManager.currentSystem.IsPointerOverEventSystemObject() //\\ may be usefull to stop object from placing when pressing ui elements
        //if finger touches screen
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            RaycastHit[] hits;

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            hits = Physics.RaycastAll(ray, 100.0F);

            for (int i = 0; i < hits.Length; i++)
            {
                GameObject hit = hits[i].transform.gameObject;
                //Debug.Log("hit is = " + hit);

                //if hit is tag hyperlink => open link function
                switch (hit.tag)
                {
                    case "HyperLink":
                        hit.transform.gameObject.GetComponent<Hyperlink>().openUrl();
                        break;
                    default:
                        Debug.LogError("SwitchCase exception: output was unexpected: " + hit);
                        break;
                }
            }
        }
    }
}
