using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class ArcherArena : DistanceOnClick {

    public static readonly string Name = "ArcherArena";
    
    protected override void OnClick() {
        Vector2 selfPosition = transform.position;

        //Пример поиска расстояния от арены лучников до ворот 
        //Vector2 GatePosition = GameObject.Find("Gate").transform.position;
        //Debug.Log("Distance from " + Name + " to " + Gate.Name + " equals to " + Vector2.Distance(selfPosition, GatePosition));
        //Напиши код для поиска расстояния до всех объектов и вывода их в дэбаг лог


        for (int i = 0; i < ObjectsList.Count; i++)
        {
            if (adjustedNodes[3, i] != 0)
            {
                Debug.Log($"{transform.name} : {ObjectsList[i].name} : {Math.Round(Vector2.Distance(selfPosition, ObjectsList[i].transform.position), 2)}");
            }
        }
    }
}
