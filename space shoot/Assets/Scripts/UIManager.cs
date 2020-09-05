using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _liveImage;
    [SerializeField]
    private Sprite[] _liveSprite;

    private bool _flick = false;
    
    //[SerializeField]
    //private int _score = 0;
    private Player _player;
    //private Player _player;
    void Start () {
        //_score = 0;
        _player = GameObject.Find ("Player").GetComponent<Player> ();
        _scoreText.text = "Score: " + 0;
        // _gameOverText.text = "GAME OVER";
        //_liveImage.sprite = _liveSprite[2];
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update () {
        scoreUpdate();
        livesUpdate();
        Debug.Log("Player Lives: " + _player.getLives());
        if (_player.getLives() == 0){
            _gameOverText.gameObject.SetActive(true);
            _flick = true;
            StartCoroutine (textFlicker());
            _restartText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R)){
                SceneManager.LoadScene(1);
            }
        }

    }

    public void scoreUpdate () {

        if (_player != null) {
            //_score = _player.getScore();
            _scoreText.text = "Score: " + _player.getScore ();
        }
    }

    public void livesUpdate () {
        if (_player != null) {
            Debug.Log("lives: " + _player.getLives());
            _liveImage.sprite = _liveSprite[_player.getLives()];
        }
    }

    IEnumerator textFlicker ()
    {
        while (_flick){
            
            _gameOverText.text = " ";
            yield return new WaitForSeconds(0.75f);
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.75f);

            
        }

    }
}