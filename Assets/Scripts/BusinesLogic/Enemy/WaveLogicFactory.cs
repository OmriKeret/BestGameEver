using UnityEngine;
using System.Collections;

public class WaveLogicFactory {

	WaveLogic[] tutorialWaves;//TBD
    readonly WaveLogic[] _easyWaves;
	WaveLogic[] _mediumWaves;
	WaveLogic[] _hardWaves;
	WaveLogic[] _extremeWaves;
    WaveLogic[] _orderWaves;
    PodiumFactory podiumMaker;

	public WaveLogicFactory(){
        podiumMaker = new PodiumFactory();
        _orderWaves = new WaveLogic[]{
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid},
                            new EnemyLocation[]{EnemyLocation.TopRight,EnemyLocation.TopRight},
                            new int[]{1,1}),
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid},
                            new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.TopRight,EnemyLocation.TopLeft},
                            new int[]{1,1,1}),
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid},
                            new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.TopRight,EnemyLocation.TopRight,EnemyLocation.TopRight},
                            new int[]{2,1,1})                                
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
        podiumMaker.SetupNewWave(i_model.waveNumber);
        //for debug
        Debug.Log("Init wave " + i_model.waveNumber);
        if (i_model.waveNumber >= _orderWaves.Length)
        {
            i_model.waveNumber = 0;
        }
        //end for debug
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
