using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class MainMenuSwitch : MonoBehaviour
{
    //DO NOT MODIFY NUMBERING OF BUTTONS, PANELS, ETC.
    public static bool isActive = false;
    private GameObject mainMenuPanel;
    public GameObject[] buttons;
    public bool[] isActiveButtons;
    public static GameObject selected1;
    //for title and description panels
    public GameObject[] panels1;
    public bool[] isActivePanels1;
    //for items, skills, etc panels
    public GameObject[] panels2;
    public bool[] isActivePanels2;
    //for texts of title and description
    //something

    void Start()
    {
        mainMenuPanel = GameObject.Find("Canvas").transform.FindChild("MainMenuPanel").gameObject;
    }

    void Update()
    {
        //for opening and closing menu
        if (Input.GetKeyDown(KeyCode.M) && !TextBoxNPC.isActive && !TextBoxObj.isActive)
        {
            PlayerMovement.shouldUpdate = !PlayerMovement.shouldUpdate;
            isActive = !isActive;
            mainMenuPanel.SetActive(isActive);
        }
        else if (TextBoxNPC.isActive || TextBoxObj.isActive)
        {
            PlayerMovement.shouldUpdate = false;
            isActive = false;
            mainMenuPanel.SetActive(isActive);
        }
        DefaultMainMenu();

        ManageButtons();

        ManageTitlePanel();

        ActivateTitlePanels();

        ActivateItemsPanels();

        ActivateSkillsPanels();

        ActivateEquipsPanels();

        ActivateStatsPanels();

        ActivateUltPanels();

        ActivateSwitchPanels();

        ActivateOptionsPanels();

        ActivateOptionsPanels();
    }
    //for managing button presses and UI
    void ManageButtons()
    {
        if (isActive)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (selected1 == buttons[i])
                {
                    isActiveButtons[i] = true;
                }
                else
                {
                    isActiveButtons[i] = false;
                }
            }
        }
    }
    //set main menu to default settings if it is closed
    void DefaultMainMenu()
    {
        if (!isActive)
        {
            selected1 = null;
            for (int i = 0; i < isActiveButtons.Length; i++)
            {
                isActiveButtons[i] = false;
            }
            for (int i = 0; i < 2; i++)
            {
                panels1[i].SetActive(true);
            }
            for (int i = 3; i < 4; i++)
            {
                panels1[i].SetActive(false);
            }
            for (int i = 0; i < 17; i++)
            {
                panels2[i].SetActive(false);
            }
        }
    }
    //to manage title and description buttons
    void ManageTitlePanel()
    {
        if (isActive)
        {
            if (isActiveButtons[0] || isActiveButtons[1] || isActiveButtons[2] || isActiveButtons[3] || isActiveButtons[4] || isActiveButtons[5] || isActiveButtons[6] || isActiveButtons[7] || isActiveButtons[8] || isActiveButtons[9])
            {
                isActivePanels1[0] = false;
                isActivePanels1[1] = false;
                isActivePanels1[2] = false;
                isActivePanels1[3] = true;
                isActivePanels1[4] = true;
            }
            else
            {
                isActivePanels1[0] = true;
                isActivePanels1[1] = true;
                isActivePanels1[2] = true;
                isActivePanels1[3] = false;
                isActivePanels1[4] = false;
            }
        }
    }

    void ActivateTitlePanels()
    {
        for (int i = 0; i < isActivePanels1.Length; i++)
        {
            panels1[i].SetActive(isActivePanels1[i]);
        }
    }

    void ActivateItemsPanels()
    {
        if (isActive)
        {
            panels2[13].SetActive(isActiveButtons[0]);
            panels2[14].SetActive(isActiveButtons[0]);
            isActivePanels2[13] = isActiveButtons[0];
            isActivePanels2[14] = isActiveButtons[0];
        }
    }

    void ActivateSkillsPanels()
    {
        if (isActive)
        {
            panels2[0].SetActive(isActiveButtons[2]);
            isActivePanels2[0] = isActiveButtons[2];
        }
    }

    void ActivateEquipsPanels()
    {
        if (isActive)
        {
            panels2[10].SetActive(isActiveButtons[1]);
            panels2[11].SetActive(isActiveButtons[1]);
            panels2[12].SetActive(isActiveButtons[1]);
            isActivePanels2[10] = isActiveButtons[1];
            isActivePanels2[11] = isActiveButtons[1];
            isActivePanels2[12] = isActiveButtons[1];
        }
    }

    void ActivateStatsPanels()
    {
        if (isActive)
        {
            //isActiveButtonss[3]
        }
    }

    void ActivateUltPanels()
    {
        if (isActive)
        {
            panels2[5].SetActive(isActiveButtons[4]);
            isActivePanels2[5] = isActiveButtons[4];
        }
    }

    void ActivateSwitchPanels()
    {
        if (isActive)
        {
            panels2[15].SetActive(isActiveButtons[7]);
            panels2[17].SetActive(isActiveButtons[7]);
            isActivePanels2[15] = isActiveButtons[7];
            isActivePanels2[17] = isActiveButtons[7];
        }
    }

    void ActivateOptionsPanels()
    {
        if (isActive)
        {
            panels2[16].SetActive(isActiveButtons[9]);
            isActivePanels2[16] = isActiveButtons[9];
        }
    }
}
