using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// Used to manage the UI cards currently in the hand

public class UI_HandBehavior : MonoBehaviour
{
    ArrayList cards = new ArrayList();

    // Prefab of the UI card
    public GameObject UI_Card;

    // The object that represents our deck in the UI
    public GameObject deckObject;

    Vector3 handPosition;

    // Use this for initialization
    void Start()
    {
        GameObject handPositionObject = GameObject.Find("HandPosition");
        handPosition = handPositionObject.transform.position;
        Destroy(handPositionObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddCard();
        }
    }

    public void AddCard()
    {
        GameObject card = Instantiate(UI_Card) as GameObject;
        card.transform.SetParent(transform, false);
        card.transform.position = deckObject.transform.position;
        card.GetComponent<UI_CardBehavior>().SetIndex(cards.Count);
        cards.Add(card);
        UpdateCards();
    }

    public void RemoveCard(int cardIndex)
    {
        GameObject card = cards[cardIndex] as GameObject;
        Destroy(card);
        cards.RemoveAt(cardIndex);
        UpdateCards();
    }

    void UpdateCards()
    {
        for (int i = 0; i < cards.Count; i++ )
        {
            GameObject card = cards[i] as GameObject;
            card.GetComponent<UI_CardBehavior>().SetIndex(i);
            card.GetComponent<UI_CardBehavior>().UpdateCard(handPosition, cards.Count);
        }
    }
}
