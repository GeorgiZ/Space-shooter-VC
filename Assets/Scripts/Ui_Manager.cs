using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Ui_Manager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Sprite[] _spriteLives;
    [SerializeField] private Image _ImgLives;
    [SerializeField] private GameObject Game_Over_Text;
    [SerializeField] private GameObject Restart_Game_Text;
    [SerializeField] private Text _ammoCount;
    [SerializeField] private Transform bar;
    [SerializeField] private Transform _bossHpBar;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _nuclear;
    [SerializeField] GameObject spawnManager;

    private Image _nuclearColour;
    private Animator BlinkingAmmunition;
    public float _barX = 1;
    private float maxBar = 1f;
    private Player ThePlayer;

    void Start()
    {
        _nuclearColour = _nuclear.GetComponent<Image>();
        ThePlayer = GameObject.Find("Player").GetComponent<Player>();
        _scoreText.text = "Score : " + 0;
        StartCoroutine(Bar());
        BlinkingAmmunition = this.transform.GetChild(4).gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        ThePlayer.CheckLives();
        Quit();
        UpdateNuclearState();
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score :" + playerScore;
    }

    public void UpdateNuclearState()
    {
        if(ThePlayer._isNuclearActive == false)
        {
            _nuclearColour.color = Color.gray;
        }
        else if(ThePlayer._isNuclearActive == true)
        {
            _nuclearColour.color = Color.white;
        }
    }

    public void UpdateAmmo(int ammo)
    {
        _ammoCount.text = "Ammo : " + ammo + " /30";
        if (ammo == 0)
        {
            BlinkingAmmunition.SetBool("Blink", true);
            _ammoCount.text = "Out of ammunition";
            _ammoCount.color = Color.red;          
        }
        else
        {
            _ammoCount.color = Color.white;
            BlinkingAmmunition.SetBool("Blink", false);
        }   
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
            GameObject clone =  Instantiate(_explosion, ThePlayer.transform.position, Quaternion.identity);
            Destroy(clone, 1.47f);
        }
    }

    private void Quit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void ThrusterBarVisualization()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {        
            bar.localScale = new Vector3(_barX, 1f);
            _barX = _barX - 0.1f;
            
        }
        else if(_barX < maxBar)
        {
            _barX = _barX + 0.1f;
            bar.localScale = new Vector3(_barX, 1f);          
        }
    }

    IEnumerator Bar()
    {
        while(1 > 0)
        {
            yield return new WaitForSeconds(0.2f);
            ThrusterBarVisualization();
            if(_barX < 0)
            {
                _barX = 0;
            }
        }  
    } 
}
