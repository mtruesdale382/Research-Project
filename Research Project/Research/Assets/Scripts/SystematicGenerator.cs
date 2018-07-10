using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SystematicGenerator : MonoBehaviour
{
    public GameObject objectprefab; //prefab to spawn

    GameObject currentObject; //refers to current object being edited

    int count = 0;

    public Transform[] spawnPoints;

    private void Update()
    {
        if (count < 5)
        {
            spawn();
            count++;
        }
    }

    public void Randomize(GameObject current)
    {
        BaseObject bobject = current.GetComponent<BaseObject>();

        Color currentColor = Color.white;

        if (Random.Range(0, 9) < 3) //gives a 30% chance that the hopping will occur when randomized
        {
            bobject.isHopping = true;
        }
        else
        {
            bobject.isHopping = false;
        }

        if (Random.Range(0, 9) < 5) //gives a 50% chance that the walking will occur when randomized
        {
            bobject.isWalking = true;
        }
        else
        {
            bobject.isWalking = false;
        }

        currentColor = Random.ColorHSV();

        foreach (Renderer r in current.GetComponentsInChildren<Renderer>()) //gets children in current object
        {
            r.material.color = currentColor; //changes color to current color
        }

        bobject.currentHeight = Random.Range(1, 5);
        bobject.currentWidth = Random.Range(1, 3);
    }

    public void nextScene()
    {
        SceneManager.LoadScene("Test Scene");
    }

    void spawn()
    {
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        currentObject = Instantiate(objectprefab, spawnPoints[count].position, spawnPoints[count].rotation);

        Randomize(currentObject);
    }
}
