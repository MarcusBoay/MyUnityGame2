using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxObj : MonoBehaviour
{

    private GameObject textPanel;

    public GameObject obj;

    public TextAsset textFile;
    public string[] textLines;

    private int x = 0;
    public static bool isActive = false;

    void Start()
    {
        textPanel = GameObject.Find("Canvas").transform.FindChild("TextBoxFull").gameObject;
        if (textFile != null)
        {
            textLines = textFile.text.Split('\n');
        }
    }

    void Update()
    {
        obj.GetComponent<NPCInteract>().Interact();
        if (NPCInteract.isTalking && !MainMenuSwitch.isActive)
        {
            switch (isActive)
            {
                case true:
                    PlayerMovement.shouldUpdate = false;
                    isActive = true;
                    try
                    {
                        textPanel.GetComponentInChildren<Text>().text = textLines[x];
                        x = x + 1;
                    }
                    catch
                    {
                        x = 0;
                        PlayerMovement.shouldUpdate = true;
                        isActive = false;
                        NPCInteract.isTalking = false;
                    }
                    break;
                case false:
                    PlayerMovement.shouldUpdate = false;
                    isActive = true;
                    try
                    {
                        textPanel.GetComponentInChildren<Text>().text = textLines[x];
                        x = x + 1;
                    }
                    catch
                    {
                        x = 0;
                        PlayerMovement.shouldUpdate = true;
                        isActive = false;
                        NPCInteract.isTalking = false;
                    }
                    break;
            }
            textPanel.SetActive(isActive);
        }
    }
}
