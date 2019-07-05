using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using InControl;

public class playerBehavior : MonoBehaviour
{
    public float playerHealth = 100;
    public gameCamera gameCam;
    public bool inCombat = false;
    public playerMovement playerMover;

    // Start is called before the first frame update
    void Start()
    {
        playerMover = GetComponent<playerMovement>();
        // var InputDevice = InputManager.ActiveDevice;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AlertEnemies()
    {
        // RaycastHit[] hits=Physics.SphereCastAll (hitTestPivot.position,20.0f, hitTestPivot.up);
        // foreach (RaycastHit hit in hits) {
        // 	if (hit.collider != null && hit.collider.tag == "Enemy") {
        // 		hit.collider.GetComponent<NPC_Enemy>().SetAlertPos(transform.position);
        // 	}
        // }
    }

    public void DamagePlayer(float amt)
    {
        // animator.SetBool ("Dead", true);
        // animator.transform.parent = null;
        // this.enabled = false;
        // myRigidBody.isKinematic = true;
        // GameManager.RegisterPlayerDeath ();
        // gameObject.GetComponent<Collider> ().enabled = false;
        gameCam.ToggleShake(0.3f);
        // Vector3 pos = animator.transform.position;
        // pos.y = 0.2f;
        // animator.transform.position = pos;
        playerHealth -= amt;
    }
}
