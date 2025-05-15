using UnityEngine;

public class PinchToScale : MonoBehaviour
{
    private GameObject gameObj;
    private Vector3 OrigionalScale;
    private Vector3 StartScale;

    private Vector3 maxScale;
    private float maxScaleNum = 3.5f;

    private Vector3 minScale;
    private float minScaleNum = 0.5f;

    private float initialDistance;
    void Start()
    {
        //set object to the object this script is on
        gameObj = gameObject;
        OrigionalScale = gameObj.transform.localScale;

        maxScale = new Vector3(OrigionalScale.x + maxScaleNum , OrigionalScale.y + maxScaleNum, OrigionalScale.z + maxScaleNum);
        minScale = new Vector3(OrigionalScale.x - minScaleNum, OrigionalScale.y - minScaleNum, OrigionalScale.z - minScaleNum);
    }

    private void OnEnable()
    {
        //every time the script is enabled set scale back to normal;
        resetScale();
    }

    void Update()
    {
        PinchToScaleObject();
    }

    private void PinchToScaleObject()
    {
        //if 2 fingers are on the screen
        if (Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            //if you release fingers from screen cancel
            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled
                || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                return; //if one of the inputs are canceled do nothing;
            }

            //get distance between fingers
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                StartScale = gameObj.transform.localScale;
            }
            else
            {
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                if(Mathf.Approximately(initialDistance, 0))
                {
                    return; //to small of a distance ignore it
                }

                var factor = currentDistance / initialDistance;
                Vector3 scaled = StartScale * factor;

                //clamp the size 
                scaled.x = Mathf.Clamp(scaled.x, minScale.x, maxScale.x);
                scaled.y = Mathf.Clamp(scaled.y, minScale.y, maxScale.y);
                scaled.z = Mathf.Clamp(scaled.z, minScale.z, maxScale.z);

                //apply scale to object
                gameObj.transform.localScale = scaled;
            }
        }
    }

    private void resetScale()
    {
        gameObj.transform.localScale = OrigionalScale;
    }
}
