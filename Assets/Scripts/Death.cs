using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Death : MonoBehaviour {
    public Material monsterMat;
    public Material xrayMat;
    public Text endingText;
    public Text creditsText;

    public static bool invulnerable = false;
    public static Death instance;
    public void Awake()
    {
        instance = this; //Death.instance.death()
        resetShader();
    }
    public void resetShader()
    {
        monsterMat.SetFloat("_DarknessNoiseRange", 3);
        monsterMat.SetFloat("_DarknessDistance", 5.3f);
        //xrayMat.SetFloat("_VisorRange", 18);
    }
    public void death()
    {
        if (invulnerable) return;
      //  StartCoroutine(FadeToDeath());
        StartCoroutine(FadeToDeathSceneReload());
    }
     
    public void goodEnding()
    {
        StartCoroutine(FadeToWinSceneReload());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        { 
                invulnerable = false;
            death();
            //StartCoroutine(RestartLevelCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            StopAllCoroutines();
            resetShader();
            
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            invulnerable = !invulnerable;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        { 
            creditsText.gameObject.SetActive(!creditsText.gameObject.activeSelf);
        }





    }


    IEnumerator RestartLevelCoroutine()
    {
        Debug.Log("Restarting scene in 5 seconds");
        yield return new WaitForSeconds(3f);


        if (Player.instance._playerToShader.VisorOn)
        {
            Player.instance._playerToShader.PlayerAudioSource.Stop();
            Player.instance._playerToShader.PlayerAudioSource.volume = 0.4f;
            Player.instance._playerToShader.PlayerAudioSource.loop = false;
            Player.instance._playerToShader.PlayerAudioSource.clip = Player.instance._playerToShader.cVisorStop;
            Player.instance._playerToShader.PlayerAudioSource.Play();
            Player.instance._playerToShader.toggleVisor();
        }

        SceneManager.LoadScene(1);
    }

    IEnumerator FadeToDeath()
    {
        for (float f = monsterMat.GetFloat("_DarknessNoiseRange"); f <= 30; f *= 1.1f)
        {

            monsterMat.SetFloat("_DarknessNoiseRange", f);
            yield return null;
        }
        for (float f = monsterMat.GetFloat("_DarknessDistance"); f <= 60; f *= 1.1f)
        {

            monsterMat.SetFloat("_DarknessDistance", f);
            yield return null;
        }
    }

    IEnumerator FadeToDeathSceneReload()
    {
        invulnerable = true;
        Debug.Log("Death");
        endingText.text = "You have have been consumed";
        endingText.gameObject.SetActive(true);
        
        bool breakVisor = Player.instance._playerToShader.VisorOn;
        if (breakVisor)
        {
            Player.instance._playerToShader.PlayerAudioSource.Stop();
            Player.instance._playerToShader.PlayerAudioSource.volume = 0.4f;
            Player.instance._playerToShader.PlayerAudioSource.loop = false;
            Player.instance._playerToShader.PlayerAudioSource.clip = Player.instance._playerToShader.cVisorStop;
            Player.instance._playerToShader.PlayerAudioSource.Play();
            Player.instance._playerToShader.toggleVisor();
        }

        for (float f = monsterMat.GetFloat("_DarknessNoiseRange"); f <= 30; f *= 1.1f)
        {
            //xrayMat.SetFloat("_VisorRange", f);
            monsterMat.SetFloat("_DarknessNoiseRange", f);
            if(breakVisor)
            Player.instance._playerToShader.toggleVisor();  //make the visor flicker off

            yield return null;
        }

        if (Player.instance._playerToShader.VisorOn)
        {
            Player.instance._playerToShader.toggleVisor();
        }


        for (float f = monsterMat.GetFloat("_DarknessDistance"); f <= 100; f *= 1.1f)
        {

            monsterMat.SetFloat("_DarknessDistance", f);
            yield return null;
        }
        yield return new WaitForSeconds(4f);

     

        endingText.gameObject.SetActive(false);
        invulnerable = false;

         

        SceneManager.LoadScene(1);
    }
    IEnumerator FadeToWinSceneReload()
    {
        Debug.Log("Victory");
        invulnerable = true;
        endingText.text = "You have discovered the source";
        endingText.gameObject.SetActive(true);

        for (float f = monsterMat.GetFloat("_DarknessNoiseRange"); f >= 3; f /= 1.1f)
        {
            //xrayMat.SetFloat("_VisorRange", f);
            monsterMat.SetFloat("_DarknessNoiseRange", f);
            yield return null;
        }
        for (float f = monsterMat.GetFloat("_DarknessDistance"); f >= 1; f /= 1.1f)
        {

            monsterMat.SetFloat("_DarknessDistance", f);
            yield return null;
        }
        yield return new WaitForSeconds(4f);


        if (Player.instance._playerToShader.VisorOn)
        { 
            Player.instance._playerToShader.PlayerAudioSource.Stop();
        Player.instance._playerToShader.PlayerAudioSource.volume = 0.4f;
        Player.instance._playerToShader.PlayerAudioSource.loop = false;
        Player.instance._playerToShader.PlayerAudioSource.clip = Player.instance._playerToShader.cVisorStop;
        Player.instance._playerToShader.PlayerAudioSource.Play();
        Player.instance._playerToShader.toggleVisor();
        }

        endingText.gameObject.SetActive(false);
        invulnerable = false;




        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);


    }
}
