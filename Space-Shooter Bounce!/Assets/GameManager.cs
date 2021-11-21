using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int punti;
    public int nemici;
    public int maxNemici;
    public int asteroidi;
    public int maxAsteroidi;
    public Text testoPunti;
    public Text testoEnergia;
    public Text testoLife;
    public GameObject starship;
    public GameObject gameOverCanvas;
    public GameObject gameExitCanvas;
    public GameObject scudo;
    public SpawnerSpeciale spwanSpec;
    public AudioSource audioS;
    public float rebirthCD;
    public int playerEnergy;
    public int playerLife;
    public int nemiciUccisi;
    public float asteroideCD;
    public int numNemici;
    private AudioClip laserHitClip;
    private AudioClip enemydeath;
    private AudioClip shieldhit;
    public bool isBossTime = false;
    public float untilBoss;
    public AudioClip playerExplo;


    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        punti = 0;
        nemici = 0;
        asteroidi = 0;
        maxNemici = 3;
        playerEnergy = 100;
        playerLife = 3;
        rebirthCD = 3;
        maxAsteroidi = 1;
        asteroideCD = 45;
        audioS = GetComponent<AudioSource>();
        untilBoss = 180f;

        laserHitClip = Resources.Load("laserhit_player_sshooter") as AudioClip;
        enemydeath = Resources.Load("normenemy_death_sshooter") as AudioClip;
        shieldhit = Resources.Load("shieldhit_sshooter") as AudioClip;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Scene currentScene = SceneManager.GetActiveScene();
        //if (currentScene.buildIndex == 0)
        //{
        //    Destroy(gameObject);
        //}
        ExitLevel();
        numNemici = FindObjectsOfType<NemicoNormale>().Length;

        asteroideCD -= Time.deltaTime;
        if (!isBossTime)
        {
            untilBoss -= Time.deltaTime;
        }
        

        if (!starship.activeSelf && !gameOverCanvas.activeSelf)
        {
            rebirthCD -= Time.deltaTime;
            if (rebirthCD < 0)
            {
                playerLife = 3;
                testoLife.text = "LifePoints: " + playerLife;
                starship.SetActive(true);
                rebirthCD = 3;
            }
        }

        if (playerEnergy <= 0)
        {
            scudo.SetActive(false);
        }

        if (untilBoss <= 0 && !isBossTime)
        {
            spwanSpec.callBoss = true;
            isBossTime = true;
            untilBoss = 300f;
        }
    }

    public void AggiungiPunti(int nuoviPunti)
    {
        punti = punti + nuoviPunti;
        testoPunti.text = "Punti: " + punti;
    }

    public void AumentaNemici()
    {
        nemici++;
    }

    public void DiminuisciNemici()
    {
        nemiciUccisi++;
        if (nemiciUccisi == 10)
        {
            spwanSpec.GeneraSpeciale();
            nemiciUccisi = 0;
        }
    }

    public void DiminuisciEnergia(int energiaPersa)
    {
        playerEnergy = playerEnergy - energiaPersa;
        if (playerEnergy < 0)
        {
            playerEnergy = 0;
        }
        testoEnergia.text = "Energia: " + playerEnergy;
    }

    public void AumentaEnergia(int nuovaEnergia)
    {
        playerEnergy = playerEnergy + nuovaEnergia;
        if (!scudo.activeSelf)
        {
            scudo.SetActive(true);
        }
        testoEnergia.text = "Energia: " + playerEnergy;
    }

    public void DiminuisciLife(int lifePersa)
    {
        playerLife = playerLife - lifePersa;
        if (playerLife < 0)
        {
            playerLife = 0;
        }
        testoLife.text = "LifePoints: " + playerLife;
    }

    public void AumentaLife(int nuovaLife)
    {
        playerLife = playerLife + nuovaLife;
        testoLife.text = "LifePoints: " + playerLife;
    }

    public void AumentaAsteroide()
    {
        asteroidi++;
    }

    public void DiminuisciAsteroide()
    {
        asteroidi--;
        asteroideCD = 45;
    }

    public bool ilGiocatoreEVivo()
    {
        if (playerLife > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PossoMandareUnNuovoNemico()
    {
        if (numNemici < 3 && !isBossTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlayerExplosion()
    {
        audioS.PlayOneShot(playerExplo);
    }

    public bool PossoMandareAsteroide()
    {
        if (asteroidi < maxAsteroidi && asteroideCD <= 0 && !isBossTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        starship.SetActive(false);
    }

    public void PlayExplosion()
    {
        audioS.clip = laserHitClip;
        audioS.Play();
    }

    public void PlayDeathSound()
    {
        audioS.clip = enemydeath;
        audioS.Play();
    }

    public void PlayShieldHit()
    {
        audioS.clip = shieldhit;
        audioS.Play();
    }

    public void ExitLevel()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameExitCanvas.activeSelf)
        {
            gameExitCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameExitCanvas.activeSelf)
        {
            gameExitCanvas.SetActive(false);
        }
    }
}
