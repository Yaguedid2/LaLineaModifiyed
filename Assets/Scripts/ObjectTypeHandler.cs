using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ObjectTypeHandler : MonoBehaviour
{
    public static ObjectTypeHandler instance;
    public  DrawableObject[] objects=new DrawableObject[345];
    

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    public struct DrawableObject
    {
        public string className;
        public int index;
        public Categories objectCategories;
    }
    [Serializable]
    public struct Categories
    {
        public bool bigSize;
        public bool admire;
        public bool fly;
        public bool ride;
        public bool faith;
        public bool afraid;
        public bool eat;
        public bool shoot;
        public bool axe;
        public bool backpack;
        public bool enter;
        public bool sleep;
        public bool basket;
        public bool bed;
        public bool sitDown;
        public bool book;
        public bool animalRide;
        public bool drive;
        public bool pray;


    }
}
