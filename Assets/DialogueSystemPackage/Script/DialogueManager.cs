using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    public float waitCharSentence = 0.1f;
    
    public GameObject continueButton;

    private WaitForSeconds waitCharacterSentence;

    private bool scribingSentenceWaiting;

    private Queue<string> sentences;


    // Use this for initialization
    void Start() {
        sentences = new Queue<string>();
        waitCharacterSentence = new WaitForSeconds(waitCharSentence);
    }

    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

        FindObjectOfType<EventSystem>().SetSelectedGameObject(continueButton);

    }

    public WaitForSeconds waitCharaSentence() {
        if (scribingSentenceWaiting) 
            return waitCharacterSentence;
        else 
            return new WaitForSeconds(-1);
    }
    
    public void DisplayNextSentence() {
        if (scribingSentenceWaiting == false) {
            if (sentences.Count == 0) {
                EndDialogue();
                return;
            }
            scribingSentenceWaiting = true;
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        else {
            scribingSentenceWaiting = false;
        }
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            if (letter != ' ')
                yield return waitCharaSentence();
            else
                yield return null;
        }
        scribingSentenceWaiting = false;
    }

    void EndDialogue() {
        animator.SetBool("IsOpen", false);
    }
}