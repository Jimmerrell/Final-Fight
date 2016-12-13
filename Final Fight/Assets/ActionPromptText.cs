using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionPromptText : MonoBehaviour 
{

    public GameObject attackPrompt;
    //public GameObject rebuttalPrompt;
    public Text text;

	// Use this for initialization
	void Start () 
    {
        text = attackPrompt.GetComponent<Text>();
        //text = attackPrompt.GetComponent<TextAreaAttribute>().ToString();
        //Debug.Log("text = ");
        //Debug.Log(text.text);
	}
	
	// Update is called once per frame
	void Update () 
    {
        text.text = "Changes!";
        text.color = Color.red;
	}

    public void SetText (string str)
    {
        text.text = str;
    }
}
