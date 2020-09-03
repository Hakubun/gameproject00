using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private float _speed = 4.0f;
    private GameObject _enemy;
    private SpawnManager _spawn;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate (_enemy, new Vector3 (0, 7, 0), Quaternion.identity);
        //transform.position = new Vector3 (0, 5, 0);
        _spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawn == null)
        {
            Debug.LogError("Spawn Manager is NULL, enemy");
        }
        else{
            Debug.Log("Got it");
        }
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        //move down 4 meter per sec
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // respawn at top when pass bottom screen
        if (transform.position.y <= -6)
        {
            transform.position = new Vector3(Random.Range(-10f, 10f), 7, 0);
        }
        //Debug.Log("From enemy: " + _spawn._selfDistruction);
        if (_spawn._selfDistruction == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {

        //Debug.Log ("hit: " + other.transform.tag);
        //if other is player: self destroy + hit player
        if (other.transform.tag == "Player") {
            //Debug.Log (other.gameObject.tag + " hit me");
            //null checking
            //other.transform.GetComponent<Player>().Damage(); <----this might cause error if the Component "Player" doesnt exist
            Player player = other.transform.GetComponent<Player> ();
            if (player != null) {
                player.Damage ();
            }
            Destroy (this.gameObject);
        } else if (other.transform.tag == "Laser") {
            //Debug.Log (other.gameObject.tag + " hit me");
            Destroy (other.gameObject);
            Destroy (this.gameObject);
            //add score 10
            //_player.addScore;
            if (_player != null)
            {
                Debug.Log("add 10");
                _player.addScore(Random.Range(1, 10));
            }
            
        }
        //if other is laser: self destroy + destroy laser (laser first)
    }
}