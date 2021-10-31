using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class BoxReceiver : MonoBehaviour
{
    [SerializeField] private BoxType type;
    [SerializeField] private BoxSpawner spawner;
    [SerializeField] private TextMesh text;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Box"))
            return;

        var box = other.GetComponent<Box>();

        Debug.Log(box.Type);

        if (box.Type == type)
        {
            SetText("Nice!");
            Destroy(other.gameObject);
            spawner.SpawnBox();
        }
        else
            SetText("Dumb!");
    }

    private void SetText(string message)
    {
        text.text = message;
        Invoke(nameof(ClearText), 2);
    }

    private void ClearText() => text.text = "";
}
