using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToEnemyInfo : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(2);
        }
    }
}
