using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using OVR.OpenVR;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text WinText;

    private bool button;
    private Rigidbody rb;
    private int count;

    public bool Switch1;
    public bool Switch2;
    public bool Switch3;

    public bool Trigger1;
    public bool Trigger2;
    public bool Trigger3;
    public bool Trigger4;
    public int PuzzCount;
    public bool status;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        WinText.text = "";
        button = false;
        Switch1 = false;
        Switch2 = true;
        Switch3 = false;

        Trigger1 = false;
        Trigger2 = false;
        Trigger3 = false;
        Trigger4 = false;
        PuzzCount = 1;
        status = false;
}

    void FixedUpdate()
    {
        OVRInput.FixedUpdate();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

        }

        if (other.gameObject.CompareTag("Door") && count >= 12)
        {
            other.gameObject.SetActive(false);

        }


        if (other.gameObject.CompareTag("Reset"))
        {
            count = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if (other.gameObject.CompareTag("Laser Button"))
        {
            if (!button)
            {
                button = true;
                other.gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                button = false;
                other.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }


        }

        if (other.gameObject.CompareTag("Door1") && button)
        {
            other.gameObject.SetActive(false);

        }

        if (other.gameObject.CompareTag("Switch"))
        {
            if(other.gameObject.GetComponent<Renderer>().material.color == Color.red)
            {
                other.gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                other.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }



        if (other.gameObject.CompareTag("Switch1"))
        {
            Switch1 = !Switch1;
            Switch3 = !Switch3;
        }

        if (other.gameObject.CompareTag("Switch2"))
        {
            Switch1 = !Switch1;
            Switch2 = !Switch2;
        }

        if (other.gameObject.CompareTag("Switch3"))
        {
            Switch2 = !Switch2;
            Switch3 = !Switch3;
        }

        if (other.gameObject.CompareTag("Door2") && solved1())
        {
            other.gameObject.SetActive(false);

        }

        if (other.gameObject.CompareTag("Trigger"))
        {
           

            if (status)
            {
                if (other.gameObject.GetComponent<Renderer>().material.color == Color.blue)
                {
                    other.gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if (other.gameObject.GetComponent<Renderer>().material.color == Color.green)
                {
                    other.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                }
                else if (other.gameObject.GetComponent<Renderer>().material.color == Color.yellow)
                {
                    other.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                }
                else if (other.gameObject.GetComponent<Renderer>().material.color == Color.magenta)
                {
                    other.gameObject.GetComponent<Renderer>().material.color = Color.black;
                }
            }
            
            else
            {
                other.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
        }


        if (other.gameObject.CompareTag("Trigger1") && CheckStatus(1))
        {
            Trigger1 = true;
            PuzzCount++;
        }

        if (other.gameObject.CompareTag("Trigger2") && CheckStatus(2))
        {
            Trigger2 = true;
            PuzzCount++;
        }


        if (other.gameObject.CompareTag("Trigger3") && CheckStatus(3))
        {
            Trigger3 = true;
            PuzzCount++;
        }


        if (other.gameObject.CompareTag("Trigger4") && CheckStatus(4))
        {
            Trigger4 = true;
            PuzzCount++;
        }

        if (other.gameObject.CompareTag("Door3") && solved2())
        {
            other.gameObject.SetActive(false);

        }



    }
         

    bool CheckStatus(int x)
    {
        status = false;
        switch (x)
        {
            case 1:
                if (!Trigger2 && !Trigger3 && !Trigger4)
                    status = true;
                break;
            case 2:
                if (Trigger1 && !Trigger3 && !Trigger4)
                    status = true;
                break;
            case 3:
                if (Trigger1 && Trigger2 && !Trigger4)
                    status = true;
                break;
            case 4:
                if (Trigger1 && Trigger2 && Trigger3)
                    status = true;
                break;
        }
        if (!status) { reset();}

        return status;
    }

    void reset()
    {
        PuzzCount = 1;
        Trigger1 = false;
        Trigger2 = false;
        Trigger3 = false;
        Trigger4 = false;
    }

    bool solved2()
    {
         if(Trigger1 && Trigger2 && Trigger3 && Trigger4)
        {
            return true;
        }
        return false;
}



    bool solved1()
    {
        if(Switch1 && Switch2 && Switch3)
        {
            return true;
        }
        return false;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            WinText.text = "You Win!";
        }
    }

}

