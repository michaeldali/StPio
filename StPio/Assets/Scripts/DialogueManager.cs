using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // private Queue<string> sentences;
    private string currentText;
    private Stack<string> forward;
    private Stack<string> backward;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI dialogueText;
    public Controller player;

    // Start is called before the first frame update
    void Start()
    {
        forward = new Stack<string>();
        backward = new Stack<string>();
        currentText = "";
        //sentences = new Queue<string>();
    }


    public void StartDialogue (Dialogue dialogue)
    {
        dateText.text = dialogue.date;
        forward.Clear();
        for (int i=dialogue.sentences.Length-1; i>-1; i--)
        {
            forward.Push(dialogue.sentences[i]);
        }

        DisplayNextSentence();
    }

    // Called from continue button
    public void DisplayNextSentence()
    {
        if (forward.Count == 0)
        {
            EndDialogue();
            return;
        }
        // Grab the next sentence
        string sentence = forward.Pop();
        
        // Send current sentence to the backward set
        backward.Push(currentText);
        // Set current text equal to the next sentence
        currentText = sentence;
        // Display next sentence
        dialogueText.text = currentText;
    }

    // Called from backward button
    public void DisplayPreviousSentence()
    {
        // Grab the previous sentence
        string sentence = backward.Pop();
        if (sentence == "")
        {
            return;
        }
        // Send current sentence to the forward set
        forward.Push(currentText);
        // Set current text equal to the previous sentence
        currentText = sentence;
        // Display prev sentence
        dialogueText.text = currentText;
    }

    void EndDialogue()
    {
        player.isReading = false;
    }


}
