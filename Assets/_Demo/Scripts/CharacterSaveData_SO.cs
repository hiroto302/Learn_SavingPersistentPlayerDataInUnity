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

    [Header("SaveData")]
    [SerializeField]
    string key;         // We’ll use this key to store  the Json representation of this object via PlayerPrefs.


    public float LevelMultiplier
    {
        get { return 1 + (currentLevel - 1) * levelBuff; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    // 敵を攻撃した際に、得られる Points
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

        /* we’ll load the data here.
            this will load the JsonData which is stored at the key value in PlayerPrefs,
            and will simply overwrite the values here stored in this script. */

        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);

        Debug.Log("OnEnable : Referenced in SCOP");
    }

    void OnDisable()
    {
        if(key == "")
        {
            key = name;  // we'll simply use the object's name as the key
        }

        /* And then we simply use PlayerPrefs.SetString using our key value,
                and we store the JsonData at that key,
                and lastly, we run PlayerPrefs.save so those changes are committed to disk. */

        string jsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();

        Debug.Log("OnDisable : OUT SCOP");
    }
}
