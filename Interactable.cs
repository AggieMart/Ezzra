using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum InteractType
{
    door,
    basket,
    electronic,
    cloth,
    phone
}
public class Interactable : MonoBehaviour
{
    public string Name;
    public InteractType type;
    [Header("if object is a door")]
    public Vector3 DoorOpenRotation;
    public Vector3 DoorClosedRotation;
    public bool IsOpen;




    [Header("if object is a basket")]
    public GameObject BasketText;
    public GameObject hand;
    public bool OnHand;

    [Header("if object is n Electronic")]
    public bool TurnedOn=true;



    // Start is called before the first frame update
    void Start()
    {
        OnHand = false;
        //BasketText.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if(type==InteractType.door)
        {
            transform.localRotation = Quaternion.Lerp(transform.rotation,IsOpen?Quaternion.Euler(DoorOpenRotation): Quaternion.Euler(DoorClosedRotation), Time.deltaTime*3);
        }
        if (OnHand)
        {
            transform.position = hand.transform.position +  new Vector3(0, -1, 0);
            
        }
        
    }

    public void Interact()
    {
        if(type == InteractType.door)
        {
            //door interaction function
            ChangeDoorState();
            print("interacted with door");
        }
        if(type==InteractType.basket)
        {

            //basket interaction function
            if (OnHand == false)
            {
                Takebasket();
            }
            else 
            {
                Dropbasket();
            }
        }
        if (type == InteractType.electronic)
        {
            //door interaction function
            TurnOffElectronics();
            print("interacted with switch");
        }
        if (type == InteractType.phone)
        {
            //door interaction function
            UsePhone();
        }
    }

    void ChangeDoorState()
    {
        IsOpen = !IsOpen;
    }
    void TurnOffElectronics()
    {
        TurnedOn = false;
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
    }
    void Takebasket()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        OnHand = true;
        BasketText.SetActive(false);
        //BasketText.SetActive(false);
    }
    void Dropbasket()
    {
        OnHand = false;
        FindObjectOfType<ObjectiveSystem>().CompleteObjective(3);
        Destroy(gameObject);
        //transform.localPosition =  new Vector3(3.5f, 2f, 15.5f);

        //BasketText.SetActive(true);
    }


    void UsePhone()
    {
        FindObjectOfType<ObjectiveSystem>().CompleteObjective(4);
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
    }
}
