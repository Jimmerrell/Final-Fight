using UnityEngine;
using System.Collections;

// Used to manage the HUD elements in relation to the player

public class UI_HUDManager : MonoBehaviour
{

    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Sorts objects based on their Z position
        // Only really useful for sorting UI objects
        ObjectSort.SortChildren(gameObject);
    }
}
