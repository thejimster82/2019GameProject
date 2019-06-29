using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AlertEnemies(){
		// RaycastHit[] hits=Physics.SphereCastAll (hitTestPivot.position,20.0f, hitTestPivot.up);
		// foreach (RaycastHit hit in hits) {
		// 	if (hit.collider != null && hit.collider.tag == "Enemy") {
		// 		hit.collider.GetComponent<NPC_Enemy>().SetAlertPos(transform.position);
		// 	}
		// }
	}

    
	public void DamagePlayer(){
		// animator.SetBool ("Dead", true);
		// animator.transform.parent = null;
		// this.enabled = false;
		// myRigidBody.isKinematic = true;
		// GameManager.RegisterPlayerDeath ();
		// gameObject.GetComponent<Collider> ().enabled = false;
		// GameCamera.ToggleShake (0.3f);
		// Vector3 pos = animator.transform.position;
		// pos.y = 0.2f;
		// animator.transform.position = pos;
	}
}
