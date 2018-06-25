using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>() != null) {
            GameManager.ins.EndGame();
        }
    }
}
