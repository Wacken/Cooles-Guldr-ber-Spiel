using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToShader : MonoBehaviour {
    public GameObject PlayerObject;
	// Use this for initialization
	void Start () {
        if (PlayerObject == null)
        {
            PlayerObject = this.gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {

        Color pos = new Color(PlayerObject.transform.position.x, PlayerObject.transform.position.y, PlayerObject.transform.position.z);
        Shader.SetGlobalColor("_PlayerPos", pos);
    }
}
