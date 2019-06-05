using UnityEngine;
using System.Collections;

public class DestroyEnemy : MonoBehaviour
{
	public float tumble;
    private GameController gameController;
	public GameObject playerExplosion;
	public GameObject enemyExplosion;

    void Start ()
    {
	    GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble; 
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent <GameController>();
        }
        if (gameController == null)
        {
            Debug.Log ("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Boundary" || other.tag == "Enemy")
        {
            return;
        }
		else if (other.tag == "Player") {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}
       else if (other.tag == "Bolt"){
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.WinGame ();
	   }
	  
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
