using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float detectionRadius = 2f;
    public LayerMask InteractableMask;
    public Interactable currentTarget;
    public TMP_Text TargetNameText;
    Bandana bandana;

    private void Start()
    {
        bandana = GetComponentInChildren<Bandana>();
        bandana.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="bandana")
        {
            Destroy(other.gameObject);
            FindObjectOfType<ObjectiveSystem>().CompleteObjective(0);
            bandana.gameObject.SetActive(true);
        }
    }
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius,InteractableMask);
        if (colliders.Length == 0)
            currentTarget = null;
        else
        {
            currentTarget = colliders[0].GetComponent<Interactable>();
            foreach (Collider collider in colliders)
            {
                if (Vector3.Distance(transform.position, collider.transform.position) < Vector3.Distance(transform.position, currentTarget.transform.position))
                {
                    currentTarget = collider.GetComponent<Interactable>();
                }
                    
            
            }
        }
        if (currentTarget != null)
        {
            Debug.Log(currentTarget.gameObject.name);
            TargetNameText.text = currentTarget.Name;
        }
        else
        {
            TargetNameText.text = "";
        }

        if(Input.GetMouseButtonDown(0) && currentTarget!=null)
        {
            currentTarget.Interact();
        }
    }


}
