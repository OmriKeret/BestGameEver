using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveLogic : MonoBehaviour {

	EnemyType[] _enemies;
	int _currentLocation;
	float _strengthMultiplier;
	float _speedMultiplier;
	int _hpBonus;


	public WaveLogic(EnemyType[] i_enemies){
		_enemies = new EnemyType[i_enemies.Length];
		for (int i=0; i<i_enemies.Length; i++) {
			_enemies[i] = i_enemies[i];
		}
		_currentLocation = 0;
		_strengthMultiplier = 1;
		_speedMultiplier = 1;
		_hpBonus = 0;
	}

	public EnemyType getNextEnemy(){
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
