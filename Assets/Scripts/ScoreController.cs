using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public GameObject player;
    public Animator UIBGAnimator;
    public TMP_Text textComponent;
    private bool animPlayed;

    public int deaths, wins;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        Debug.Log("Loaded");
            deaths = PlayerPrefs.GetInt("deaths");
            wins = PlayerPrefs.GetInt("wins");
    }

    private void Update()
    {
        textComponent.SetText($"Wins: {wins}\nDeaths: {deaths}");
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !animPlayed)
        {
            animPlayed = true;
            UIBGAnimator.SetTrigger("Win");
            wins++;
            PlayerPrefs.SetInt("wins", wins);
            PlayerPrefs.Save();
            Debug.Log("Saved");
            StartCoroutine(LevelReload());
        }
        if(player.GetComponent<PlayerMotor>().health <= 0 && !animPlayed)
        {
            animPlayed = true;
            player.GetComponent<Animator>().SetTrigger("death");
            player.GetComponent<PlayerMotor>().enabled = false;
            UIBGAnimator.SetTrigger("Lose");
            deaths++;
            PlayerPrefs.SetInt("deaths", deaths);
            Debug.Log("Saved");
            PlayerPrefs.Save();
            StartCoroutine(LevelReload());

        }
        IEnumerator LevelReload()
         {
             yield return new WaitForSeconds(2f);
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // EXZ_EXZ comment
         }
    }

}
