﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUIKillBadgeManager : MonoBehaviour
{

	public GUIKillBadge killBadge;
	public float LifeTime = 5;
	private List<GUIKillBadge> badgeList = new List<GUIKillBadge> ();

	void Start ()
	{
		badgeList = new List<GUIKillBadge> ();
		//InvokeRepeating ("testText", 1.0f, 1.0f);
	}

	void testText(){
		PushKillText("Sun","Palm","Sniper");

	}

	public void PushKillText (string killer, string victim, string killtype)
	{
		PushText (killer + " <color='#FF9900'>" + killtype + "</color> " + victim);
	}

	public void PushText (string text)
	{
		GameObject obj = (GameObject)GameObject.Instantiate (killBadge.gameObject, Vector3.zero, Quaternion.identity);
		obj.gameObject.transform.SetParent (this.transform);
		GUIKillBadge killbadge = obj.GetComponent<GUIKillBadge> ();
		killbadge.KillText.text = text;
		killbadge.timeTemp = Time.time;
		badgeList.Add (killbadge);
	}

	void Update ()
	{

		RectTransform killBadgeTransform = killBadge.GetComponent<RectTransform> ();

		for (int i = 0; i < badgeList.Count; i++) {

			if (badgeList [i] != null) {
				
				RectTransform rect = badgeList [i].gameObject.GetComponent<RectTransform> ();

				if (rect) {
					rect.anchoredPosition = new Vector2 (-killBadgeTransform.sizeDelta.x / 2, -(((killBadgeTransform.sizeDelta.y * i) + (killBadgeTransform.sizeDelta.y / 2))));
					rect.localScale = killBadge.gameObject.transform.localScale;
				}

				if (Time.time > badgeList [i].timeTemp + LifeTime) {
					GameObject.Destroy (badgeList [i].gameObject);
					badgeList.RemoveAt (i);

				}
			}
		}
	}
}
