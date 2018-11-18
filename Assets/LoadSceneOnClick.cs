using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    AudioSource aud;

    public void LoadSceneByIndex(int index)
    {
        aud = GetComponent<AudioSource>();
	aud.Play();
        SceneManager.LoadScene(index);
    }
}
