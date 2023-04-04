using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Castle : DistanceOnClick {
    public static readonly string Name = "Castle";

    protected override void OnClick() {
        Vector2 selfPosition = transform.position;
        //Напиши код для поиска расстояния до всех объектов и вывода их в дэбаг лог

        for (int i = 0; i < ObjectsList.Count; i++)
        {
            if (adjustedNodes[4, i] != 0)
            {
                Debug.Log($"{transform.name} : {ObjectsList[i].name} : {Vector2.Distance(selfPosition, ObjectsList[i].position)}");
            }
        }
    }
}
