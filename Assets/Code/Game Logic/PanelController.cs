using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PanelController 
{
    public List<int> Numbers { get; private set; }

    private List<InteractiveNumber> _numbers;
    private NumbersGenerator _numbersGenerator;
    private (int min, int max) _range;

    

    public PanelController (List<InteractiveNumber> numbers, (int min, int max) range)
    {
        _range = range;
        _numbers = numbers;
        Numbers = new List<int>(_numbers.Count);
        _numbersGenerator = new NumbersGenerator(_numbers.Count);
    }

    public void StartChallenge()
    {
        Numbers.Clear();
        Numbers.AddRange(_numbersGenerator.GenerateNewNumberSet(_range));
        for (var i = 0; i < _numbers.Count; i++)
        {
            _numbers[i].SetNumber(Numbers[i]);
            _numbers[i].Text.color = Random.ColorHSV(0f, 0.9f);
        }
    }

}
