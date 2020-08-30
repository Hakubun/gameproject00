using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _enemy;
    private IEnumerator coroutine;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private bool _stopSpawning = false;
    public bool _selfDistruction = false;
    [SerializeField]
    private float yieldTime = 3.0f;
    [SerializeField]
    private GameObject[] _powerUP;

    void Start () {
        //SpawnRoutine ();
        coroutine = SpawnEnemyRoutine (yieldTime);
        StartCoroutine (coroutine);
        StartCoroutine (SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update () {

    }

    IEnumerator SpawnEnemyRoutine (float T) {

        while (_stopSpawning == false) {

            Vector3 spawnLocation = new Vector3 (Random.Range (-10.0f, 10.0f), 6.5f, 0);
            //assign newly create enemy to a variable to put in container
            GameObject newEnemy = Instantiate (_enemy, spawnLocation, Quaternion.identity);
            //assign the newEnemy to a "parent" akk "container"
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds (T);
        }
    }

    IEnumerator SpawnPowerUpRoutine () {
        while (_stopSpawning == false) {
            Instantiate (_powerUP[Random.Range (0,3)], new Vector3 (Random.Range (-9.5f, 9.5f), 6.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds (Random.Range (3.0f, 8.0f));
        }
    }


    public void OnPlayerDeath () {
        _stopSpawning = true;
        _selfDistruction = true;
    }
}