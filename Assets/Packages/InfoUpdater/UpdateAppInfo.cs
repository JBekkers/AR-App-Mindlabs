using UnityEngine;
using TMPro;

public class UpdateAppInfo : MonoBehaviour
{
    //this script is made to automate the app name and version on the bottom of the information screen
    //this also updates the creator credits. please do not edit this script in any way thanks - jelle
    private void Awake()
    {
        UpdateAppInformation();
    }

    private void UpdateAppInformation()
    {
        TextMeshProUGUI creditsTxt = GameObject.FindGameObjectWithTag("Credits").GetComponent<TextMeshProUGUI>();
        creditsTxt.text = "App made by:\nJelle Bekkers";
        TextMeshProUGUI appInfoTxt = GameObject.FindGameObjectWithTag("AppInfo").GetComponent<TextMeshProUGUI>();
        appInfoTxt.text = Application.productName.ToString() + " - Version: " + Application.version.ToString();
    }
}
