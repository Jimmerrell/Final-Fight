using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UI_CardBehavior : MonoBehaviour
{
    GameManager GameManager_Reference;

    // The amount our card will move when highlighted
    public float highlightPositionOffset = 5;

    // The position our card will move to when highlighted
    Vector3 highlightPosition;

    // The original position of our card
    Vector3 originalPosition;

    // The outline around our card (Not appropriate, looking to use edge-detection for proper outline)
    Outline cardOutline;

    // Condition that our card has been dropped in a valid area to activate it
    bool cardToBePlayed = false;

    // Our card's movement velocity
    //Vector3 vel = Vector3.zero;

    // Our card's index in the hand
    int index;

    //UI_HandBehavior UI_HandController;

    // Use this for initialization
    void Start()
    {
        GameManager_Reference = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        highlightPosition = transform.position;
        highlightPosition.y += highlightPositionOffset;
        highlightPosition.z = -99;

        originalPosition = transform.position;
        cardOutline = GetComponent<Outline>();
        //UI_HandController = GameObject.Find("HUD").GetComponent<UI_HandBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HighlightCard()
    {
        if (bIsOpponentCard())
            return;

        // Check if our card has just been played
        // (We don't want to modify its position if it's being used)
        if (!cardToBePlayed)
        {
            // Set the card's position to its highlighted position
            transform.position = highlightPosition;
        }

        // If we have an outline, enable it
        if (cardOutline)
        {
            Color effectColor = cardOutline.effectColor;
            effectColor.a = 255;
            cardOutline.effectColor = effectColor;
        }
    }

    public void UnHighlightCard()
    {
        if (bIsOpponentCard())
            return;

        // Check if our card has just been played
        if (!cardToBePlayed)
        {
            // Return us to our original position on the screen
            transform.position = originalPosition;
        }

        // If we have an outline, remove it
        if (cardOutline)
        {
            Color effectColor = cardOutline.effectColor;
            effectColor.a = 0;
            cardOutline.effectColor = effectColor;
        }

    }

    public void DragCard()
    {
        if (bIsOpponentCard())
            return;

        // While we mouse is dragging this UI element, set its position to the mouse position
        Vector3 mousePos = Input.mousePosition;
        transform.position = new Vector3(mousePos.x, mousePos.y, -99);
    }

    public void ReleaseCard()
    {
        if (bIsOpponentCard())
            return;

        // When we release the mouse button, check if our card is in a valid area to activate
        if (transform.position.y < Screen.height / 4)
        {
            // If we are still on the lower fourth of the screen, return to our hand
            transform.position = originalPosition;

            // Make sure our card has not been set to be played
            cardToBePlayed = false;
        }
        else
        {
            // If we are in the upper three-fourths of the screen, play the card
            cardToBePlayed = true;
            //UI_HandController.RemoveCard(index);
        }
    }

    // Used to update card such as when this card's position in the hand is changed
    public void UpdateCard(Vector3 handPos, int numCards)
    {
        float cardwidth = GetComponent<RectTransform>().rect.width;

        Vector3 newPos;

        //if (index % 2 == 0)
        //{
        //    newPos.x = handPos.x - index * (100 - (numCards * 2));
        //}
        //else
        //{
        //    newPos.x = handPos.x + index * (100 - (numCards * 2));
        //}

        newPos.x = (handPos.x - index * (cardwidth * 0.58f - (numCards * 2))) + cardwidth * 0.6f;
        
        newPos.y = handPos.y;
        newPos.z = index; //5-0 = 5, 5-1 = 4, 5-5 = 0

        transform.position = newPos;

        highlightPosition = transform.position;
        highlightPosition.y += highlightPositionOffset;
        highlightPosition.z = -99;

        originalPosition = transform.position;
    }

    public void SetIndex(int _index)
    {
        index = _index;
    }

    public int GetIndex()
    {
        return index;
    }


    bool bIsOpponentCard()
    {
        if (GameManager_Reference.GetTurn() == 1)
            if (transform.parent.name.ToString() == "Hand_Panel_Opponent")
                return true;
        else if (GameManager_Reference.GetTurn() == 2)
            if (transform.parent.name.ToString() == "Hand_Panel_Player")
                return true;

        return false;
    }
}
