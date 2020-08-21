using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingColliderTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider collision)
    {
        Death.instance.goodEnding();
    }
}
