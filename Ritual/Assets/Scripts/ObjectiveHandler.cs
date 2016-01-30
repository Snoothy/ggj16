using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObjectiveHandler : MonoBehaviour {

	public enum Objectives {CAKE, HATS, RICE, BOUQUET, RING, BOOK};

	Text objText;

	struct Objective {
		public string Description;
		public bool complete;
		public string objectName;

		public Objective(string d){
			Description = d;
			complete = false;
			objectName = "Nothing";
		}
	};


	Dictionary<Objectives, Objective> olist = new Dictionary<Objectives, Objective>();

	// Use this for initialization
	void Start () {
		olist.Add(Objectives.CAKE, new Objective ("Prepare a cake at the dining table"));
		olist.Add(Objectives.HATS, new Objective ("Put festive hats on the guests"));
		olist.Add(Objectives.RICE, new Objective ("Put rice in the empty rice bag"));
		olist.Add(Objectives.BOUQUET, new Objective ("Bring a bouquet for the bride"));
		olist.Add(Objectives.RING, new Objective ("The groom is missing the ring"));
		olist.Add(Objectives.BOOK, new Objective ("The pasto forgot to bring is own book"));

		objText = transform.FindChild ("Objectives").GetComponent<Text> ();
		UpdateObjectives ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("a")) {
			CompleteObjective (Objectives.BOOK, "Bible");
		}
	}

	public void CompleteObjective(Objectives type, string oName){
		Objective tmp;
		if (olist.TryGetValue (type, out tmp)) {
			tmp.complete = true;
			tmp.objectName = oName;
			olist.Remove (type);
			olist.Add (type, tmp);
			UpdateObjectives ();
		}
	}

	void UpdateObjectives(){
		string output = "";
		foreach (KeyValuePair<Objectives, Objective> o in olist){
			output += o.Value.Description + " " + (o.Value.complete ? "☑" : "☐") + "\n";
		}
		objText.text = output;
	}
}
