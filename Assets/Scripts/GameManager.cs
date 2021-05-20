using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private CivilianPatrol[] civilian;
    public int civilianCount;

    [SerializeField] GameObject lift = null,joystick;

    public bool isGameOver = false;
    PlayerController player;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        civilian = FindObjectsOfType<CivilianPatrol>();
        foreach (var civ in civilian)
        {
            civilianCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(civilianCount < 1)
        {
            OpenLiftAnim();
        }

        if (isGameOver)
        {
            Invoke("ShowGameOver", 0.3f);

        }
    }

    public void OpenLiftAnim()
    {
        lift.GetComponent<Animator>().SetTrigger("OpenLift");
        lift.GetComponent<BoxCollider>().enabled = false;
    }

    public void CloseLiftAnim()
    {
        lift.GetComponent<Animator>().SetTrigger("CloseLift");
        player.moveInput = Vector3.zero;
       // joystick.SetActive(false);
       
        UiControls.instance.ShowGameWinPanel();
    }

    void ShowGameOver()
    {
        UiControls.instance.ShowGameOverPanel();
    }

    
    
}
