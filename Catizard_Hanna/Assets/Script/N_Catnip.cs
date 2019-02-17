using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Catnip : MonoBehaviour
{

    public N_CardSystem NCS;

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("cat"))
        {
            NCS.isCatnipOn = true;
            gameObject.SetActive(false);
        }

        if (coll.gameObject.CompareTag("catnip"))
        {
            NCS.isCatnip = true;
        }
    }

}
