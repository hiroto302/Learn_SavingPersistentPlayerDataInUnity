using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Save Data", menuName = "Character/Data", order = 1)]
public class CharacterSaveData_SO : ScriptableObject
{
    [Header("Stats")]

    [SerializeField]
    int currentHealth;

    [Header("Leveling")]

    [SerializeField]
    int currentLevel = 1;

    [SerializeField]
    int maxLevel = 30;

    [SerializeField]
    int basisPoint = 200;

    [SerializeField]
    int pointsTillNextLevel;

    [SerializeField]
    float levelBuff = 0.1f;



    public float LevelMultiplier
    {
        get { return 1 + (currentLevel - 1) * levelBuff; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public void AggregateAttackPoints(int points)
    {
        // 次のレベリングと到達するまで
        pointsTillNextLevel -= points;
        if(pointsTillNextLevel <= 0)
        {
            // Level の更新
            currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
            // 次の LevelUp まで必要な point
            pointsTillNextLevel += (int)(basisPoint * LevelMultiplier);

            Debug.Log("Level Up!!! CurrentLevel is " + currentLevel.ToString());
        }
    }

    void OnEnable()
    {
        if(pointsTillNextLevel == 0)
        {
            pointsTillNextLevel += (int)(basisPoint * LevelMultiplier);
        }
    }
}
