using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour
{

    public GameObject itselfGameObject;

    public void PanelEntryExit()
    {
        if (MainMenuSwitch.selected1 == itselfGameObject)
        {
            MainMenuSwitch.selected1 = null;
        }
        else
        {
            MainMenuSwitch.selected1 = itselfGameObject;
        }
    }
}
