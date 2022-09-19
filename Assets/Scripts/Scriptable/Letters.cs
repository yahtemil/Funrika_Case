using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "Letters/CreateLetter")]
public class Letters : ScriptableObject
{
    public string LetterName;
    [SerializeField]
    public LetterPoints[] letterPoints = new LetterPoints[5];
}

[System.Serializable]
public class LetterPoints
{
    public int PosX;
    public int PosY;
    [SerializeField]
    public int[,] Pos = new int[5, 5];
}
