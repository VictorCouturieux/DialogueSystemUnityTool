using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

	public string name;
	public float waitCharSentence = 0.1f;

	[TextArea(3, 10)]
	public string[] sentences;

}
