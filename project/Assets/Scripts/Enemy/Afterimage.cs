using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    [SerializeField]int imageNum = 9;
    [SerializeField]float cdTime = 0.02f;
    List<Transform> afterImages = new List<Transform>();
    GameObject afterImgParent;
    //int currentImage;
    bool OpenImage;
    private void Start()
    {
        afterImgParent = new GameObject("GhostShadow");
        for (int i = 0; i < imageNum; i++)
        {
            var afterImg = new GameObject("afterimage");
            afterImg.transform.position = transform.position ;
            var spr = afterImg.AddComponent<SpriteRenderer>();
            spr.sprite = GetComponent<SpriteRenderer>().sprite;
            spr.sortingOrder = -i - 1;
            spr.color = new Color(spr.color.r,spr.color.g,spr.color.b,1-1.0f*i/imageNum);
            afterImages.Add(afterImg.transform);
            afterImg.transform.SetParent(afterImgParent.transform);
        }
        OpenImage = true;
        CloseAfterImage();
    }
    public void StartAfterImage()
    {
        afterImgParent.SetActive(true);
        for (int i = 0; i < afterImages.Count; i++)
        {
            afterImages[i].position = transform.position;
            afterImages[i].rotation = transform.rotation;
        }
        OpenImage = true;
    }
    public void ShowAfterImage()
    {
        if (afterImages[0].position != transform.position && OpenImage)
        {
            OpenImage = false;
            Invoke(nameof(MoveImage), cdTime);
        }
        if (afterImages[imageNum - 1].position == transform.position)
        {
            OpenImage = true;
        }
    }

    void MoveImage()
    {
        if (afterImages[imageNum - 1].position == transform.position) return;
        for (int i = imageNum - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                afterImages[i].position = transform.position;
                afterImages[i].rotation = transform.rotation;
                afterImages[i].GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                afterImages[i].position = afterImages[i - 1].position ;
                afterImages[i].rotation = afterImages[i - 1].rotation ;
                afterImages[i].GetComponent<SpriteRenderer>().sprite = afterImages[i - 1].GetComponent<SpriteRenderer>().sprite;
            }
        }
        Invoke(nameof(MoveImage), cdTime);
    }

    public void CloseAfterImage()
    {
        afterImgParent.SetActive(false);
    }
}
