using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool IsOpen = false;
    private Animator anim;

    public GameObject InfoMenu;
    private bool IsInfoOpen = false;

    public GameObject SettingsMenu;
    private bool isSettingsOpen;

    public GameObject QuitMenu;
    private bool isQuitConfirmOpen;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (IsOpen)
        {
            DisableOpenMenus();
            anim.Play("MenuAnimClose");
            IsOpen = false;
        }
        else if (!IsOpen)
        {
            anim.Play("MenuAnimOpen");
            IsOpen = true;
        }
    }

    public void ToggleAppinfo()
    {
        if (IsInfoOpen)
        {
            InfoMenu.SetActive(false);
            IsInfoOpen = false;
        }
        else if (!IsInfoOpen)
        {
            DisableOpenMenus();
            InfoMenu.SetActive(true);
            IsInfoOpen = true;
        }
    }

    public void ToggleSettings()
    {
        if (isSettingsOpen)
        {
            SettingsMenu.SetActive(false);
            isSettingsOpen = false;
        }
        else if (!isSettingsOpen)
        {
            DisableOpenMenus();
            SettingsMenu.SetActive(true); ;
            isSettingsOpen = true;
        }
    }

    public void ToggleQuitConfirm()
    {
        if (isQuitConfirmOpen)
        {
            QuitMenu.SetActive(false);
            isQuitConfirmOpen = false;
        }
        else if (!isQuitConfirmOpen)
        {
            DisableOpenMenus();
            QuitMenu.SetActive(true); ;
            isQuitConfirmOpen = true;
        }
    }

    void DisableOpenMenus()
    {
        if (IsInfoOpen)
        {
            ToggleAppinfo();
            return;
        }

        if (isSettingsOpen)
        {
            ToggleSettings();
            return;
        }

        if (isQuitConfirmOpen)
        {
            ToggleQuitConfirm();
            return;
        }
    }

    public void QuitApplication()
    {
        Debug.Log("Quit Application");
        Application.Quit();
    }
}
