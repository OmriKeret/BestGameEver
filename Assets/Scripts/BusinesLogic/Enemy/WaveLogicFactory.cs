using UnityEngine;
using System.Collections;

public class WaveLogicFactory {

	WaveLogic[] tutorialWaves;//TBD
    readonly WaveLogic[] _easyWaves;
	WaveLogic[] _mediumWaves;
	WaveLogic[] _hardWaves;
	WaveLogic[] _extremeWaves;
    WaveLogic[] _orderWaves;

	public WaveLogicFactory(){
        _orderWaves = new WaveLogic[]{
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid},
                            new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.TopRight,EnemyLocation.BottomRight},
                            new int[]{1,1,1})
        };

        _easyWaves = new WaveLogic[]{
		};
        _mediumWaves = new WaveLogic[]{
			
		};
        _hardWaves = new WaveLogic[]{
			
		};
        _extremeWaves = new WaveLogic[]{
			
		};

	}

    public WaveLogic createWaveByOrder(WaveGenerateModel i_model)
    {
        return new WaveLogic(_orderWaves[i_model.waveNumber]);
    }

	private WaveLogic createWave(WaveLogic[] i_waves){
		int waveNumber = UnityEngine.Random.Range (0,i_waves.Length);
	    WaveLogic wave = new WaveLogic(i_waves[waveNumber]);
		return wave;
	}


	public WaveLogic createEasyWave(){
		return createWave(_easyWaves);
	}

}
