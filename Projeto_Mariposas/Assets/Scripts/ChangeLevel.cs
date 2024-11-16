using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour, IInteractable
{
    public Animator animator;
    private int levelIndex;

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeToLevel(int sceneIndex){
        levelIndex = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void onFadeComplete(){
        SceneManager.LoadScene(levelIndex);
    }

    public void Interact()
    {
        FadeToLevel(levelIndex);
    }
}
