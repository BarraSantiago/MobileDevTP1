using UnityEngine;

public enum Difficulty
{
    EASY,
    NORMAL,
    HARD
}

[CreateAssetMenu(fileName = "Difficulty", menuName = "ScriptableObjects/DifficultyScriptableObject")]
public class DifficultyScriptableObject : ScriptableObject
{
    public Difficulty currentDifficulty;
}
