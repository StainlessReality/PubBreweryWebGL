using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour 
{
	private RaycastHit hit; // will store information about what has been clicked
	private Animator anim; //will represent the animator component of the clicked object
	private bool haveHitSomething; //answer to whether something is hit
	private string gameObjectName;//will store the name of the object with animation controller
	private string triggerName;//will store the name of the animator controller trigger parameter to be fired
	private Transform objectWithAnimCtrler;// wiss store the transform of the object with animation controller
    private int noOfTriggers;
    private AnimatorControllerParameter[] triggers;

	// Update is called once per frame
	void Update () 
	{
        //If the LMB has been pressed, shoot a ray and trigger appropriate action
		if (Input.GetMouseButtonDown(0))
        {
			//This boolean holds the answer to whether a collider has been hit
			haveHitSomething = CastRay();

			ExecuteClickAction();
		}
	}

	private bool CastRay()
    {
		//Cast a ray at the mouse position
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//Check if the ray has hit something. The answer is stored in haveHitSomething
		//The object it hits is stored using 'hit'
		haveHitSomething = Physics.Raycast(ray, out hit, 100.0f);
		return haveHitSomething;
    }
	private void ExecuteClickAction()
    {
        if (haveHitSomething)
        {
            
			if (hit.transform != null) //Does the thing you clicked have a transform?
            {
				print("you hit the collider " + hit.transform.gameObject.name + " which has a transform component");
				
				if (hit.transform.GetComponent<Animator>() != null) //Does it also have an Animator component?
                {
					objectWithAnimCtrler = hit.transform;
					print("the collider you hit (" + hit.transform.gameObject.name + ") has a transform and an animator component");
					TriggerAnimation();
				}
				else if(hit.transform.parent != null) //Does it have a parent?
				{
                    if (hit.transform.parent.GetComponent<Animator>() != null) //Does the parent have an animator?
                    {
						objectWithAnimCtrler = hit.transform.parent; //Get the parent of the object we just tried
						print("the thing you hit ("+ hit.transform.gameObject.name + ") has a transform, but no animator component. However, its first parent " + objectWithAnimCtrler.gameObject.name + " does!");
						TriggerAnimation();
                    }
                    else
                    {
						print("the parent has no animator");
                        if (hit.transform.parent.parent != null) //Does it have a second parent?
                        {
                            if (hit.transform.parent.parent.GetComponent<Animator>() != null) //Does the second parent have an animator?
                            {
                                objectWithAnimCtrler = hit.transform.parent.parent; //Get the parent of the object we just tried
                                print("the thing you hit (" + hit.transform.gameObject.name + ") has a transform, but no animator component. However, its second parent " + objectWithAnimCtrler.gameObject.name + " does!");
                                TriggerAnimation();
                            }
                            else
                            {
                                print("the second parent has no animator");
                                if (hit.transform.parent.parent.parent != null) //Does it have a third parent?
                                {
                                    if (hit.transform.parent.parent.parent.GetComponent<Animator>() != null) //Does the third parent have an animator?
                                    {
                                        objectWithAnimCtrler = hit.transform.parent.parent.parent; //Get the parent of the object we just tried
                                        print("the thing you hit (" + hit.transform.gameObject.name + ") has a transform, but no animator component. However, its third parent " + objectWithAnimCtrler.gameObject.name + " does!");
                                        TriggerAnimation();
                                    }
                                    else
                                    {
                                        print("the third parent has no animator");
                                        if (hit.transform.parent.parent.parent.parent != null) //Does it have a fourth parent?
                                        {
                                            
                                            if (hit.transform.parent.parent.parent.parent.GetComponent<Animator>() != null) //Does the fourth parent have an animator?
                                            {
                                                objectWithAnimCtrler = hit.transform.parent.parent.parent.parent; //Get the parent of the object we just tried
                                                print("the thing you hit (" + hit.transform.gameObject.name + ") has a transform, but no animator component. However, its fourth parent " + objectWithAnimCtrler.gameObject.name + " does!");
                                                TriggerAnimation();
                                            }
                                            else
                                            {
                                                print("the fourth parent has no animator");
                                            }
                                        }
                                        else
                                        {
                                            print("it has no fourth parent");
                                        }
                                    }
                                }
                                else
                                {
									print("it has no third parent");
								}
                            }
                        }
                        else
                        {
							print("it has no second parent");
						}
                    }
				}
                else
                {
					print("the collider you hit has a tranform, but neither it nor any of its parents have an animator component");
                }
			}
            else
            {
				print("the collider you hit has no transform component");
            }
        }
        else
        {
			print("no collider has been hit by raycast");
        }
    }
	private void TriggerAnimation()
	{
		gameObjectName = hit.transform.gameObject.name;//Store name of the collider's object
		anim = objectWithAnimCtrler.GetComponent<Animator>(); //Store the object's animator component
		triggerName = gameObjectName + "_clicked"; //Build the relevant animator controller trigger parameter name
        triggers = anim.parameters;
        foreach (var trigger in triggers)
        {
            if (trigger.name == triggerName)
            {
                print("firing " + triggerName);
                anim.SetTrigger(triggerName); //Fire the parameter in the appropriate animator component
            }
            else
            {
                print(objectWithAnimCtrler.gameObject.name + " has no parameter called " + triggerName);
            }
        }
	}
}
