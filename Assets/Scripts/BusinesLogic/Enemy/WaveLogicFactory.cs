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
            //0
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.TopRight,EnemyLocation.TopRight},
            new int[]{1,1}),
            //1                
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.TopRight,EnemyLocation.TopLeft},
            new int[]{1,1,1}),
            //2                
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.TopRight,EnemyLocation.TopRight,EnemyLocation.TopRight},
            new int[]{2,1,1})     ,
            //3                
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.TopRight,EnemyLocation.TopRight,EnemyLocation.TopRight},
            new int[]{2,1,1}) ,
            //4                
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.TopRight,EnemyLocation.MidRight,EnemyLocation.BottomRight,EnemyLocation.MidLeft,EnemyLocation.BottomLeft},
            new int[]{6}) ,
            //5                
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.BottomLeft,EnemyLocation.TopMid,EnemyLocation.BottomRight,EnemyLocation.TopLeft,EnemyLocation.TopRight,EnemyLocation.BottomLeft,EnemyLocation.TopMid,EnemyLocation.BottomRight,EnemyLocation.TopLeft,EnemyLocation.TopRight},
            new int[]{1,1,1,1,1,1,1,1,1,1}) ,
            //6               
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Spike,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.BottomRight,EnemyLocation.MidRight,EnemyLocation.BottomRight,EnemyLocation.MidRight,EnemyLocation.MidRight},
            new int[]{1,1,1,1,1}) ,
            //7               
            new WaveLogic(new EnemyType[]{EnemyType.Spike,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Spike,EnemyType.Stupid,EnemyType.Spike,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Spike},
            new EnemyLocation[]{EnemyLocation.MidLeft,EnemyLocation.TopLeft,EnemyLocation.BottomLeft,EnemyLocation.MidLeft,EnemyLocation.TopMid,EnemyLocation.TopRight,EnemyLocation.TopRight,EnemyLocation.MidLeft,EnemyLocation.MidRight},
            new int[]{1,1,2,1,1,1,1,1}) ,
            //8               
            new WaveLogic(new EnemyType[]{EnemyType.Spike,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Spike,EnemyType.Stupid,EnemyType.Spike,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Spike},
            new EnemyLocation[]{EnemyLocation.TopRight,EnemyLocation.TopMid,EnemyLocation.BottomRight,EnemyLocation.MidLeft,EnemyLocation.TopMid,EnemyLocation.TopRight,EnemyLocation.TopRight,EnemyLocation.MidLeft,EnemyLocation.MidRight},
            new int[]{1,1,1,1,2,1,2}) ,
            //9               
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Tank,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Tank,EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.TopRight,EnemyLocation.TopMid,EnemyLocation.BottomRight,EnemyLocation.MidLeft,EnemyLocation.TopMid,EnemyLocation.TopRight,EnemyLocation.TopRight,EnemyLocation.MidLeft,EnemyLocation.MidRight},
            new int[]{1,1,2,1,1,2}) ,
            //10           
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Spike,EnemyType.Tank,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Spike,EnemyType.Tank,EnemyType.Stupid,EnemyType.Tank,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Spike,EnemyType.Spike,EnemyType.Tank},
            new EnemyLocation[]{EnemyLocation.TopMid,EnemyLocation.TopRight,EnemyLocation.TopMid,EnemyLocation.MidLeft,EnemyLocation.TopLeft,EnemyLocation.MidRight,EnemyLocation.TopMid,EnemyLocation.TopRight,EnemyLocation.TopLeft,EnemyLocation.BottomLeft,EnemyLocation.TopLeft,EnemyLocation.TopRight,EnemyLocation.BottomRight,EnemyLocation.BottomRight,EnemyLocation.TopMid,EnemyLocation.TopRight},
            new int[]{2,2,3,2,4,3}) ,



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
