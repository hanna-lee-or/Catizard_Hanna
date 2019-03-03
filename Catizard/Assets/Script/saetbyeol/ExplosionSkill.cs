using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSkill : MonoBehaviour
{

    public void OnTriggerStay2D(Collider2D coll)
    {
        if (N_CardSystem.isBoom)
        {
            if (coll.gameObject.CompareTag("catnip")) // 캣닢 충돌 감지
            {
                coll.gameObject.SetActive(false);
            }

            if (coll.gameObject.CompareTag("scrow")) // 허수아비 충돌 감지
            {
                coll.gameObject.SetActive(false);
            }
        }
    }

}
