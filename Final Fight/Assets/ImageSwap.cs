using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageSwap : MonoBehaviour 
{
    Image actionPromptImage;

    public Sprite AttackImage;
    public Sprite RebuttalImage;

	// Use this for initialization
	void Start () 
    {
        actionPromptImage = GetComponent<Image>();
        SetImageToAttack();
        actionPromptImage.color = Color.white;
	}
	
    public void SetImageToAttack()
    {
        actionPromptImage.sprite = AttackImage;
    }

    public void SetImageToRebuttal()
    {
        actionPromptImage.sprite = RebuttalImage;
    }
}
