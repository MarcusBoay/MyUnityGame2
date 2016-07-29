using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxNPC : MonoBehaviour
{
    private GameObject textPanel;
    private GameObject imagePanel;

    public GameObject NPC;

    public TextAsset textFile1;
    public string[] textLines1;
    public TextAsset textFile2;
    public string[] textLines2;
    private GameObject textBoxImage;

    private GameObject SC;

    public int[] imageNumbering1;
    public int[] imageNumbering2;

    private int x = 0;
    private bool y = false;
    public static bool isActive = false;

    void Start()
    {
        if (textFile1 != null)
        {
            textLines1 = textFile1.text.Split('\n');
        }
        if (textFile2 != null)
        {
            textLines2 = textFile2.text.Split('\n');
        }
        textPanel = GameObject.Find("Canvas").transform.FindChild("TextBox").gameObject;
        imagePanel = GameObject.Find("Canvas").transform.FindChild("PlayerImagePanel").gameObject;
        SC = GameObject.Find("ImageController");
        textBoxImage = imagePanel.transform.FindChild("Image").gameObject;
    }

    void Update()
    {
        NPC.GetComponent<NPCInteract>().Interact();
        if (NPCInteract.isTalking && !MainMenuSwitch.isActive)
        {
            switch (isActive)
            {
                case true:
                    PlayerMovement.shouldUpdate = false;
                    isActive = true;
                    try
                    {
                        switch (y)
                        {
                            case false:
                                textPanel.GetComponentInChildren<Text>().text = textLines1[x];
                                textBoxImage.GetComponent<Image>().sprite = SC.GetComponent<SkinController>().skins[imageNumbering1[x]];
                                break;
                            case true:
                                textPanel.GetComponentInChildren<Text>().text = textLines2[x];
                                textBoxImage.GetComponent<Image>().sprite = SC.GetComponent<SkinController>().skins[imageNumbering2[x]];
                                break;
                        }
                            x = x + 1;
                    }
                    catch
                    {
                        if (textFile2 != null)
                        {
                            y = !y;
                        }
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
                        switch (y)
                        {
                            case false:
                                textPanel.GetComponentInChildren<Text>().text = textLines1[x];
                                textBoxImage.GetComponent<Image>().sprite = SC.GetComponent<SkinController>().skins[imageNumbering1[x]];
                                break;
                            case true:
                                textPanel.GetComponentInChildren<Text>().text = textLines2[x];
                                textBoxImage.GetComponent<Image>().sprite = SC.GetComponent<SkinController>().skins[imageNumbering2[x]];
                                break;
                        }
                        x = x + 1;
                    }
                    catch
                    {
                        if (textFile2 != null)
                        {
                            y = !y;
                        }
                        x = 0;
                        PlayerMovement.shouldUpdate = true;
                        isActive = false;
                        NPCInteract.isTalking = false;
                    }
                    break;
            }
            textPanel.SetActive(isActive);
            imagePanel.SetActive(isActive);
        }
    }
}
