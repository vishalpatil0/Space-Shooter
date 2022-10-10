using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle to text
    [SerializeField]
    private Text _scoreText;
    private Player _player;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _livesDisplay;
    [SerializeField]
    private Text _GameOver;
    [SerializeField]
    private Text _restartGame;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "Score: " + _player.getScore();
    }

    public void updateLives(int CurrentLives)
    {
        _livesDisplay.sprite = _livesSprite[CurrentLives];
    }

    public void gameOver()
    {
        _GameOver.gameObject.SetActive(true);
        _restartGame.gameObject.SetActive(true);
        StartCoroutine(flickeringGameOver());
    }

    IEnumerator flickeringGameOver()
    {
        while (true)
        {
            _GameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _GameOver.text = " ";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
