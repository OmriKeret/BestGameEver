using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveLogic {

	EnemyType[] _enemies;
	int _currentLocation;
	float _strengthMultiplier;
	float _speedMultiplier;
	int _hpBonus;


	public WaveLogic(EnemyType[] i_Enemies){
		_enemies = new EnemyType[i_Enemies.Length];
		for (int i=0; i<i_Enemies.Length; i++) {
			_enemies[i] = i_Enemies[i];
		}
		_currentLocation = 0;
		_strengthMultiplier = 1;
		_speedMultiplier = 1;
		_hpBonus = 0;
	}

    public WaveLogic(WaveLogic i_Wave)
    {
        _enemies = new EnemyType[i_Wave._enemies.Length];
        for (int i = 0; i < _enemies.Length; i++)
        {
            _enemies[i] = i_Wave._enemies[i];
        }
        _currentLocation = i_Wave._currentLocation;
        _speedMultiplier = i_Wave._speedMultiplier;
        _strengthMultiplier = i_Wave._strengthMultiplier;
        _hpBonus = i_Wave._hpBonus;
    }

	public EnemyType GetNextEnemy(){
		EnemyType currentEnemy = EnemyType.End;
		if (_currentLocation < _enemies.Length) {
			currentEnemy = _enemies[_currentLocation];
			_currentLocation++;
		}
		return currentEnemy;
	}

	public int getLength(){
		return _enemies.Length;
	}
}
