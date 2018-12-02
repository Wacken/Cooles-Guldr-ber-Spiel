using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingColliderTrigger : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Death.instance.deathEnding();
    }
}
