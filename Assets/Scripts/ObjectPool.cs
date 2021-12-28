using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : MonoBehaviour
{
    [Header("Object Pooling Part")]
    [SerializeField] public Queue<Rigidbody> pooledObjects;
    [SerializeField] private Rigidbody ballPrefab;
    [SerializeField] public int poolSize = 16;
    [SerializeField] public Text poolSizeText;


    private void Awake()
    {
        pooledObjects = new Queue<Rigidbody>();

        //Create the pool with given poolsize and order them
        for (int i = 0; i < poolSize; i++)
        {
            Rigidbody obj = Instantiate(ballPrefab);
            obj.gameObject.SetActive(false);
            pooledObjects.Enqueue(obj);
           
            
        }
    }
    private void Start()
    {
        poolSizeText.text = "Remaining Balls In The Pool:  " + poolSize;
    }
    public Rigidbody GetPooledObject()
    {
        Rigidbody obj = pooledObjects.Dequeue();
        obj.transform.position= Vector3.down;
        obj.gameObject.SetActive(true);
        //pooledObjects.Enqueue(obj);
        
        return obj;

    }
   
}
