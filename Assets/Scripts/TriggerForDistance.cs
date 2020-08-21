using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForDistance : MonoBehaviour {

    [SerializeField]
    float _radius;
    [SerializeField]
    AudioSource _source;
    bool _doTheStuff = true; 
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(Player.instance._player.transform.position, transform.position);
        if(distance < _radius && _doTheStuff)
        {
            _doTheStuff = false;
            _source.Play();
        }
        if(!_source.isPlaying && !_doTheStuff)
        {
            _source.maxDistance = 40;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
