using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Ui_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Sprite[] _spriteLives;

    [SerializeField]
    private Image _ImgLives;

    [SerializeField]
    private GameObject Game_Over_Text;

    [SerializeField]
    private GameObject Restart_Game_Text;

    [SerializeField]
    private Text _ammoCount;

    private Player ThePlayer;


    // Start is called before the first frame update
    void Start()
    {
        ThePlayer = GameObject.Find("Player").GetComponent<Player>();
        _scoreText.text = "Score : " + 0;
    }

    private void Update()
    {
        //Restart();
        ThePlayer.CheckLives();
        Quit();
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score :" + playerScore;
    }

    public void UpdateAmmo(int ammo)
    {
        _ammoCount.text = "Ammo : " + ammo;
        
    }

    IEnumerator GmaeOverBehaviour()
    {
        while (true)
        {
            Game_Over_Text.gameObject.SetActive(true);
            Restart_Game_Text.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Game_Over_Text.gameObject.SetActive(false);
            Restart_Game_Text.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            
        }


    }

    public void ChangeLives(int currentLives)
    {
        _ImgLives.sprite = _spriteLives[currentLives];

        if (currentLives <= 0)
        {
            StartCoroutine(GmaeOverBehaviour());
        }
    }

    private void Quit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
  
}
