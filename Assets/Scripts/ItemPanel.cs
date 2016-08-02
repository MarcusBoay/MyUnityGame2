using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    private GameObject MMM;
    private GameObject IM;
    private GameObject ILP;
    public Text prefabText;

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
            foreach (GameObject text in ILP.transform) //wtf is going on here? HALP
            {
                Destroy(text);
            }
            for (int i = 0; i < IM.GetComponent<ItemManager>().itemNames.Length; i++)
            {
                if (IM.GetComponent<ItemManager>().itemQuantities[i] > 0)
                {
                    Text text = Instantiate(prefabText, new Vector3(0, 0, 0), Quaternion.identity) as Text;
                    text.transform.SetParent(ILP.transform);
                    text.text = IM.GetComponent<ItemManager>().itemNames[i];
                }
            }
        }
    }
}
