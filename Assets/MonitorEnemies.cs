using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorEnemies : MonoBehaviour {

    public GameObject enemies;
    public GameObject winPanel;
    public Text enemyCounter;

    private int enemiesRemaining;

	// Use this for initialization
	void Start () {
        enemiesRemaining = enemies.transform.childCount;
        enemyCounter.text = enemiesRemaining + "";
	}

	// Update is called once per frame
	void Update () {
        if (enemies.transform.childCount != enemiesRemaining)
        {
            enemiesRemaining = enemies.transform.childCount;
            enemyCounter.text = enemiesRemaining + "";
        }
        if (enemiesRemaining <= 0 && !winPanel.activeInHierarchy) {
            winPanel.SetActive(true);
        }
	}
}
