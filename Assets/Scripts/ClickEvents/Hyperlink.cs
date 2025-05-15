using UnityEngine;
using System.Collections;

public class Hyperlink : MonoBehaviour
{
    [SerializeField]
    public string Link;

    //simple function to open url
    public void openUrl()
    {
        Application.OpenURL(Link);
    }
}
