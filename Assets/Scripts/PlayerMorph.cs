using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMorph : MonoBehaviour
{
    public GameObject BaseForm;
    public GameObject[] Forms;
    private int CurrentForm = 0;

    private PlayerControls controls;
    private bool isTransformed = false;

    private void Awake() 
    {

        controls = new PlayerControls();

        controls.Gameplay.MorphScroll_L.performed += ctx => {
            if(!isTransformed)
            {
                Debug.Log("Left");
                ScrollLeft();
            }
        };

        controls.Gameplay.MorphScroll_R.performed += ctx => {
            if(!isTransformed)
            {
                Debug.Log("Right");
                ScrollRight();
            }
        };

        controls.Gameplay.Morph.performed += ctx => {
            Morph();
        };
    }

    private void OnEnable() 
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable() 
    {
        controls.Gameplay.Disable();
    }

    private void ScrollLeft()
    {
        CurrentForm -= 1;
        if(CurrentForm < 0)
        {
            CurrentForm = Forms.Length - 1;
        }
    }

    public string GetCurrentFormName()
    {
        return Forms[CurrentForm].name;
    }

    private void ScrollRight()
    {
        CurrentForm += 1;
        if(CurrentForm >= Forms.Length)
        {
            CurrentForm = 0;
        }
    }

    private void Morph()
    {
        if(isTransformed)
        {
            BaseForm.SetActive(true);
            BaseForm.transform.position = Forms[CurrentForm].transform.position;
            BaseForm.GetComponent<Rigidbody2D>().velocity = Forms[CurrentForm].GetComponent<Rigidbody2D>().velocity;
            Forms[CurrentForm].SetActive(false);
            isTransformed = false; 
        }
        else
        {
            Forms[CurrentForm].SetActive(true);
            Forms[CurrentForm].transform.position = BaseForm.transform.position;
            Forms[CurrentForm].GetComponent<Rigidbody2D>().velocity = BaseForm.GetComponent<Rigidbody2D>().velocity;
            BaseForm.SetActive(false);
            isTransformed = true; 
        }
    }
}
