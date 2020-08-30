using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
    [SerializeField]
    private float _speed = 3.0f;
    private GameObject _powerup;
    //powerup id -> 0 = trippleshot 1 = speed 2 = shield
    [SerializeField]
    private int _powerupID;

    // Start is called before the first frame update
    void Start () {
        //Instantiate ()
    }

    // Update is called once per frame
    void Update () {
        //movedown at speed of 3 (adjustable)
        transform.Translate (Vector3.down * _speed * Time.deltaTime);

        //destroy when leave screen
        if (transform.position.y < -6) {
            Destroy (this.gameObject);
        }

        //check for collision
        //oncollected destroy
        //only by player

    }

    private void OnTriggerEnter2D (Collider2D other) {

        if (other.tag == "Player") {
            Player player = other.transform.GetComponent<Player> ();
            if (player != null) {

                switch (_powerupID) {
                    case 0:
                        player.TripleShot ();
                        break;
                    case 1:
                        player.SpeedBoost ();
                        break;
                    case 2:
                        player.ShieldUP ();
                        break;
                }

            }
            Destroy (this.gameObject);
        }
    }
}