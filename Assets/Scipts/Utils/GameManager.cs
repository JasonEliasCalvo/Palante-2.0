using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void DelegatedGameStates();
    public DelegatedGameStates eventInitialGameStart;
    public DelegatedGameStates eventInitialGameEnd;
    public DelegatedGameStates eventTypingGameStart;
    public DelegatedGameStates eventTypingGameReset;
    public DelegatedGameStates eventTypingGameEnd;
    public DelegatedGameStates eventSyllableGameStart;
    public DelegatedGameStates eventSyllableGameEnd;
    public static GameManager instance;

    private Timer timer;
    [SerializeField] float initiateTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GamePrepate();
    }

    public void GamePrepate()
    {
        timer = FindObjectOfType<Timer>();
        Invoke(nameof(InitialGameStart), 0.2f);
    }

    public void InitialGameStart()
    {
        eventInitialGameStart?.Invoke();
    }

    public void InitialGameEnd()
    {
        eventInitialGameEnd?.Invoke();
    }

    public void GamePause()
    {

    }
    public void GameResume()
    {

    }

    public void TypingGameStart()
    {
        timer.eventEndTime += ResetTypingGame;
        timer.Initiate(initiateTime);
        eventTypingGameStart?.Invoke();
    }

    public void ResetTypingGame()
    {
        eventTypingGameReset?.Invoke();
    }

    public void TypingGameEnd()
    {
        eventTypingGameEnd?.Invoke();
    }

    public void SyllableGameStart()
    {
        eventSyllableGameStart?.Invoke();
    }

    public void SyllableGameEnd()
    {
        eventSyllableGameEnd?.Invoke();
    }

    public Timer GetTimer()
    {
        return timer;
    }
}
