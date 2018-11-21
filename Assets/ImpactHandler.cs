using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImpactHandler : MonoBehaviour {

    // The number of impacts it takes to remove this object
    public int hitPoints = 1;
    public GameObject explosion;
    public GameObject preExplosion;
    public Slider healthSlider;

    public GameObject losePanel;

    public bool isPlayer = false;
    private bool debounce = false;

    private void OnDeath(Vector3 impact)
    {
        GameObject e = Instantiate(explosion, impact, Quaternion.identity);
        Destroy(e, 3);
        Destroy(gameObject, 0.05f);
        if (isPlayer && losePanel != null && !losePanel.activeInHierarchy) {
            losePanel.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (debounce) return;
        debounce = true;
        if (col.gameObject.name == "Terrain")
            hitPoints = 0;
        else
            hitPoints--;
        if (isPlayer){
            healthSlider.value = hitPoints;
        }
        if (hitPoints <= 0)
        {
            OnDeath(col.contacts[0].point);
        }
        else if (preExplosion != null)
        {
            GameObject e = Instantiate(preExplosion, col.contacts[0].point, Quaternion.identity);
            Destroy(e, 6);
        }
        if (col.gameObject.name == "Missile")
            Destroy(col.gameObject);
        debounce = false;
    }
}
