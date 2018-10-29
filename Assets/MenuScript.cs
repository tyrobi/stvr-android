using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public GameObject startButton;
    public GameObject exitButton;
    public EventSystem events;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    public static void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public static void LoadLevel() {
        SceneManager.LoadScene("Scenes/test_scene.unity");
    }
}
