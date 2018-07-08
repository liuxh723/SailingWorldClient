using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KBEngine;


public class RoleSelectManager : MonoBehaviour {

    // Use this for initialization
    public InputField inputName;
    public Text textCountry;
    public Text textSex;



    private UINT8 selCountry = 0;
    private UINT8 sex = 0;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelContry(int index)
    {
        selCountry = (UINT8)index;
        textCountry.text = "Country：" + index.ToString();
    }

    public void SelSex(int index)
    {
        sex = (UINT8)index;
        textSex.text = "Sex:"+ index.ToString();
    }

    public void RoleSet()
    {
        if (selCountry != 0 && sex != 0&& inputName.text != string.Empty)
        {
            KBEngine.Event.fireIn("CreateAvatar", selCountry, sex, inputName.text);
        }
        
        enabled = false;    
    }
}
