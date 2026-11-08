using System.Collections.Generic;
using UnityEngine;

public class RopeLine : MonoBehaviour
{
    [SerializeField] private List <Transform> startPoint;
    [SerializeField] private List <Transform> endPoints;
    [SerializeField] private List<LineRenderer> lineRenderers;


    private void Awake()
    {
        foreach(LineRenderer line in lineRenderers)
        {
            line.positionCount = 2;
            line.startWidth = 0.06f;
            line.endWidth = 0.06f;
        }
    }

    private void Update()
    {

        for(int i=0; i < lineRenderers.Count;i++)
        {
            lineRenderers[i].SetPosition(0, startPoint[i].position);
            lineRenderers[i].SetPosition(1, endPoints[i].position);
        }
        

    }
}