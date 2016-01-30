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
		//if (Input.GetKeyDown ("a")) {
			//Debug.Log (GenerateEndText());
		//}
	}

	string GenerateEndText(){
		Objective hat, cake, book, ring, rice, bouquet; 
		olist.TryGetValue (Objectives.HATS, out hat);
		olist.TryGetValue (Objectives.CAKE, out cake);
		olist.TryGetValue (Objectives.BOOK, out book);
		olist.TryGetValue (Objectives.RING, out ring);
		olist.TryGetValue (Objectives.RICE, out rice);
		olist.TryGetValue (Objectives.BOUQUET, out bouquet);

		string s = string.Format ("What a marvelous wedding it was! You gave all the guests fabulous {0}s to wear, filling them with excitement. A {1}, filled with delicious cream, was prepared for everyone to enjoy. The pastor joined the couple in holy matrimony with verses from the {2} and {3}s was exchanged followed by a kiss. The guests cheerfully threw {4} at the couple as they left the church. As a final act, the bride threw the {5} into the crowd as of tradition.",
			hat.objectName, cake.objectName, book.objectName, ring.objectName, rice.objectName, bouquet.objectName);

		return s;
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

	void OnCompleteGame(){
		Debug.LogWarning ("Game complete");
	}

	void UpdateObjectives(){
		string output = "";
		bool complete = true;
		foreach (KeyValuePair<Objectives, Objective> o in olist){
			output += o.Value.Description + " " + (o.Value.complete ? "☑" : "☐") + "\n";
			complete &= o.Value.complete;
		}
		objText.text = output;
		if (complete)
			OnCompleteGame ();
	}
}
