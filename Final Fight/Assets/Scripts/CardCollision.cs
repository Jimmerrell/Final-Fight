using UnityEngine;
using System.Collections;

public class CardCollision : MonoBehaviour
{
    public bool bIsInPlayPanel = false;

    GameObject game_manager;

    // Use this for initialization
    void Start()
    {
        game_manager = GameObject.Find("Game_Manager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (game_manager.GetComponent<GameManager>().nCurrentTurn == 1)
        {
            if (otherCollider.gameObject.name == "Play_Panel_Player")
                bIsInPlayPanel = true;
        }
        else if (game_manager.GetComponent<GameManager>().nCurrentTurn == 2)
        {
            if (otherCollider.gameObject.name == "Play_Panel_Opponent")
                bIsInPlayPanel = true;
        }
    }

    void OnCollisionExit2D(Collision2D otherCollider)
    {
        if (game_manager.GetComponent<GameManager>().nCurrentTurn == 1)
        {
            if (otherCollider.gameObject.name == "Play_Panel_Player")
                bIsInPlayPanel = false;
        }
        else if (game_manager.GetComponent<GameManager>().nCurrentTurn == 2)
        {
            if (otherCollider.gameObject.name == "Play_Panel_Opponent")
                bIsInPlayPanel = false;
        }
    }
}
