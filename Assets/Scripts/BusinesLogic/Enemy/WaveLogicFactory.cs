﻿using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class WaveLogicFactory
{

    WaveLogic[] tutorialWaves;//TBD
    readonly WaveLogic[] _easyWaves;
    WaveLogic[] _mediumWaves;
    WaveLogic[] _hardWaves;
    WaveLogic[] _extremeWaves;
    WaveLogic[] _orderWaves;
    PodiumFactory podiumMaker;

    public WaveLogicFactory()
    {
        podiumMaker = new PodiumFactory();
        /* _orderWaves = new WaveLogic[]{
             //0
             new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid, EnemyType.Stupid,EnemyType.Stupid , EnemyType.Stupid,EnemyType.Stupid , EnemyType.Stupid,EnemyType.Stupid},
             new EnemyLocation[]{EnemyLocation.TopRight,EnemyLocation.TopRight, EnemyLocation.TopRight,EnemyLocation.TopRight, EnemyLocation.TopRight,EnemyLocation.TopRight, EnemyLocation.TopRight,EnemyLocation.TopRight},
             new int[]{4,4}),
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

         };*/

        _easyWaves = new WaveLogic[]{
			//Easy would be 8+ mobs
            //Stupids from the right
			new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid, EnemyType.Stupid,EnemyType.Stupid},
			new EnemyLocation[]{EnemyLocation.TopRight,EnemyLocation.TopRight, EnemyLocation.TopRight,EnemyLocation.TopRight},
			new int[]{1,1,1,1}),
			//Stupids from the left
			new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid, EnemyType.Stupid,EnemyType.Stupid},
			new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.TopLeft, EnemyLocation.TopLeft,EnemyLocation.TopLeft},
			new int[]{1,1,1,1}),
			//Stupids from both left and right
			new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid, EnemyType.Stupid,EnemyType.Stupid},
			new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.TopRight, EnemyLocation.TopLeft,EnemyLocation.TopRight},
			new int[]{1,1,1,1}),
			//Stupids from both right and left
			new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid, EnemyType.Stupid,EnemyType.Stupid},
			new EnemyLocation[]{EnemyLocation.TopRight,EnemyLocation.TopLeft, EnemyLocation.TopRight,EnemyLocation.TopLeft},
			new int[]{1,1,1,1}),
			//Stupids square
			new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid, EnemyType.Stupid,EnemyType.Stupid},
			new EnemyLocation[]{EnemyLocation.BottomLeft,EnemyLocation.BottomRight, EnemyLocation.TopLeft, EnemyLocation.TopRight},
			new int[]{2,2}),
			//Stupids triangle
			new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid, EnemyType.Stupid},
			new EnemyLocation[]{EnemyLocation.BottomLeft,EnemyLocation.BottomRight, EnemyLocation.TopMid},
			new int[]{3}),
            //Stupids all from topright, introducing Tank
			//Stupids + Tank
			new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid, EnemyType.Tank,EnemyType.Stupid},
			new EnemyLocation[]{EnemyLocation.TopRight,EnemyLocation.TopRight, EnemyLocation.TopRight,EnemyLocation.TopRight},
			new int[]{1,1,1,1}),
			//Stupids all from topleft, introducing Spike
			//Stupids + Spike
			new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid, EnemyType.Spike,EnemyType.Stupid},
			new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.TopLeft, EnemyLocation.TopLeft,EnemyLocation.TopLeft},
			new int[]{1,1,1,1}),
			//Stupids + Tank + Spike, shuffled 1.
			new WaveLogic(new EnemyType[]{EnemyType.Stupid, EnemyType.Stupid,EnemyType.Spike, EnemyType.Stupid,EnemyType.Stupid},
			new EnemyLocation[]{EnemyLocation.BottomLeft,EnemyLocation.BottomRight, EnemyLocation.TopLeft,EnemyLocation.TopMid, EnemyLocation.TopRight},
			new int[]{1,1,2,1}),
			//Shuffled 2
			new WaveLogic(new EnemyType[]{EnemyType.Stupid, EnemyType.Stupid,EnemyType.Stupid, EnemyType.Stupid , EnemyType.Tank},
			new EnemyLocation[]{EnemyLocation.BottomLeft,EnemyLocation.BottomRight, EnemyLocation.TopLeft,EnemyLocation.TopMid, EnemyLocation.TopRight},
			new int[]{1,2,1,1}),
			//Shuffled 3
			new WaveLogic(new EnemyType[]{EnemyType.Stupid, EnemyType.Stupid,EnemyType.Spike, EnemyType.Stupid, EnemyType.Tank},
			new EnemyLocation[]{EnemyLocation.BottomLeft,EnemyLocation.BottomRight, EnemyLocation.TopLeft,EnemyLocation.TopMid, EnemyLocation.TopRight},
			new int[]{1,2,1,1}),
			//Shuffled 4
			new WaveLogic(new EnemyType[]{EnemyType.Stupid, EnemyType.Stupid,EnemyType.Stupid, EnemyType.Spike, EnemyType.Tank},
			new EnemyLocation[]{EnemyLocation.BottomLeft,EnemyLocation.BottomRight, EnemyLocation.TopLeft,EnemyLocation.TopMid, EnemyLocation.TopRight},
			new int[]{1,1,2,1}),
            //new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid},
            //new EnemyLocation[]{EnemyLocation.TopRight,EnemyLocation.TopRight},
            //new int[]{1,1}),
            //2 left
            
			/*new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.TopLeft},
            new int[]{1,1}),
            // left right
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.MidRight,EnemyLocation.MidLeft},
            new int[]{1,1}),
            // All right
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.MidRight,EnemyLocation.TopRight,EnemyLocation.BottomRight, },
            new int[]{1,1,1}),
            // All left
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.MidLeft,EnemyLocation.TopLeft,EnemyLocation.BottomLeft, },
            new int[]{1,1,1}),
            //triangle
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.BottomLeft,EnemyLocation.BottomRight,EnemyLocation.TopMid},
            new int[]{3}),
            //X
            new WaveLogic(new EnemyType[]{EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.BottomRight,EnemyLocation.TopLeft,EnemyLocation.BottomLeft,EnemyLocation.TopRight },
            new int[]{2,2}),*/

            
		};
        _mediumWaves = new WaveLogic[]{
			//Stupid&spike bottom up
            new WaveLogic(new EnemyType[]{EnemyType.Spike,EnemyType.Stupid,EnemyType.Spike,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.BottomLeft,EnemyLocation.BottomRight,EnemyLocation.TopRight,EnemyLocation.TopLeft},
            new int[]{2,2}),
            //Stupid&spike top down
            new WaveLogic(new EnemyType[]{EnemyType.Spike,EnemyType.Stupid,EnemyType.Spike,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.TopRight,EnemyLocation.TopLeft,EnemyLocation.BottomLeft,EnemyLocation.BottomRight},
            new int[]{2,2}),
            //Mix triangle
            new WaveLogic(new EnemyType[]{EnemyType.Tank,EnemyType.Spike,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.TopMid,EnemyLocation.BottomRight,EnemyLocation.BottomLeft},
            new int[]{3}),
            //Mix triangle 2
            new WaveLogic(new EnemyType[]{EnemyType.Tank,EnemyType.Spike,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.TopMid,EnemyLocation.BottomLeft,EnemyLocation.BottomRight},
            new int[]{3}),
            //2 spike and stupid square
            new WaveLogic(new EnemyType[]{EnemyType.Spike,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Stupid,EnemyType.Spike},
            new EnemyLocation[]{EnemyLocation.MidLeft,EnemyLocation.BottomLeft,EnemyLocation.BottomRight,EnemyLocation.TopLeft,EnemyLocation.TopRight,EnemyLocation.MidRight},
            new int[]{1,4,1}),
            
		};
        _hardWaves = new WaveLogic[]{
            //Arials
            new WaveLogic(new EnemyType[]{EnemyType.Tank,EnemyType.Spike,EnemyType.Spike},
            new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.TopRight,EnemyLocation.MidRight},
            new int[]{2,1}),
            //
            new WaveLogic(new EnemyType[]{EnemyType.Tank,EnemyType.Spike,EnemyType.Spike,EnemyType.Stupid},
            new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.BottomRight,EnemyLocation.BottomLeft,EnemyLocation.TopRight},
            new int[]{1,2,1}),
            //Pentagon
            new WaveLogic(new EnemyType[]{EnemyType.Spike,EnemyType.Spike,EnemyType.Stupid,EnemyType.Spike,EnemyType.Spike},
            new EnemyLocation[]{EnemyLocation.BottomLeft,EnemyLocation.TopMid,EnemyLocation.BottomRight,EnemyLocation.TopLeft,EnemyLocation.TopRight},
            new int[]{1,1,1,1,1}),
            
			
		};
        _extremeWaves = new WaveLogic[]{
			//Square of death
            new WaveLogic(new EnemyType[]{EnemyType.Tank,EnemyType.Spike,EnemyType.Spike,EnemyType.Tank},
            new EnemyLocation[]{EnemyLocation.TopLeft,EnemyLocation.BottomRight,EnemyLocation.TopRight,EnemyLocation.MidLeft},
            new int[]{2,2}),
            //All
            new WaveLogic(new EnemyType[]{EnemyType.Spike,EnemyType.Spike,EnemyType.Spike,EnemyType.Spike},
            new EnemyLocation[]{EnemyLocation.BottomLeft,EnemyLocation.BottomRight,EnemyLocation.TopLeft,EnemyLocation.TopRight},
            new int[]{2,1,1}),
		};

    }

    public void createWaveByOrder(WaveGenerateModel i_model)
    {
        podiumMaker.SetupNewWave(i_model.waveNumber);
        //for debug

        if (i_model.waveNumber >= _orderWaves.Length)
        {
            i_model.waveNumber = _orderWaves.Length - 1;

        }
        //end for debug
        i_model.wave = new WaveLogic(_orderWaves[i_model.waveNumber]);
    }

    private WaveLogic createWave(WaveLogic[] i_waves)
    {
        int waveNumber = UnityEngine.Random.Range(0, i_waves.Length);
        WaveLogic wave = i_waves[waveNumber];
        
        return wave;
    }

    public WaveType ChooseRandomWaveType(int waveNumber)
    {
        int[] TypeChances = {   100 - 5*waveNumber, 
                                (int)(-0.25 * waveNumber * waveNumber + 7.5 * waveNumber + 1.5), 
                                (int)(-0.0067 * waveNumber * waveNumber * waveNumber + 0.2524 * waveNumber * waveNumber - 0.1905 * waveNumber - 0.9524), 
                                (int)(0.1048*waveNumber*waveNumber - 0.8571*waveNumber + 0.2381) };

        for (int i = 1; i < TypeChances.Length; i++)
        {
            TypeChances[i] = TypeChances[i] < 0 ? 0 : TypeChances[i];//Can't be below 0
            if (TypeChances[i] == 0)
            {
                TypeChances[i - 1] = 100;
            }
            TypeChances[i] += TypeChances[i - 1];
        }

        int randomPick = Random.Range(0, 100);
        int correctType = -1;
        for (correctType = 0; correctType < TypeChances.Length; correctType++)
        {
            if (randomPick < TypeChances[correctType])
            {
                break;
            }
        }
        
        return (WaveType)correctType;

    }


    public WaveLogic CreateWave(WaveType i_WaveType)
    {
        WaveLogic wave = null;
        //Only Easy for now - Or.M
        //remove the next three lines to return to normal random
        wave = createMediumWave();
        podiumMaker.SetupNewWave(wave);
        return wave;
        //Only Easy for now
        switch (i_WaveType)
        {
            case WaveType.Easy:
                wave = createEasyWave(); break;
            case WaveType.Medium:
                wave = createMediumWave(); break;
            case WaveType.Hard:
                wave = createHardWave(); break;

            default:
                wave = createEasyWave(); break;
        }

        podiumMaker.SetupNewWave(wave);
        
        return wave;
    }

    private WaveLogic createEasyWave()
    {
        WaveLogic one = createWave(_easyWaves);
        WaveLogic two = createWave(_easyWaves);
        if (!one.IsLow() && !two.IsLow())
        {
            two = createWave(_easyWaves);
        }

        WaveLogic merge = one.MergeWaves(createWave(_easyWaves)).MergeWaves(createWave(_easyWaves)).MergeWaves(two);
        return merge;
    }

    private WaveLogic createMediumWave()
    {
        bool type = Random.Range(0, 2) == 0 ? true : false;
        WaveLogic one = type ? createWave(_easyWaves) : createWave(_mediumWaves);
        WaveLogic two = type ? createWave(_mediumWaves) : createWave(_easyWaves);
        WaveLogic three = type ? createWave(_easyWaves) : createWave(_mediumWaves);
        WaveLogic four = type ? createWave(_mediumWaves) : createWave(_easyWaves);

        WaveLogic merge = one.MergeWaves(two).MergeWaves(three).MergeWaves(four);
        return merge;
    }

    private WaveLogic createHardWave()
    {
        bool type = Random.Range(0, 2) == 0 ? true : false;
        WaveLogic one = type ? createWave(_hardWaves) : createWave(_mediumWaves);
        WaveLogic two = type ? createWave(_mediumWaves) : createWave(_hardWaves);
        WaveLogic three = type ? createWave(_hardWaves) : createWave(_mediumWaves);

        WaveLogic merge = one.MergeWaves(two).MergeWaves(three);
        return merge;
    }

    private WaveLogic createExtremeWave()
    {
        bool type = Random.Range(0, 2) == 0 ? true : false;
        WaveLogic one = type ? createWave(_hardWaves) : createWave(_extremeWaves);
        WaveLogic two = type ? createWave(_extremeWaves) : createWave(_hardWaves);
        WaveLogic three = type ? createWave(_hardWaves) : createWave(_extremeWaves);

        WaveLogic merge = one.MergeWaves(two).MergeWaves(three);
        return merge;
    }

}
