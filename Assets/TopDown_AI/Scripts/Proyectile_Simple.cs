using UnityEngine;
using System.Collections;

public class Proyectile_Simple : MonoBehaviour
{
    public enum CollisionTarget { PLAYER, ENEMIES }
    public CollisionTarget collisionTarget;
    public float lifeTime = 3.0f;
    public float speed = 1.5f;

    bool friendlyFire = false;
    bool hitTest = true;
    bool moving;
    void Start()
    {

        moving = true;
        Destroy(gameObject, lifeTime);
    }


    void Update()
    {

        if (moving)
            transform.Translate(transform.forward * speed, Space.World);



    }
    void OnTriggerEnter(Collider col)
    {
        if (collisionTarget == CollisionTarget.PLAYER && col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<playerBehavior>().DamagePlayer(10);
            DestroyProyectile();

        }
        else if (collisionTarget == CollisionTarget.ENEMIES && col.gameObject.tag == "Enemy" && friendlyFire)
        {
            col.gameObject.GetComponent<NPC_Enemy>().Damage();
            DestroyProyectile();
        }
        else if (col.gameObject.tag == "PlayerAtk")
        {
            transform.rotation = col.transform.rotation;
        }
        else if (col.gameObject.tag == "Finish")
        { //This is to detect if the proyectile collides with the world, i used this tag because it is standard in Unity (To prevent asset importing issues)
            DestroyProyectile();
        }



    }
    void DestroyProyectile()
    {

        /*hitTest=false;
		gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		gameObject.GetComponent<Collider> ().enabled = false;
		moving = false;*/
        Destroy(gameObject);
    }

}

