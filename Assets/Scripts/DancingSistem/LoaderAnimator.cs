using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoaderAnimator : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator animator;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            string jsonPath = Path.Combine(Application.persistentDataPath, "animationData.json");

            if (File.Exists(jsonPath))
            {
                string jsonData = File.ReadAllText(jsonPath);
                GameManager.AnimationData animationData = JsonUtility.FromJson<GameManager.AnimationData>(jsonData);
                
                if (animationData != null ) 
                {
                    animator.SetFloat(animationData.blendTreeDancingParameter, animationData.blendDancingValue);
                }
                else
                {
                    Debug.LogWarning("Animator not assigned ");
                }
            }
            else
            {
                Debug.LogWarning("Animation data file not found.");
            }
        }
        else
        {
            Debug.LogWarning("GameManager not found in the scene.");
        }
    }
}
