using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Random = System.Random;

public class BoxSpawner : MonoBehaviour
{
    private readonly Random random = new Random();

    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] Material boxMaterial;
    [SerializeField] private Box[] boxes;

    private void Awake() => Invoke(nameof(SpawnBox), 0.5f);

    public void SpawnBox()
    {
        var boxIndex = random.Next(0, boxes.Length);
        var boxType = random.Next(0, 2);

        var box = Instantiate(boxes[boxIndex], spawnPoint, Quaternion.identity);
        box.Type = (BoxType)boxType;
        SetBoxColor(box.Type);
    }

    private void SetBoxColor(BoxType boxType)
    {
        Color color;
        switch (boxType)
        {
            case BoxType.Green:
                color = new Color(0.5411764705882353f, 1.0f, 0.5333333333333333f);
                break;
            default:
                color = new Color(1.0f, 0.6274509803921569f, 0.6196078431372549f);
                break;
        }

        boxMaterial.color = color;
    }
}