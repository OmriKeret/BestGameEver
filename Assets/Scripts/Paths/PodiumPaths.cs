using UnityEngine;
using System.Collections;

public class PodiumPaths : MonoBehaviour
{

    public static Vector3[] NotMoveing = new Vector3[]
    {new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f)};

    public static Vector3[] AxisX = new Vector3[]
    {
        new Vector3(0f, 0f, 0f), new Vector3(9f, 0f, 0f), new Vector3(18f, 0f, 0f), new Vector3(28.5f, 0f, 0f),
        new Vector3(28.5f, 0f, 0f), new Vector3(18f, 0f, 0f), new Vector3(9f, 0f, 0f), new Vector3(0f, 0f, 0f),
        new Vector3(0f, 0f, 0f), new Vector3(-8f, 0f, 0f), new Vector3(-16f, 0f, 0f), new Vector3(-26.7f, 0f, 0f),
        new Vector3(-26.7f, 0f, 0f), new Vector3(-16f, 0f, 0f), new Vector3(-8f, 0f, 0f), new Vector3(0f, 0f, 0f)
    };


}
