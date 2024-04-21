using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreMaster : MonoBehaviour
{

    public scoreText[]  scores;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    static int[] GetDigits(int number)
    {
        // Convert the number to a string
        string numberString = number.ToString();

        // Create an array to store the digits
        int[] digits = new int[numberString.Length];

        // Iterate over each character of the string
        for (int i = 0; i < numberString.Length; i++)
        {
            // Convert each character back to an integer and store it in the array
            digits[i] = int.Parse(numberString[i].ToString());
        }

        return digits;
    }

    public void presentScore(int n){
        int[] digits = GetDigits(n);
        Debug.Log("digits are " + digits);
        for (int i = 0; i < digits.Length; i++)
        {
            Debug.Log(digits[i]);
            scores[i].incrementScore(digits[i]);
        }
    }
}
