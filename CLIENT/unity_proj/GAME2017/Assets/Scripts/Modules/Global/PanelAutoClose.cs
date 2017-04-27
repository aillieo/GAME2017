using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PanelAutoClose : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{

		AddAutoCloseToPanel ();

		AddEventPointerClickToChildren ();


	}

	// Update is called once per frame
	void Update () {

	}

	public void OnScriptControll(BaseEventData arg0)
	{
		//Debug.Log("PanelAutoClose");
		GAME2017.UIManager.Instance.ClosePanel();
	}

	void AddAutoCloseToPanel()
	{
		// add auto-close to panel 
		var trigger = transform.gameObject.GetComponent<EventTrigger>();
		if (trigger == null)
			trigger = transform.gameObject.AddComponent<EventTrigger>();

		// init triggers
		if (trigger.triggers == null) 
			trigger.triggers = new List<EventTrigger.Entry> ();

		EventTrigger.Entry entryPointerClick = null;
		foreach(var e in trigger.triggers)
		{
			if (e.eventID == EventTriggerType.PointerClick) {
				entryPointerClick = e;
				break;
			}
		}

		UnityAction<BaseEventData> callback = new UnityAction<BaseEventData> (OnScriptControll);

		if (null == entryPointerClick) {

			EventTrigger.Entry entry = new EventTrigger.Entry ();
			entry.eventID = EventTriggerType.PointerClick;
			trigger.triggers.Add (entry);

			entry.callback = new EventTrigger.TriggerEvent ();
			entry.callback.AddListener (callback);

		} 
		else {
			if(entryPointerClick.callback == null)
			{
				entryPointerClick.callback = new EventTrigger.TriggerEvent ();
			}
			entryPointerClick.callback.AddListener (callback);
		}

	}

	void AddEventPointerClickToChildren()
	{
		// add pointer-click to all child nodes
		foreach (Transform child in gameObject.transform)  
		{  
			var trigger = child.gameObject.GetComponent<EventTrigger>();
			if (trigger == null)
				trigger = child.gameObject.AddComponent<EventTrigger>();

			// init triggers
			if (trigger.triggers == null) 
				trigger.triggers = new List<EventTrigger.Entry> ();

			EventTrigger.Entry entryPointerClick = null;
			foreach(var e in trigger.triggers)
			{
				if (e.eventID == EventTriggerType.PointerClick) {
					entryPointerClick = e;
					break;
				}
			}

		} 
	}
}
