using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersGenerator
{
    private int _count;
    private List<int> _numbers;



    public NumbersGenerator(int count)
    {
        _numbers = new List<int>();
        _count = count;
    }

    public int[] GenerateNewNumberSet((int min, int max) range)
    {
        _numbers.Clear();
        for (int i = 0; i < _count; i++)
        {
            var variable = Random.Range(range.min, range.max);

            if (!_numbers.Contains(variable))
            {
                _numbers.Add(variable);
            }
            else i--;
        }
        return _numbers.ToArray();
    }
}
