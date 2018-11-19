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

    public bool isPlayer = false;
    private bool debounce = false;


    private void OnCollisionEnter(Collision col) {
        if (debounce) return;
        debounce = true;
        hitPoints--;
        if (isPlayer){
            healthSlider = GameObject.Find("/HUDCanvas/HealthUI/HealthSlider").GetComponent<Slider>();
		    healthSlider.value = hitPoints;
	    }

        if (hitPoints <= 0)
        {
            GameObject e = Instantiate(explosion, col.contacts[0].point, Quaternion.identity);
            Destroy(e, 3);
            Destroy(gameObject, 0.05f);

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
