using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Sprite closedChest;
    private bool _isPickingUp = false;
    private bool chestHasNotBeenOpened = true;
    private bool insideItem = false;
    private GameObject gameObjectTouchingMe;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Can interact with item");
        insideItem = true;
        gameObjectTouchingMe = col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Item is gone");
        insideItem = false;
    }
    void Update()
    {
        if (insideItem) {

        if (gameObjectTouchingMe.tag == "Player" && Input.GetKey(KeyCode.F) && !_isPickingUp)
        {
            _isPickingUp = true;
            switch(gameObject.tag)
            {
                case "HeartItem":
                Player.currHealth = Mathf.Min(Player.currHealth + 5, Player.maxHealth);
                Destroy(gameObject);
                break;

                case "Chest":
                if (chestHasNotBeenOpened) {
                StartCoroutine("OpenChest");
                chestHasNotBeenOpened = false;
                }
                break;
            }
        }
        }
    }
    IEnumerator OpenChest() 
    {
        gameObject.GetComponent<Animator>().SetBool("IsOpening", true);
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<Animator>().SetBool("IsOpening", false);
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = closedChest;
    }
}
