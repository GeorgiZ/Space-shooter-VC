using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(3);
        }

    }
}
