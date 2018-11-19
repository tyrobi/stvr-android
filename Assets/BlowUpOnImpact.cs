using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUpOnImpact : MonoBehaviour {

    public GameObject explosion;

	void OnCollisionEnter(Collision col) {
        GameObject e = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(e, 1);
        Destroy(gameObject);
    }
}
