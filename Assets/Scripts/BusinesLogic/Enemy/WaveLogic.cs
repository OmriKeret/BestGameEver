using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveLogic {

	EnemyType[] _enemies;
    EnemyLocation[] _locations;
    int[] _numOfEnemiesToInstaniate;
	int _currentNumOfEnemy;
    int _currentEnemiesInRow;
	float _strengthMultiplier;
	float _speedMultiplier;
	int _hpBonus;


	public WaveLogic(EnemyType[] i_Enemies, EnemyLocation[] i_Location, int[] i_NumOfEnemies){
		_enemies = new EnemyType[i_Enemies.Length+1];
        for (int i = 0; i < _enemies.Length-1; i++)
        {
			_enemies[i] = i_Enemies[i];
            _locations[i] = i_Location[i];
		}
        for (int i = 0; i < _numOfEnemiesToInstaniate.Length; i++)
        {
            _numOfEnemiesToInstaniate[i] = i_NumOfEnemies[i];
        }
        _enemies[_enemies.Length - 1] = EnemyType.End;
		_currentNumOfEnemy = 0;
        _currentEnemiesInRow = 0;
		_strengthMultiplier = 1;
		_speedMultiplier = 1;
		_hpBonus = 0;
	}

    public WaveLogic(WaveLogic i_Wave)
    {
        _enemies = new EnemyType[i_Wave._enemies.Length+1];
        for (int i = 0; i < _enemies.Length-1; i++)
        {
            _enemies[i] = i_Wave._enemies[i];
            _locations[i] = i_Wave._locations[i];
        }
        for (int i = 0; i < _numOfEnemiesToInstaniate.Length; i++)
        {
            _numOfEnemiesToInstaniate[i] = i_Wave._numOfEnemiesToInstaniate[i];
        }
        _enemies[_enemies.Length - 1] = EnemyType.End;
        _currentNumOfEnemy = i_Wave._currentNumOfEnemy;
        _currentEnemiesInRow = i_Wave._currentEnemiesInRow;
        _speedMultiplier = i_Wave._speedMultiplier;
        _strengthMultiplier = i_Wave._strengthMultiplier;
        _hpBonus = i_Wave._hpBonus;
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

    /*
    public WaveLogic mergeWaves(WaveLogic i_Wave)
    {
        EnemyType[] newEnemyTypes = new EnemyType[_enemies.Length + i_Wave._enemies.Length];
        for (int i = 0; i < _enemies.Length; i++)
        {
            newEnemyTypes[i] = _enemies[i];
        }
        for (int i = 0; i < i_Wave._enemies.Length; i++)
        {
            newEnemyTypes[i + _enemies.Length] = i_Wave._enemies[i];
        }
        return new WaveLogic(newEnemyTypes);
    }
     * */
}
