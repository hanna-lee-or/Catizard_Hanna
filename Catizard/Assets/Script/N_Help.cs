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
            for (int i = 0; i < 7; i++)
        {
            helps[i].gameObject.SetActive(false);
        }
        helps[Page].gameObject.SetActive(true);

    }

    public void PageUp()
    {
        if (Page == 6)
        {
            return;
        }
        else if (Page < 6)
        {
            helps[Page].gameObject.SetActive(false);
            Page++;
            helps[Page].gameObject.SetActive(true);
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
            helps[Page].gameObject.SetActive(true);
        }
    }
}
