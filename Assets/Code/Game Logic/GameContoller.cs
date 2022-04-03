using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContoller
{
    private PanelController _panelController;

    private List<InteractiveNumber> _numbersToGuess;
    private List<InteractiveNumber> _numbersToDrag;

    private List<InteractiveNumber> _correctGueses;


    public GameContoller (List <InteractiveNumber> numbersToGuess, List<InteractiveNumber> numbersToDrag, (int min, int max) range)
    {
        _numbersToGuess = numbersToGuess;
        _panelController = new PanelController(_numbersToGuess, range);
        _numbersToDrag = new List<InteractiveNumber>(numbersToDrag);
        _correctGueses = new List<InteractiveNumber>();

    }

    public IEnumerator StartNewGame(float time)
    {
        _panelController.StartChallenge();
        var count = Mathf.Min(_numbersToDrag.Count, _panelController.Numbers.Count);
        for (var i = 0; i < count ; i++ )
        {
            var n = (int) Random.Range(0, _panelController.Numbers.Count - 1);
            _numbersToDrag[i].SetNumber(_panelController.Numbers[n]);
            _panelController.Numbers.RemoveAt(n);
        }

        foreach(var number in _numbersToGuess)
        {
            number.CompareNumbers += CompareNumber;
        }

        yield return new WaitForSeconds(time);

        foreach (var number in _numbersToGuess)
        {
            number.CanvasGroup.alpha = 0;
        }

    }

    public void CheckGuesses ()
    {
        foreach (var num in _numbersToDrag)
        {
            num.gameObject.SetActive(false);
            //num.CanvasGroup.alpha = 0;
        }
        foreach (var num in _numbersToGuess)
        {
            num.CanvasGroup.alpha = 1;
            num.Text.color = Color.red;
        }
        foreach (var num in _correctGueses)
        {
            num.Text.color = Color.green;
        }
    }


    private void CompareNumber(InteractiveNumber correctNumber, InteractiveNumber dragedNumber) 
    {
        
        if (correctNumber.Number == dragedNumber.Number)
        {
            _correctGueses.Add(correctNumber);
        }
    }
    
}
