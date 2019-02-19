using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class G_StoreNum : MonoBehaviour, IPointerDownHandler
{

    public int StoreNum;
    public GameObject StoreScreen;

    // 마우스가 눌릴 때
    public void OnPointerDown(PointerEventData data)
    {
        G_Store.StoreNum = StoreNum;
        StoreScreen.SetActive(true);
    }

}
