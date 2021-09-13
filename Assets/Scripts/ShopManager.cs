using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Material playerMat;

    public void PurchasePlayerColor(string productName)
    {
        // It is controlled according to the names coming from the buttons and the color changes are made.
        switch (productName)
        {
            case "redBall":
                playerMat.color = Color.red;
                break;
            case "blueBall":
                playerMat.color = Color.blue;
                break;
            case "purpleBall":
                playerMat.color = Color.magenta;
                break;
            default:
                break;
        }
    }
}
