using UnityEngine;
using System.Collections;

public struct SceneStats
{

    public const float LeftEdge = -27.81f;
    public const float RightEdge = 29.94f;
    public const float TopEdge = 19f;
    public const float EnemyBottomEdge = 9f;
    public const float EnemyMidEdge = 14f;
    public const  float BottomEdge = -10.81f;

    public static readonly Vector3 TopRight = new Vector3(RightEdge, TopEdge);
    public static readonly Vector3 MidRight = new Vector3(RightEdge, EnemyMidEdge);
    public static readonly Vector3 BottomRight = new Vector3(RightEdge, EnemyBottomEdge);
    public static readonly Vector3 TopLeft = new Vector3(LeftEdge, TopEdge);
    public static readonly Vector3 MidLeft = new Vector3(LeftEdge, EnemyMidEdge);
    public static readonly Vector3 BottomLeft = new Vector3(LeftEdge, EnemyBottomEdge);
    public static readonly Vector3 TopMid = new Vector3(0, TopEdge+3);

    public static readonly float Width = RightEdge - LeftEdge;
    public static readonly float Hight = TopEdge - BottomEdge;


}
