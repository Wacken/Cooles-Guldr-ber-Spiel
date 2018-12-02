using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {
    public Material monsterMat;
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
    }
    public void death()
    {
        StartCoroutine(FadeToDeath());
        StartCoroutine(RestartLevelCoroutine());
    }

    public void deathEnding()
    {
        StartCoroutine(FadeToDeathSceneReload());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            death();
            //StartCoroutine(RestartLevelCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StopAllCoroutines();
            resetShader();
            
        }
    }


    IEnumerator RestartLevelCoroutine()
    {
        Debug.Log("Restarting scene in 5 seconds");
        yield return new WaitForSeconds(3f);
        
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
        SceneManager.LoadScene(0);
    }
}
