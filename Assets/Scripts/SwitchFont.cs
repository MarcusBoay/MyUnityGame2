using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwitchFont : MonoBehaviour {

    public Text zeText;

    public void SwitchFontEnter()
    {
        zeText.fontStyle = FontStyle.Italic;
    }
    
    public void SwitchFontExit()
    {
        zeText.fontStyle = FontStyle.Normal;
    }
}
