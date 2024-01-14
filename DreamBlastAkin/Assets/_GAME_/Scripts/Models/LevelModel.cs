using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModel
{
    private int _currentLevel;
    
    public int CurrentLevel()
    {
        return _currentLevel;
    }

    public void IncreaseCurrentLevel(int increaseAmount)
    {
        _currentLevel += increaseAmount;
    }
    
    public void SetCurrentLevel(int value)
    {
        _currentLevel = value;
    }
}
