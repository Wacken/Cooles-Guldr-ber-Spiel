using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToShader : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameObject MonsterObject;
    public bool VisorOn = true;
    public Material visorMaterial;
    public AudioSource PlayerAudioSource;
    //public AudioSource VisorStop;
    // public AudioSource VisorRun;
    public AudioClip cVisorStart;
    public AudioClip cVisorStop;
    public AudioClip cVisorRun;


    // Use this for initialization
    void Start()
    {
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
    public void toggleVisor()
    {
        VisorOn = !VisorOn;
        Shader.SetGlobalFloat("_VisorOn", VisorOn ? 1 : 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            toggleVisor();
            if (VisorOn == true)
            {

                PlayerAudioSource.Stop();
                PlayerAudioSource.volume = 0.4f; 
                PlayerAudioSource.loop = false; 
                PlayerAudioSource.clip = cVisorStart;
                PlayerAudioSource.Play();

                StartCoroutine("StartVisorRunSound");

            }
            else {
                PlayerAudioSource.Stop();
                PlayerAudioSource.volume = 0.4f;
                PlayerAudioSource.loop = false;
            PlayerAudioSource.clip = cVisorStop; 
            PlayerAudioSource.Play();
            }
        }
        Color pos = new Color(PlayerObject.transform.position.x, PlayerObject.transform.position.y, PlayerObject.transform.position.z);
        Shader.SetGlobalColor("_PlayerPos", pos);


        pos = new Color(MonsterObject.transform.position.x, MonsterObject.transform.position.y, MonsterObject.transform.position.z);
        Shader.SetGlobalColor("_MonsterPos", pos);
    }


    IEnumerator StartVisorRunSound()
    {
        yield return new WaitForSeconds(2f);
        PlayerAudioSource.volume = 0.1f;
        PlayerAudioSource.Stop();
        PlayerAudioSource.clip = cVisorRun;
        PlayerAudioSource.loop = true;
        PlayerAudioSource.Play();
    }

}