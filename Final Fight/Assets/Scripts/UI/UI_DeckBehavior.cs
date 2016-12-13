using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_DeckBehavior : MonoBehaviour
{
    Outline deckOutline;
    public Text cardsRemainingPopup;

    // Use this for initialization
    void Start()
    {
        deckOutline = GetComponent<Outline>();
        HideRemainingCards();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HighlightDeck()
    {
        if (deckOutline)
        {
            Color effectColor = deckOutline.effectColor;
            effectColor.a = 255;
            deckOutline.effectColor = effectColor;
        }
    }

    public void UnHighlightDeck()
    {
        if (deckOutline)
        {
            Color effectColor = deckOutline.effectColor;
            effectColor.a = 0;
            deckOutline.effectColor = effectColor;
        }
    }

    public void PeekRemainingCards()
    {
        //cardsRemainingPopup.gameObject.SetActive(true);
    }

    public void HideRemainingCards()
    {
       // cardsRemainingPopup.gameObject.SetActive(false);
    }
}
