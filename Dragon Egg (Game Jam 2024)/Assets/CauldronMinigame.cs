using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CauldronMinigame : MonoBehaviour
{
    [SerializeField] private GateAbility abilityPool;
    
    [SerializeField] private GameObject[] chemicalBottles;

    [SerializeField] private List<GameObject> currentChemicals;

    [SerializeField] private DripSpawnerScript ds;

    [SerializeField] private List<string> recipe;

    [SerializeField] private Material clearWater;
    [SerializeField] private Material badWater;
    [SerializeField] private Material redWater;

    [SerializeField] private GameObject water;

    [SerializeField] private GameObject puddle1;
    [SerializeField] private GameObject puddle2;

    //Initialise Component
    private void Start()
    {
        currentChemicals = new List<GameObject>();
        recipe = new List<string>
        {
            {"Iodine"},
            {"Nitrogen"},
            {"Oxygen"},
            {"Carbon"},
            {"Carbon"},
            {"Hydrogen"}
        };
    }

    //Pour Chemicals Into Cauldron
    public void PourContents(GameObject chemical)
    {
        currentChemicals.Add(chemical);
        //Check if that completed the recipe

        if (currentChemicals.Count == recipe.Count)
        {
            CheckRecipe();
        }
        
    }
    
    //Check For Correct Recipe
    private void CheckRecipe()
    {
        for (int i = 0; i < currentChemicals.Count; i++)
        {
            if (currentChemicals[i].name == recipe[i])
            {
                if (i == 3)
                {
                    //Set the cauldron water colour to purple
                }
                print("Chemical: "+ i + " is Correct!");
                ChangeMat(water, redWater);
                ChangeMat(puddle1, redWater);
                ChangeMat(puddle2, redWater);
                
                abilityPool.SetAbility(Ability.Fireball);
                //Needs to be fireball ability *
                //Set the cauldron water colour to red
            }
            else
            {
                print("Chemical" + i + " is wrong..");
                ChangeMat(water, badWater);
                ChangeMat(puddle1, badWater);
                ChangeMat(puddle2, badWater);
                abilityPool.SetAbility(Ability.Slowness);
            }
        }
    }
    
    //Empty Cauldron Contents
    public void EmptyCauldron()
    {
        currentChemicals.Clear();
        //Set water to clear blue
        ChangeMat(water, clearWater);
        ChangeMat(puddle1, clearWater);
        ChangeMat(puddle2, clearWater);
        abilityPool.SetAbility(Ability.None);
    }
    //Update Cauldron Based On Progress
    
    //Perform Completion Feedback


    private void ChangeMat(GameObject change, Material newMat)
    {
        change.GetComponent<MeshRenderer>().material = newMat;
        ds.dropMat = newMat;
    }
    
    
    //DEBUG
    [ContextMenu("COMPLETE MINIGAME")]
    private void DEBUG()
    {
        water.GetComponent<MeshRenderer>().material = redWater;
        ChangeMat(water, redWater);
        ChangeMat(puddle1, redWater);
        ChangeMat(puddle2, redWater);
        abilityPool.SetAbility(Ability.Fireball);
    }
}
