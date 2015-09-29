using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WaveLogic {

	EnemyType[] _enemies;
    EnemyLocation[] _locations;
    int[] _numOfEnemiesToInstaniate;
	int _currentNumOfEnemy;
    int _currentEnemiesInRow;
	float _strengthMultiplier;
	float _speedMultiplier;
	int _hpBonus;
    public int MaxNumOfEasy = 4;


	public WaveLogic(EnemyType[] i_Enemies, EnemyLocation[] i_Location, int[] i_NumOfEnemies){
		_enemies = new EnemyType[i_Enemies.Length+1];
        _locations = new EnemyLocation[i_Location.Length + 1];
        _numOfEnemiesToInstaniate = new int[i_NumOfEnemies.Length+1];
        for (int i = 0; i < i_Enemies.Length; i++)
        {
			_enemies[i] = i_Enemies[i];
		}
        _enemies[_enemies.Length - 1] = EnemyType.End;
        for (int i = 0; i < i_Location.Length; i++)
        {
            _locations[i] = i_Location[i];
        }
        _locations[_locations.Length - 1] = EnemyLocation.BottomRight;
        for (int i = 0; i < i_NumOfEnemies.Length; i++)
        {
            _numOfEnemiesToInstaniate[i] = i_NumOfEnemies[i];
        }
        _numOfEnemiesToInstaniate[_numOfEnemiesToInstaniate.Length - 1] = 1;
        
		_currentNumOfEnemy = 0;
        _currentEnemiesInRow = 0;
		_strengthMultiplier = 1;
		_speedMultiplier = 1;
		_hpBonus = 0;
	}

    public WaveLogic(WaveLogic i_Wave) : this(i_Wave._enemies,i_Wave._locations,i_Wave._numOfEnemiesToInstaniate)
    {

    }

    /**
     * Try to init an enemy.
     * Get type and location and init them.
     * return false if there are more enemies that need to be instantiated at the same time.
     * */
    public bool InitEnemy(out EnemyType o_Type, out EnemyLocation o_Location)
    {
        //These values won't instanciate, they only indicates the end of the wave
        o_Type = EnemyType.End;
        o_Location = EnemyLocation.BottomRight;
        if (_currentNumOfEnemy < _enemies.Length)
        {
            o_Type = _enemies[_currentNumOfEnemy];
            o_Location = _locations[_currentNumOfEnemy];
            if (_numOfEnemiesToInstaniate[_currentEnemiesInRow] == 0)
            {
                _currentEnemiesInRow++;
            }
            _numOfEnemiesToInstaniate[_currentEnemiesInRow]--;
            _currentNumOfEnemy++;
        }
        return _numOfEnemiesToInstaniate[_currentEnemiesInRow] == 0;

    }

	public EnemyType GetNextEnemy(){
		EnemyType currentEnemy = EnemyType.End;
		if (_currentNumOfEnemy < _enemies.Length) {
			currentEnemy = _enemies[_currentNumOfEnemy];
			_currentNumOfEnemy++;
		}
		return currentEnemy;
	}

	public int getLength(){
		return _enemies.Length;
	}

    
    public WaveLogic MergeWaves(WaveLogic i_Wave)
    {
        //Combining enemy type
        List<EnemyType> newTypes = new List<EnemyType>();
        foreach (EnemyType enemyType in _enemies)
        {
            if (enemyType != EnemyType.End)
            {
                newTypes.Add(enemyType);
            }
        }
        foreach (EnemyType enemyType in i_Wave._enemies)
        {
            if (enemyType != EnemyType.End)
            {
                newTypes.Add(enemyType);
            }
        }

        //Combining enemy location
        List<EnemyLocation> locations = new List<EnemyLocation>();
        for (int i = 0; i < _locations.Length - 1; i++)
        {
            locations.Add(_locations[i]);
        }

        for (int i = 0; i < i_Wave._locations.Length - 1; i++)
        {
            locations.Add(i_Wave._locations[i]);
        }

        //Combining number to init
        List<int> initTogather = new List<int>();
        for (int i =0;i<_numOfEnemiesToInstaniate.Length-2;i++)
        {
            initTogather.Add(_numOfEnemiesToInstaniate[i]);
        }

        if (IsCombinalbe(i_Wave))
        {
            initTogather.Add(LastSet+i_Wave.FirstSet);
        }
        else
        {
            initTogather.Add(LastSet);
            initTogather.Add(i_Wave.FirstSet);
        }

        for (int i = 1; i < i_Wave._numOfEnemiesToInstaniate.Length-1; i++)
        {
            initTogather.Add(i_Wave._numOfEnemiesToInstaniate[i]);
        }

        WaveLogic newWave = new WaveLogic(newTypes.ToArray(), locations.ToArray(), initTogather.ToArray());
        return newWave;
    }

    public override string ToString()
    {
        string info = String.Empty;
        foreach (EnemyType enemy in _enemies)
        {
            info += enemy+" ";
        }
        info += Environment.NewLine;
        foreach (EnemyLocation enemyLocation in _locations)
        {
            info += string.Format("{0} ", enemyLocation.ToString());
        }
        info += Environment.NewLine;
        foreach (int i in _numOfEnemiesToInstaniate)
        {
            info += string.Format("{0}, ", i.ToString());
        }
        info += Environment.NewLine;
        return info;
    }

    public int Length
    {
        get { return _enemies.Length; }
    }

    public EnemyLocation FirstLocation
    {
        get { return _locations[0]; }
    }

    public EnemyLocation[] Locations
    {
        get { return _locations; }
    }

    public EnemyLocation LastLocation
    {
        get { return _locations[_locations.Length - 2]; }
    }

    public int FirstSet
    {
        get { return _numOfEnemiesToInstaniate[0]; }
    }

    public int LastSet
    {
        get { return _numOfEnemiesToInstaniate[_numOfEnemiesToInstaniate.Length - 2]; }
    }

    public bool IsCombinalbe(WaveLogic i_Wave)
    {
        return false;//Just for debug
        EnemyLocation first = i_Wave.FirstLocation;
        EnemyLocation last = LastLocation;

        //Equal - No
        if (first == last)
        {
            return false;
        }
        if (LastSet + i_Wave.FirstSet > MaxNumOfEasy)
        {
            return false;
        }
        //Parallel
        if ((int) first + (int) last == 8)
        {
            return true;
        }
        //Diagonal
        if (Math.Abs((int)first - (int)last) == 4)
        {
            return true;
        }
        //Near - No
        if (Math.Abs((int)first - (int)last) == 1)
        {
            return false;
        }
        return combinationFullCheck(i_Wave);
    }

    /**
     * Make sure there won't be two enemies in the same location after the merge
     * */
    private bool combinationFullCheck(WaveLogic i_Wave)
    {
        //If both sets have more than 1, the combination makes a messy screen
        if (LastSet > 1 && i_Wave.FirstSet > 1)
        {
            return false;
        }
        int[] locations = new int[Enum.GetNames(typeof(EnemyLocation)).Length];
        try
        {

            if (LastSet > 1)
            {
                for (int i = _numOfEnemiesToInstaniate.Length - 1; i >= LastSet; i--)
                {
                    locations[(int) _locations[i]]++;
                }
            }
            else
            {
                locations[(int) _locations[_locations.Length - 2]]++;
            }


            if (FirstSet > 1)
            {
                for (int i = 0; i < i_Wave.FirstSet; i++)
                {
                    locations[(int) i_Wave._locations[i]]++;
                }
            }
            else
            {
                locations[(int) i_Wave._locations[0]]++;
            }
            for (int i = 0; i < i_Wave.FirstSet; i++)
            {
                locations[(int) i_Wave._locations[i]]++;
            }

            //check the locations array
            foreach (int i in locations)
            {
                if (i > 1)
                {
                    return false;
                }
            }
        }
        catch (IndexOutOfRangeException iee)
        {

            //throw new UnityException(iee.Message.ToString());
            return false;
        }
        return true;
    }

    public bool IsLow()
    {
        for (int i = 0; i < _enemies.Length - 1; i++)
        {
            if (_enemies[i]!=EnemyType.Stupid)
            {
                return false;
            }
        }
        return true;
    }
}
