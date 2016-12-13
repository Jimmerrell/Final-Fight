using UnityEngine;
using System.Collections;

public class PassTheDeviceOverlay : MonoBehaviour 
{
    public Vector2 centerScreenPosition;

    public GameObject readyButton;
    public GameObject gameManager;

	// Use this for initialization
	void Start () 
    {
	}

    public void Reveal(bool bReveal)
    {
        int playerturn = gameManager.GetComponent<GameManager>().nCurrentTurn;

        if(bReveal)
        {
            transform.localPosition = centerScreenPosition;
            readyButton.GetComponent<SwapImage_Button>().SwapImage(playerturn);
        }
        else
        {
            transform.Translate(0.0f, 10000.0f, 0.0f);
        }
    }
}
