using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Button startButton;
    public Button exitButton;
    public EventSystem events;

	// Use this for initialization
	void Start () {
        exitButton.onClick.AddListener(ExitGame);
        startButton.onClick.AddListener(LoadLevel);
	}

    public static void ExitGame() {
#if UNITY_EDITOR_WIN
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public static void LoadLevel() {
        SceneManager.LoadScene("Scenes/test_scene.unity");
    }
}
