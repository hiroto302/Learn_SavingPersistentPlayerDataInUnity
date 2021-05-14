using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int i;
    public float f;
    public string s;
    public Vector3 v;
    public Color c;
    public LayerMask l;
    public AnimationCurve a;
    public GameObject g;
    public Rigidbody r;
    public Transform t;
    public int[] ints;
    public List<float> floats;

    public C cl;

    [System.Serializable]
    public struct C
    {
        public int i;
        public float f;
    }


    // Load
    void OnEnable()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("json test"), this);
        print("loaded");
    }

    // Save
    void OnDisable()
    {
        /* So this will simply convert the object into a JSON format representation
            and store it in the registry via PlayerPrefs based on the key “json test”. */
        PlayerPrefs.SetString("json test", JsonUtility.ToJson(this, true));
        PlayerPrefs.Save();

        print("saved");
    }


    // This method runs whenever there are changes made to the serialized information in the editor.
    void OnValidate()
    {
        // PlayerPrefs.SetInt("test int", 34);
        // Debug.Log("OvValidate");
    }
}
