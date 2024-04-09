using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{

    [System.Serializable]
    public class AnimationData
    {
        public string blendTreeDancingParameter;
        public float blendDancingValue;
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SaveAnimationData(AnimationData animationData)
    {
        string jsonPath = Path.Combine(Application.persistentDataPath, "animationData.json"); 
        string jsonData = JsonUtility.ToJson(animationData);
        File.WriteAllText(jsonPath, jsonData);
        Debug.Log("Animation data saved to: " + jsonPath);
    }
    public void TransitionEscene(int scene) 
    {
        SceneManager.LoadScene(scene);
    } 
}
