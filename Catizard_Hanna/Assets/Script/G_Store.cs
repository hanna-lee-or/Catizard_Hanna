using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class G_Store : MonoBehaviour, IPointerDownHandler
{

    public static int StoreNum;
    public GameObject StoreScreen;
    public Sprite[] Store1, Store2, Store3;

    private Image StoreImage;
    private int index = 0;

    void Awake()
    {
        StoreImage = GetComponent<Image>();
    }

    // OnEnable is called when it is enable
    void OnEnable()
    {
        index = -1;
        switch (StoreNum)
        {
            case 1:
                StoreImage.sprite = Store1[0];
                break;
            case 2:
                StoreImage.sprite = Store2[0];
                break;
            case 3:
                StoreImage.sprite = Store3[0];
                break;
        }
    }

    // 마우스가 눌릴 때
    public void OnPointerDown(PointerEventData data)
    {
        switch (StoreNum)
        {
            case 1:
                QuitStore();
                break;
            case 2:
                QuitStore();
                break;
            case 3:
                QuitStore();
                break;
        }
    }

    // 스토리 끝냄
    public void QuitStore()
    {
        StoreScreen.SetActive(false);
    }

}
