using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    public Dare[] daresForOnePlayer;
    public Dare[] daresForTwoPlayer;
    public Dare[] daresForAllPlayer;
    private List<Dare> unmakingDaresForOne;
    private List<Dare> unmakingDaresForTwo;
    private List<Dare> unmakingDaresForAll;
    private List<string> players = new List<string>();
    private List<string> unPlayedPlayers;

    private Dare currentDare;
    private int curRandomDareIndex = 0;
    private int curPlayerIndex = 0;

    [SerializeField]
    private TMP_Text dareText;

    [SerializeField]
    private float timeBetweenDares = 0.2f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private TMP_InputField addNewPlayerInputField;

    [SerializeField]
    private TMP_Text playersListText;

    [SerializeField]
    private GameObject uiMainMenu;
    bool isActive = true;

    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private GameObject fakeButton;
    [SerializeField]
    private TMP_Text warningText;
    [SerializeField]
    private GameObject addingPlayerNameLenghtLimitWarning;

    private void Start()
    {
        uiMainMenu.SetActive(isActive);
    }

    private void Update()
    {
        uiMainMenu.SetActive(isActive);

        if (!isActive)
        {
            if (unmakingDaresForOne == null || unmakingDaresForOne.Count == 0)
            {
                unmakingDaresForOne = daresForOnePlayer.ToList<Dare>();
            }

            if (unmakingDaresForTwo == null || unmakingDaresForTwo.Count == 0)
            {
                unmakingDaresForTwo = daresForTwoPlayer.ToList<Dare>();
            }

            if (unmakingDaresForAll == null || unmakingDaresForAll.Count == 0)
            {
                unmakingDaresForAll = daresForAllPlayer.ToList<Dare>();
            }

            if (unPlayedPlayers == null || unPlayedPlayers.Count == 0)
            {
                unPlayedPlayers = new List<string>(players);
            }
        }
    }


    string getPlayer()
    {
        if (unPlayedPlayers == null || unPlayedPlayers.Count == 0)
        {
            return "!";
        }

        curPlayerIndex = Random.Range(0, unPlayedPlayers.Count);
        string curPlayer = unPlayedPlayers[curPlayerIndex];
        unPlayedPlayers.RemoveAt(curPlayerIndex);
        return curPlayer;
    }


    private string playWithOnePlayer()
    {
        string curDare = "";
        curRandomDareIndex = Random.Range(0, unmakingDaresForOne.Count);
        currentDare = unmakingDaresForOne[curRandomDareIndex];
        curDare = ReplacePlaceholder(currentDare._dare, getPlayer());
        unmakingDaresForOne.RemoveAt(curRandomDareIndex);
        return curDare;
    }

    private string playWithTwoPlayers()
    {
        string curDare = "";
        curRandomDareIndex = Random.Range(0, unmakingDaresForTwo.Count);
        currentDare = unmakingDaresForTwo[curRandomDareIndex];
        curDare = ReplacePlaceholderTwo(currentDare._dare, getPlayer(), getPlayer());
        unmakingDaresForTwo.RemoveAt(curRandomDareIndex);
        return curDare;
    }

    private string playWithAllPlayers()
    {
        string curDare = "";
        curRandomDareIndex = Random.Range(0, unmakingDaresForAll.Count);
        currentDare = unmakingDaresForAll[curRandomDareIndex];
        curDare = currentDare._dare;
        unmakingDaresForAll.RemoveAt(curRandomDareIndex);
        return curDare;
    }

    void setCurrentDare()
    {
        string curDare = "";

        if (unPlayedPlayers.Count >= 2)
        {
            int randomNumber = GenerateBiasedRandom();
            if (randomNumber == 1)
            {
                curDare = playWithOnePlayer();
            }
            else if (randomNumber == 2)
            {
                curDare = playWithTwoPlayers();
            }
            else
            {
                curDare = playWithAllPlayers();
            }
        }
        else
        {
            curDare = playWithOnePlayer();
        }

        dareText.text = curDare;
    }

    IEnumerator transitionToNextDare()
    {
        yield return new WaitForSeconds(timeBetweenDares);

        setCurrentDare();
        animator.SetTrigger("Return");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void goNextDare()
    {
        animator.SetTrigger("Clicked");

        StartCoroutine(transitionToNextDare());
    }

    public void getNewPlayerName()
    {
        string playerName = addNewPlayerInputField.text;


        if (playerName.Length > 15 || playerName.Any(c => char.IsWhiteSpace(c)))
        {
            addingPlayerNameLenghtLimitWarning.SetActive(true);
            addNewPlayerInputField.text = "";
            addNewPlayerInputField.Select();
            return;
        }
        addingPlayerNameLenghtLimitWarning.SetActive(false);

        if (!string.IsNullOrWhiteSpace(playerName))
        {
            if (players.Count < 32)
            {
                warningText.text = "";
                if (!players.Contains(playerName))
                {
                    players.Add(playerName);
                    warningText.text = "";
                }
                else
                {
                    warningText.text = playerName + " is already entered!";
                }
            }
            else
            {
                warningText.text = "The number of players can be a maximum of 32!";
            }
        }

        addNewPlayerInputField.text = "";
        addNewPlayerInputField.Select();

        showAllPlayers();

        if (players.Count >= 2)
        {
            fakeButton.SetActive(false);
            startButton.SetActive(true);
        }
    }

    public void showAllPlayers()
    {
        string allPlayersString = "";

        for (int i = 0; i < players.Count; ++i)
        {
            allPlayersString += (i + 1 + "." + players[i] + "   ");
        }

        playersListText.text = allPlayersString;
    }


    public void startGame()
    {
        isActive = false;
        uiMainMenu.SetActive(isActive);
    }

    public void clickFakeButton()
    {
        warningText.text = "Enter 2+ Players To Start";
    }

    int GenerateBiasedRandom()
    {
        int randomValue = Random.Range(0, 100);

        // 1: 60% -> 0-59
        // 2: 30% -> 60-89
        // 3: 10% -> 90-99

        if (randomValue < 60)
        {
            return 1; 
        }
        else if (randomValue < 90)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    private string ReplacePlaceholder(string input, string replacement)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(replacement))
        {
            return input;
        }

        return input.Replace("_1", replacement);
    }

    private string ReplacePlaceholderTwo(string input, string replacementOne, string replacementTwo)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(replacementOne) || string.IsNullOrEmpty(replacementTwo))
        {
            return input;
        }

        input = input.Replace("_1", replacementOne);

        input = input.Replace("_2", replacementTwo);

        return input;
    }




}
