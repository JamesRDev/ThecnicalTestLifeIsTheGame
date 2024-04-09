using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DancingPlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string blendTreeDancingParameter;
    [SerializeField] private string[] dancingValuesBlendTree;
    [SerializeField] private Button[] dancingButtons;
    [SerializeField] private Button saveDancingButtons;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int scenLoad;


    private Button lastClickedButton;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (animator == null)
        {
            Debug.LogError("Animator not asigned");
        }

        if ( dancingButtons != null && dancingButtons.Length == dancingValuesBlendTree.Length) 
        {
            for (int i = 0; i < dancingButtons.Length; i++) 
            {
                int index = i;
                dancingButtons[i].onClick.AddListener(()=> DancingAnimationBlendTree(dancingValuesBlendTree[index]));
            }
        }
        else 
        {
            Debug.LogWarning("Assigned buttons do not match assigned animations");
        }

        if( saveDancingButtons != null)
        {
            saveDancingButtons.onClick.AddListener(SaveAnimation);
        }
        else 
        {
            Debug.LogError("Dancing save button not assigned or missing ");
        }
    }

    private void DancingAnimationBlendTree(string treeValues) 
    {
        if (animator == null) 
        {
            Debug.LogError("Animator not found");
            return;
        }

        if (HasDancingTreeParameter(blendTreeDancingParameter))
        {
            animator.SetFloat(blendTreeDancingParameter, float.Parse(treeValues));

            if (lastClickedButton != null)
            {
                lastClickedButton.interactable = true; 
            }

            Button selectedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            if (selectedButton != null)
            {
                selectedButton.interactable = false;
                lastClickedButton = selectedButton; 
            }
        }
        else
        {
            Debug.LogWarning($"Parameter {blendTreeDancingParameter} not found in Animator");
        }
    }

    private bool HasDancingTreeParameter(string parameterName) 
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters ) 
        {
            if (parameter.name == parameterName && parameter.type == AnimatorControllerParameterType.Float)
            { 
                return true;
            }
        }
        return false;
    }

    private void SaveAnimation()
    {
        if (gameManager != null)
        {
            float blendValue = animator.GetFloat(blendTreeDancingParameter);
            GameManager.AnimationData animationData = new GameManager.AnimationData();
            animationData.blendTreeDancingParameter = blendTreeDancingParameter;
            animationData.blendDancingValue = blendValue;
            gameManager.SaveAnimationData(animationData);

            StartCoroutine(GoScene());
        }
        else
        {
            Debug.LogWarning("GameManager not assigned or missing.");
        }
    }

    IEnumerator GoScene() 
    {
        yield return new WaitForSeconds(5);
        gameManager.TransitionEscene(scenLoad);
    }
}
