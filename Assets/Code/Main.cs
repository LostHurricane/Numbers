using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] private int _max;
    [SerializeField] private int _min;
    [SerializeField] private int _maxTime;
    private int _time = 0;

    
    [SerializeField]
    private List<InteractiveNumber> _numbersToGuess;

    [SerializeField]
    private List<InteractiveNumber> _numbersToDrag;

    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _checkButton;
    [SerializeField]
    private Scrollbar _timeToRemember;
    [SerializeField]
    private TextMeshProUGUI _timeIndicator;

    private GameContoller _gameContoller;

    void Awake()
    {
        _gameContoller = new GameContoller(_numbersToGuess, _numbersToDrag, (_min, _max));
        
        _startButton.onClick.AddListener(RunGame);
        _checkButton.onClick.AddListener(_gameContoller.CheckGuesses);
        _timeToRemember.onValueChanged.AddListener(ScrollCheck);

    }

    public void RunGame()
    {
        _startButton.gameObject.SetActive(false);
        _timeToRemember.gameObject.SetActive(false);
        StartCoroutine(_gameContoller.StartNewGame(_time));
    }

    public void ScrollCheck(float value)
    {
        _time = (int)(value * _maxTime);
        _timeIndicator.text = _time.ToString();
    }

}
