using UnityEngine;
using System.Collections;

public class WaveLogicFactory {

	WaveLogic[] tutorialWaves;//TBD
    readonly WaveLogic[] _easyWaves;
	WaveLogic[] _mediumWaves;
	WaveLogic[] _hardWaves;
	WaveLogic[] _extremeWaves;

	public WaveLogicFactory(){
		_easyWaves = new WaveLogic[]{
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid}),
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid}),
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Spike,}),
            new WaveLogic(new EnemyType[]{EnemyType.Tank,EnemyType.Stupid}),
            new WaveLogic(new EnemyType[]{EnemyType.Tank,EnemyType.Tank}),
            new WaveLogic(new EnemyType[]{EnemyType.Tank,EnemyType.Spike}),
            new WaveLogic(new EnemyType[]{EnemyType.Spike,EnemyType.Spike})
		};
		_mediumWaves = new WaveLogic[]{
			
		};
		_hardWaves = new WaveLogic[]{
			
		};
		_extremeWaves = new WaveLogic[]{
			
		};
	}

	private WaveLogic createWave(WaveLogic[] i_waves){
		int waveNumber = UnityEngine.Random.Range (0,i_waves.Length);
	    WaveLogic wave = new WaveLogic(i_waves[waveNumber]);
		return wave;
	}


	public WaveLogic createEasyWave(){
		return createWave(_easyWaves);
	}

    public WaveLogic CreateMediumWave()
    {
        return createEasyWave().mergeWaves(createEasyWave());
    }
}
