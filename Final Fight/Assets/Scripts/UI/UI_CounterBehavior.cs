using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_CounterBehavior : MonoBehaviour
{
    // The UI element for our counter's value
    //Text counterValueUI;
    // The actual value stored in our counter (defaults to 0)
    //int counterValue = 0;
    // The initial string for our text element (Set in the inspector)
    // NOTE: Do not leave default numbers in your text UI
    //string initialString;

    // Use this for initialization
    void Start()
    {
        // Gets the text element from our object's child
        //counterValueUI = GetComponentInChildren<Text>();

        //initialString = counterValueUI.text;

        //TODO: Remove debug demonstration and link it to the player's actual information
        //counterValue = Random.Range(0, 999);
    }

    // Update is called once per frame
    void Update()
    {
        // Sets our counter's UI to the stored value
        // counterValueUI.text = initialString + counterValue;
    }
}
