using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpVisualManager : MonoBehaviour
{

    public Transform player;

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x - 2f, player.position.y + 1.5f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
