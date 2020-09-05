using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    [SerializeField]
    private float _rotateSpeed = 4.5f;
    [SerializeField]
    private bool _isDestroied;
    private Animator _asteroid;

    private SpawnManager _spawn;
    // Start is called before the first frame update
    void Start () {
        _asteroid = gameObject.GetComponent<Animator> ();
        _spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManager> ();

    }

    // Update is called once per frame
    void Update () {
        transform.Rotate (Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Laser"){
            Destroy(other.gameObject);
            _asteroid.SetTrigger("Asteroid_explo");
            if (_spawn != null) {
                _spawn.startSpawn();
            }
            Destroy(this.gameObject, 2.3f);
            _isDestroied = true;
        }
    }

    public bool isDestroied(){
        return _isDestroied;
    }
}