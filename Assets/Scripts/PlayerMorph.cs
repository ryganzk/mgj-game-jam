using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMorph : MonoBehaviour
{
    public GameObject BaseForm;
    public GameObject[] Forms;
    private int CurrentForm = 0;

    private PlayerControls Controls;
    private bool IsTransformed = false;

    private void Awake() 
    {

        Controls = new PlayerControls();

        Controls.Gameplay.MorphScroll_L.performed += ctx => {
            if(!IsTransformed)
            {
                Debug.Log("Left");
                ScrollLeft();
            }
        };

        Controls.Gameplay.MorphScroll_R.performed += ctx => {
            if(!IsTransformed)
            {
                Debug.Log("Right");
                ScrollRight();
            }
        };

        Controls.Gameplay.Morph.performed += ctx => {
            Morph();
        };
    }

    private void OnEnable() 
    {
        Controls.Gameplay.Enable();
    }

    private void OnDisable() 
    {
        Controls.Gameplay.Disable();
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
        if(IsTransformed)
        {
            BaseForm.SetActive(true);
            BaseForm.transform.position = Forms[CurrentForm].transform.position;
            BaseForm.GetComponent<Rigidbody2D>().velocity = Forms[CurrentForm].GetComponent<Rigidbody2D>().velocity;
            Forms[CurrentForm].SetActive(false);
            IsTransformed = false; 
        }
        else
        {
            Forms[CurrentForm].SetActive(true);
            Forms[CurrentForm].transform.position = BaseForm.transform.position;
            Forms[CurrentForm].GetComponent<Rigidbody2D>().velocity = BaseForm.GetComponent<Rigidbody2D>().velocity;
            BaseForm.SetActive(false);
            IsTransformed = true; 
        }
    }
}
