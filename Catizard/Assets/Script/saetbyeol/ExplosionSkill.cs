using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSkill : MonoBehaviour
{
    public string coll_tag;

    public void OnTriggerStay2D(Collider2D coll)
    {
        coll_tag = coll.gameObject.tag;
        if (N_CardSystem.isBoom)
        {
            if (coll_tag == "catnip") // 캣닢 충돌 감지
            {
                coll.gameObject.SetActive(false);
            }

            if (coll_tag == "scrow") // 허수아비 충돌 감지
            {
                coll.gameObject.SetActive(false);
            }
        }
    }

}
