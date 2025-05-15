using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SwapImages : MonoBehaviour
{
    //## SWAP BETWEEN IMAGES IN A CANVAS ##
    [Space(10)]
    public Sprite[] sprites;
    public Image uiImage;
    private int index = 0;

    private GameObject LButton;
    private GameObject RButton;

    public string folderName;

    private void Start()
    {
        uiImage = gameObject.GetComponentInChildren<Image>();
        uiImage.preserveAspect = true;

        LButton = GameObject.FindGameObjectWithTag("LButton");
        RButton = GameObject.FindGameObjectWithTag("RButton");

        GetImagesInFolder();
    }

    private void Update()
    {
        DisableButtons();
    }

    public void NextPicture ()
    {
        if (index < sprites.Length -1)
        {
            index++;
            uiImage.sprite = sprites[index];
        }
    }

    public void PreviousPicture()
    {
        if (index > 0)
        {
            index--;
            uiImage.sprite = sprites[index];
        }
    }

    private void GetImagesInFolder()
    {
        //check if file directory is valid
        if (!string.IsNullOrEmpty(folderName))
        {
            Sprite test = Resources.Load(folderName, typeof(Sprite)) as Sprite;

            if (test == null)
            {
                sprites = Resources.LoadAll<Sprite>(folderName);
                uiImage.sprite = sprites[index];
            }
            else Debug.LogError("The given directory name doesnt exists: Resources/" + folderName + " or is empty");
        }
        else Debug.LogError("The given folder name is empty");
    }

    private void DisableButtons()
    {
        //if the index is at the max value disable right button
        if (index == sprites.Length - 1)
        {
            RButton.SetActive(false);
        }
        else RButton.SetActive(true);

        //if the index is at the min value disable the left button
        if (index == 0)
        {
            LButton.SetActive(false);
        }
        else LButton.SetActive(true);
    }
}
