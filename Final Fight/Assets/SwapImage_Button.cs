using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwapImage_Button : MonoBehaviour
{
    Image RenderImage;

    public Sprite Image_1;
    public Sprite Image_2;


    // Use this for initialization
    void Start()
    {
        RenderImage = GetComponent<Image>();
        RenderImage.sprite = Image_1;
    }

    public void SwapImage(int image_number)
    {
        if (image_number == 1)
        {
            RenderImage.sprite = Image_2;
        }
        else
        {
            RenderImage.sprite = Image_1;
        }
    }
}
