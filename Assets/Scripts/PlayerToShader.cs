using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToShader : MonoBehaviour {
    public GameObject PlayerObject;
    public GameObject MonsterObject;
    public bool VisorOn = true;
    public Material visorMaterial;
 

    // Use this for initialization
    void Start () {
        if (PlayerObject == null)
        {
            PlayerObject = this.gameObject;
        }
        if (MonsterObject == null)
        {

            MonsterObject = GameObject.Find("Monster");   //in ccase ppl forgot to assign it in the script
        }
        if (MonsterObject == null)
        {
            Debug.Log("MONSTER NOT FOUND ERROR");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            VisorOn = !VisorOn;
            Shader.SetGlobalFloat("_VisorOn", VisorOn?1:0);
        }
        Color pos = new Color(PlayerObject.transform.position.x, PlayerObject.transform.position.y, PlayerObject.transform.position.z);
        Shader.SetGlobalColor("_PlayerPos", pos);


        pos = new Color(MonsterObject.transform.position.x, MonsterObject.transform.position.y, MonsterObject.transform.position.z);
        Shader.SetGlobalColor("_MonsterPos", pos);
    }
}
