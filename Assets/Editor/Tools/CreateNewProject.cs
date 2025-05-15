using UnityEditor;
using UnityEngine;

public class CreateNewProject : EditorWindow
{
    //this script is a custom build tool made for teachers to easily add new objects images etc to the AR application

    //ideas: have a temp object follow the current settings and have a preview
    //of how it will look with the scaling/position/rotation and scripts

    GameObject objList;

    GameObject Obj;
    string ObjName;

    bool canScale;

    bool HasObj;
    Vector3 posOffset = new Vector3(0f ,0f ,0f);
    Vector3 rotOffset = new Vector3(0f, 0f, 0f);

    bool HasHyperlink;
    string hyperLink;
    GameObject hyperlinkObj;
    Vector3 linkPosOffset = new Vector3(0f, 0.5f, 0f);
    Vector3 linkRotOffset = new Vector3(0f, 0f, 0f);


    bool HasImageSwap;
    string ImageFolder;
    GameObject imageSwapObj;
    Vector3 imagePosOffset = new Vector3(0f, 0.7f, 0f);
    Vector3 imageRotOffset = new Vector3(0f, 0f, 0f);

    Vector2 scrollPos;

    private void OnEnable()
    {
        //set default objects for game object fields
        imageSwapObj = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/DefaultImageObj.prefab", typeof(GameObject));
        hyperlinkObj = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/DefaultLinkObj.prefab", typeof(GameObject));
    }

    [MenuItem("Tools/Create New Project")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CreateNewProject));
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        GUILayout.Label("Create New Project", EditorStyles.boldLabel);
        GUILayout.Space(20);
        ObjName = EditorGUILayout.TextField(new GUIContent("Project naam *", "Naam van het project (in de hierarchy lijst)"), ObjName);

        //INPUT FIELDS FOR 3D OBJECT
        GUILayout.Space(20);
        HasObj = EditorGUILayout.Toggle(new GUIContent("3D Object", "Is er een 3d object waar iemand omheen kan lopen"), HasObj);
        if (HasObj)
        {
            Obj = EditorGUILayout.ObjectField(new GUIContent("3D Model *", "3D model van het object"), Obj, typeof(GameObject), false) as GameObject;
            canScale = EditorGUILayout.Toggle(new GUIContent("Schalen", "Kan de gebruiker met pinch het object vergroten of verkleinen"), canScale);

            //INPUT FIELDS FOR OBJECT OFFSET
            GUILayout.Space(10);
            GUILayout.Label("Offset 3D Object", EditorStyles.boldLabel);
            posOffset = EditorGUILayout.Vector3Field(new GUIContent("Position Offset", "Positie offset op de X,Y en Z as"), posOffset);
            rotOffset = EditorGUILayout.Vector3Field(new GUIContent("Rotation Offset", "Rotatie offset op de X,Y en Z as"), rotOffset);
        }

        //INPUT FIELDS FOR HYPERLINK
        GUILayout.Space(20);
        HasHyperlink = EditorGUILayout.Toggle(new GUIContent("Link", "Is er een object met een hyperlink waar de gebruiker op kan klikken (dit opened een internet tab)"), HasHyperlink);
        if (HasHyperlink)
        {
            hyperLink = EditorGUILayout.TextField(new GUIContent("Url *", "De link naar de website"), hyperLink);
            hyperlinkObj = EditorGUILayout.ObjectField(new GUIContent("Link Object *", "3D object waar de gebruiker op klikt om de link te openen"), hyperlinkObj, typeof(GameObject), false) as GameObject;
            GUILayout.Space(10);
            GUILayout.Label("Offset Hyperlink Object", EditorStyles.boldLabel);
            linkPosOffset = EditorGUILayout.Vector3Field(new GUIContent("Position Offset", "Positie offset op de X,Y en Z as"), linkPosOffset);
            linkRotOffset = EditorGUILayout.Vector3Field(new GUIContent("Rotation Offset", "Rotatie offset op de X,Y en Z as"), linkRotOffset);
        }

        //INPUT FIELDS FOR IMAGE SWAP
        GUILayout.Space(20);
        HasImageSwap = EditorGUILayout.Toggle(new GUIContent("Foto", "Een 3d object met een foto (bij meerdere fotos is er een optie om er met pijltjes doorheel te scrollen)"), HasImageSwap);
        if (HasImageSwap)
        {
            ImageFolder = EditorGUILayout.TextField(new GUIContent("Folder *", "naam van de folder met fotos er in (mapje moet in de Resources folder zitten)"), ImageFolder);
            imageSwapObj = EditorGUILayout.ObjectField(new GUIContent("Foto Object *", "3D object waar de fotos op worden laten zien"), imageSwapObj, typeof(GameObject), false) as GameObject;
            GUILayout.Space(10);
            GUILayout.Label("Offset Image Object", EditorStyles.boldLabel);
            imagePosOffset = EditorGUILayout.Vector3Field(new GUIContent("Position Offset", "Positie offset op de X,Y en Z as"), imagePosOffset);
            imageRotOffset = EditorGUILayout.Vector3Field(new GUIContent("Rotation Offset", "rotatie offset op de X,Y en Z as"), imageRotOffset);
        }

        EditorGUILayout.EndScrollView();

        GUILayout.Label("* verplict veld", EditorStyles.boldLabel);
        //check if the right things are filled in or not
        GUI.enabled = CheckValidationCreateButton();
        //BUTTON TO SPAWN THE FINISHED OBJECT AFTER ALL SETTINGS HAVE BEEN MADE
        if (GUILayout.Button(new GUIContent("Create Project", "Maak het project aan met de gegeven instellingen"), GUILayout.Height(50)))
        {
            SpawnObject();
        }
    }
        

    private void SpawnObject()
    {
        //spawn empty gameobject
        objList = GameObject.FindGameObjectWithTag("Objects");
        GameObject emptyObj = new GameObject(ObjName);
        emptyObj.tag = "Project";
        emptyObj.transform.parent = objList.transform;

        if (HasObj)
        {
            //spawn objects (model put in by the user)
            GameObject gameObj = Instantiate(Obj);
            gameObj.transform.parent = emptyObj.transform;
            gameObj.transform.position += posOffset;
            gameObj.transform.rotation *= Quaternion.Euler(rotOffset);

            //add scale object script if bool = true
            if (canScale)
            {
                gameObj.AddComponent<PinchToScale>();
            }
        }


        //HYPERLINK == TRUE SPAWN HYPERLINK OBJECT
        if (HasHyperlink)
        {
            GameObject linkObj = Instantiate(hyperlinkObj);

            linkObj.transform.parent = emptyObj.transform;

            linkObj.transform.position += linkPosOffset;
            linkObj.transform.rotation *= Quaternion.Euler(linkRotOffset);
            //check if that object has a hyperlink script -> if no add one
            if (!linkObj.GetComponent<Hyperlink>())
            {
                linkObj.AddComponent<Hyperlink>();
            }
            //pass the link from this tool into the hyperlink script on the object
            linkObj.GetComponent<Hyperlink>().Link = hyperLink;
        }

        //IMAGE SWAP == TRUE SPAWN IMAGE SWAP OBJECT
        if (HasImageSwap)
        {
            GameObject imageObj = Instantiate(imageSwapObj);
            imageObj.transform.parent = emptyObj.transform;
            imageObj.transform.position += imagePosOffset;
            imageObj.transform.rotation *= Quaternion.Euler(imageRotOffset);
            //check if that object has a swapimage script -> if no add one
            if (!imageObj.GetComponent<SwapImages>())
            {
                imageObj.AddComponent<SwapImages>();
            }
            //pass the link from this tool into the swapimage script on the object
            imageObj.GetComponent<SwapImages>().folderName = ImageFolder;
        }     
    }

    void AutoOffsetOnMultipleObjects()
    {
        //this function sets the offset automaticaly to the side when multiple objects get selected
        if (HasObj || HasHyperlink) { imagePosOffset = new Vector3(1.3f, 0.7f, 0f); } else imagePosOffset = new Vector3(0f, 0.7f, 0f);
        if (HasObj || HasImageSwap) { linkPosOffset = new Vector3(-1.3f, 0.5f, 0f); } else linkPosOffset = new Vector3(0f, 0.5f, 0f);
    }

    private bool CheckValidationCreateButton()
    {
        //ONE BIG CHECK TO SEE IF EVERYTHING THAT NEEDS TO BE FILLED IN IS FILLED IN IF ANY OF THESE ARE FALLS IT STOPS THE USER FROM CREATING THE OBJECT
        if (
            ObjName == string.Empty ||
            //hyperlink checks
            (HasHyperlink && hyperLink == string.Empty) ||
            (HasHyperlink && hyperlinkObj == null) ||
            //3d obj checks
            (HasObj && Obj == null) ||
            //image swap checks
            (HasImageSwap && ImageFolder == string.Empty) ||
            (HasImageSwap && imageSwapObj == null) ||
            //atleast one option should be selected
            !(HasImageSwap || HasObj || HasHyperlink)
            )
            return false;
        else return true;
    }
}
