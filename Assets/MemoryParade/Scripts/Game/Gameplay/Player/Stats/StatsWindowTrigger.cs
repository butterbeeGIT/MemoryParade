using Assets.MemoryParade.Scripts.Game.GameRoot;
using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsWindowTrigger: MonoBehaviour
{
    private GameObject StatCanvas;
    private CharacterMove playerMove;
    private Transform mirror;
    private bool inMirror = false;
    //private SpriteRenderer player;

    void Start()
    {
        StatCanvas = GameObject.Find("StatCanvas");
        playerMove = FindObjectOfType<CharacterMove>();
        //player = GameObject.Find("testPerson_0").GetComponent<SpriteRenderer>();
        StatCanvas.SetActive(false);
        mirror = GetComponent<Transform>();
    }

    void Update()
    {
        if (!inMirror && Vector2.Distance(playerMove.transform.position, mirror.transform.position) <= 0.5f)
        {
            StartStat();
        }
        if (inMirror)
        {
            StatCanvas.SetActive(false);
            playerMove.enabled = true;
            if (Vector2.Distance(playerMove.transform.position, mirror.transform.position) > 1f)
            {
                inMirror = false;
            }
        }
    }

    void StartStat()
    {
        StatCanvas.SetActive(true);
        playerMove.enabled = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inMirror = true;
        }
    }
}

