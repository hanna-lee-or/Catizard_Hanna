using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class N_Help : MonoBehaviour
{
    public GameObject UpButton, DownButton;
    public Image[] helps;
    public int Page = 0;

    // Start is called before the first frame update
    void Start()
    {
            for (int i = 0; i < 16; i++)
        {
            helps[i].gameObject.SetActive(false);
        }
        helps[Page].gameObject.SetActive(true);
        UpButton.gameObject.SetActive(true);
        DownButton.gameObject.SetActive(false);

    }

    public void PageUp()
    {

        if (Page == 15)
        {
            return;
        }
        else if (Page < 15)
        {
            helps[Page].gameObject.SetActive(false);
            Page++;
            if (Page == 15) { UpButton.gameObject.SetActive(false); }
            helps[Page].gameObject.SetActive(true);
            if (Page != 0)
            {
                DownButton.gameObject.SetActive(true);
            }
        }

    }

    public void PageDown()
    {
        if (Page == 0)
        {
            return;
        }
        else if (Page > 0)
        {
            helps[Page].gameObject.SetActive(false);
            Page--;
            if (Page == 0) { DownButton.gameObject.SetActive(false); }
            helps[Page].gameObject.SetActive(true);
            if (Page != 15)
            {
                UpButton.gameObject.SetActive(true);
            }
        }
    }
}
