using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //variable

    //[serializeField] allow designer to adjust the value but still prevent other script to access it
    [SerializeField]
    //usually set variable to private unless it need to be change to public for other script to access to.
    //put underscore -> '_' before variable name to identify them as private 
    private float _speed = 5.5f;
    [SerializeField]
    private float _firerate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    //so only player can edit it
    private int _lives = 3;
    private SpawnManager _spawn;
    //triple shot status variable
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleshotPre;
    [SerializeField]
    private GameObject _ShieldPre;
    [SerializeField]
    private bool _triple_shot = false;
    [SerializeField]
    private bool _speedUP = false;
    [SerializeField]
    private bool _shieldUP = false;

    //public float horizontalInput;

    // Start is called before the first frame update
    void Start () {
        //get player position = 0,0,0
        transform.position = new Vector3 (0, 0, 0);
        _spawn = GameObject.Find ("SpawnManager").GetComponent<SpawnManager> ();
        //null check
        if (_spawn == null) {
            Debug.LogError ("Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update () {
        movement ();

        // if hit space key: spawn game object aka 'laser'
        if (Input.GetKeyDown (KeyCode.Space) && Time.time > _canFire) {
            shoot ();
        }

    }

    void movement () {

        float horizontalInput = Input.GetAxis ("Horizontal");
        float verticalInput = Input.GetAxis ("Vertical");
        //same as transform.Translate(new Vector3(1, 0, 0))
        // *Time.deltaTime -> make the object move base on real time (1 meter / sec)
        // Vector3.right * 5 * Time.deltaTime (5 meter / sec)
        // oneline: transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
        if (_speedUP == true) {
            transform.Translate (Vector3.right * horizontalInput * 10 * Time.deltaTime);
            transform.Translate (Vector3.up * verticalInput * 10 * Time.deltaTime);
        }
        transform.Translate (Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate (Vector3.up * verticalInput * _speed * Time.deltaTime);

        //player bounds
        //Mathf.Clamp = 'set boundry'
        // - example: transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -6, 7), 0);
        // - "This set boundry to the y axis so when object wont be able to exceed the max or min value 

        //y =  7 ~ -6 x = 11 ~ -11 
        if (transform.position.y >= 7) {
            transform.position = new Vector3 (transform.position.x, -6, 0);
        } else if (transform.position.y <= -6) {
            transform.position = new Vector3 (transform.position.x, 7, 0);
        } else if (transform.position.x >= 11) {
            transform.position = new Vector3 (-11, transform.position.y, 0);
        } else if (transform.position.x <= -11) {
            transform.position = new Vector3 (11, transform.position.y, 0);
        }

    }

    void shoot () {
        //add real time and firerate to set the time to next possible fire time
        //_canFire is working as a time point that in game time when Time.time is under that timeline, player can not shootz
        _canFire = Time.time + _firerate;

        if (_triple_shot == true) {
            Instantiate (_trippleshotPre, transform.position, Quaternion.identity);
        } else {
            Instantiate (_laserPrefab, transform.position + new Vector3 (0, 1.03f, 0), Quaternion.identity);

        }

        //instantiate 3 lasers (prefab)
    }

    public void Damage () {
        if (_shieldUP == true) {
            _ShieldPre.SetActive (false);
            _shieldUP = false;
        } else {
            _lives -= 1;
        }

        //check if dead, if dead -> destroy

        if (_lives < 1) {
            //communicate with spawnmanager
            _spawn.OnPlayerDeath ();

        }
    }

    public void TripleShot () {
        _triple_shot = true;
        StartCoroutine (TripleShotRoutine ());
    }

    IEnumerator TripleShotRoutine () {

        yield return new WaitForSeconds (5.0f);
        _triple_shot = false;
    }

    public void SpeedBoost () {
        _speedUP = true;
        StartCoroutine (SpeedUPRoutine ());
    }

    IEnumerator SpeedUPRoutine () {
        yield return new WaitForSeconds (7.0f);
        _speedUP = false;
    }

    public void ShieldUP () {
        _shieldUP = true;
        //GameObject shield = Instantiate (_ShieldPre,transform.position, Quaternion.identity);
        //shield.transform.parent = this.transform;
        _ShieldPre.SetActive (true);
    }
}