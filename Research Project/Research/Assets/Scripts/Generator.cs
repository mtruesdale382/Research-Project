using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour
{
    public Slider heightSlider;
    public Slider widthSlider;

    public Toggle Walking;
    public Toggle Hopping;

    public GameObject currentObject; //refers to current object being edited
    BaseObject bobject;

    public Color currentColor;
    public Color nextColor;
    Color startingColor;

    public RawImage colorDisplayCurrent;
    public RawImage colorDisplayNext;

    float startingHeight;
    float startingWidth;

    Vector3 startLocation;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        startLocation = currentObject.GetComponent<Transform>().position;

        startingColor = currentObject.GetComponentInChildren<Renderer>().material.color;
        currentColor = startingColor;

        startingHeight = heightSlider.value;
        startingWidth = widthSlider.value;

        bobject = currentObject.GetComponent<BaseObject>(); //gets base object script from current object

        nextColor = Random.ColorHSV();

        bobject.currentHeight = startingHeight;
        bobject.currentWidth = startingWidth;
    }

    private void Update()
    {
        //takes slider values and adjusts the current objects values to the value of slider
        bobject.currentHeight = heightSlider.value;
        bobject.currentWidth = widthSlider.value;

        colorDisplayCurrent.GetComponent<RawImage>().color = currentColor;
        colorDisplayNext.GetComponent<RawImage>().color = nextColor;

        foreach (Renderer r in currentObject.GetComponentsInChildren<Renderer>()) //gets children in current object
        {
            r.material.color = currentColor; //changes color to current color
        }

        if (Walking.isOn)
        {
            bobject.isWalking = true;
        }
        else
        {
            bobject.isWalking = false;
        }

        if (Hopping.isOn)
        {
            bobject.isHopping = true;
        }
        else
        {
            bobject.isHopping = false;
        }

        if (bobject.isWalking == false)
        {
            currentObject.transform.position = startLocation;
        }
    }

    public void ColorChange()
    {
        currentColor = nextColor; //sets current color to random color       
        nextColor = Random.ColorHSV();
    }

    public void Randomize()
    {
        bobject.transform.position = startLocation;

        currentColor = startingColor;

        bobject.currentHeight = startingHeight;
        bobject.currentWidth = startingWidth;

        bobject.isHopping = false;
        bobject.isWalking = false;

        if (Random.Range(0, 9) < 3) //gives a 30% chance that the hopping will occur when randomized
        {
            Hopping.isOn = true;
        }
        else
        {
            Hopping.isOn = false;
        }

        if (Random.Range(0, 9) < 5) //gives a 50% chance that the walking will occur when randomized
        {
            Walking.isOn = true;
        }
        else
        {
            Walking.isOn = false;
        }

        currentColor = Random.ColorHSV();

        bobject.currentHeight = Random.Range(1, 5);
        bobject.currentWidth = Random.Range(1, 3);

        widthSlider.value = bobject.currentWidth;
        heightSlider.value = bobject.currentHeight;
    }

    public void jumping(bool newValue)
    {
        bobject.isHopping = newValue;
    }

    public void walking(bool newValue)
    {
        bobject.isWalking = newValue;
    }

    public void nextScene()
    {
        SceneManager.LoadScene("Test Scene");
    }
}
