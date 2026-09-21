using UnityEngine;
using System.Collections.Generic;
public class Zoo : MonoBehaviour


{
    public List<string> animals;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animals.Add("Penguin");
        animals.Add("Big Dinosaur");
        animals.Add("Sharks");

        animals.Remove("Big Dinosaur");

        //Lists are 0-indexed this is the identifier that says where this element is
        //in the list and it starts at 0 because programmers suck :(
        //Tiger [0]
        //Penguin [1]
        //Sharks [2]
        Debug.Log("Our second animal is:");
        Debug.Log(animals[1]);

        foreach (string currentAnimal in animals)
        {
            Debug.Log("Our next animal is: " + currentAnimal);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
