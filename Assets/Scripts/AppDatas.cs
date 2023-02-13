using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Appdata")]
public class AppDatas : ScriptableObject
{
    public Difficulty actualDifficulty;
    public List<Difficulty> difficultyList;
}