using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemPanel : MonoBehaviour
{
    private GameObject MMM;
    private GameObject IM;
    private GameObject ILP;
    public Button prefabButton;

    void Start()
    {
        MMM = GameObject.Find("MainMenuManager");
        IM = GameObject.Find("ItemManager");
        ILP = GameObject.Find("Canvas").gameObject.transform.FindChild("MainMenuPanel").gameObject.transform.FindChild("ItemsPanel").gameObject.transform.FindChild("LayoutPanel").gameObject;
    }

    public void ShowItems()
    {
        if (!MMM.GetComponent<MainMenuSwitch>().isActivePanels2[13])
        {
            //Deletes all items before the new set of items are shown, prevents duplicates of items when item panel is turned on and off
            var children = new List<GameObject>();
            foreach (Transform child in ILP.transform)
                children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));
            //makes item buttons show up
            for (int i = 0; i < IM.GetComponent<ItemManager>().itemNames.Length; i++)
            {
                if (IM.GetComponent<ItemManager>().itemQuantities[i] > 0)
                {
                    Button button = Instantiate(prefabButton, new Vector3(0,0,0), Quaternion.identity) as Button;
                    button.transform.SetParent(ILP.transform);
                    button.transform.FindChild("Text").GetComponent<Text>().text = IM.GetComponent<ItemManager>().itemNames[i];
                    button.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }
}
