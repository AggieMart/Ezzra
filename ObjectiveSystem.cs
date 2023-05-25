using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveSystem : MonoBehaviour
{

    public GameObject WinScreen;

    public TMP_Text[] ObjectiveTexts;
    public bool[] ObjectivesCompleted;
    //cloth
    //door
    //electronics
    //water
    //phone
    public Interactable[] Doors;
    int DoorsClosed;
    int MaxDoors;

    public Interactable[] Electronics;
    int ElecOff;
    int MaxElec;
    public Camera WonCamera;
    // Start is called before the first frame update
    void Start()
    {
        WonCamera.enabled = false;
        DoorsClosed = 0;
        ElecOff = 0;
        MaxDoors = Doors.Length;
        MaxElec = Electronics.Length;
        ObjectivesCompleted = new bool[ObjectiveTexts.Length];
    }

    // Update is called once per frame
    void Update()
    {
        ObjectiveTexts[1].text = "close all doors " + DoorsClosed.ToString()+ "/" + MaxDoors.ToString();
        ObjectivesCompleted[1] = (DoorsClosed == MaxDoors);
        ObjectiveTexts[2].text = "turn off electronics " + ElecOff.ToString()+ "/" + MaxElec.ToString();
        ObjectivesCompleted[2] = (ElecOff == MaxElec);
        for (int i = 0; i < ObjectiveTexts.Length; i++)
        {
            Color TargetColor = ObjectivesCompleted[i] ? Color.green : Color.white;
            ObjectiveTexts[i].color = Color.Lerp(ObjectiveTexts[i].color, TargetColor, 10 * Time.deltaTime);

        }
        CheckDoors();
        CheckElec();
        CheckWon();
    }


    public void CompleteObjective(int i)
    {
        ObjectivesCompleted[i] = true;
        CheckWon();
    }

    void CheckWon()
    {
        bool won = true;
        for (int i = 0; i < ObjectiveTexts.Length; i++)
        {
            if (!ObjectivesCompleted[i])
                won = false;

        }
        if (won)
        {
            WonCamera.enabled = true;
            GameManager.GameStarted = false;
            WinScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    void CheckDoors()
    {
        int d = 0;
        foreach (var door in Doors)
        {
            d += door.IsOpen ? 0 : 1;
        }
        DoorsClosed = d;
    }

    void CheckElec()
    {
        int d = 0;
        foreach (var elec in Electronics)
        {
            d += elec.TurnedOn ? 0 : 1;
        }
        ElecOff = d;
    }
}
