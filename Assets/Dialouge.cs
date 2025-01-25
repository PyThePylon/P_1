using UnityEngine;

public class Dialouge : MonoBehaviour
{
    public void PlayTauntDialogue()
    {
        string[] taunts = {
            "Is that all you’ve got? Pathetic!",
            "You can’t hide forever. I’ll always find you.",
            "Oh, running now? How adorable!",
            "What’s the matter? Scared?",
            "No escape for you. Not now, not ever!"
        };

        PlayRandomDialogue(taunts);
    }

    public void PlaySearchDialogue()
    {
        string[] searchingLines = {
            "Where are you? I can smell your fear...",
            "You can’t hide forever. I’m getting closer!",
            "Come out, come out, wherever you are...",
            "Hiding is pointless. I’ll find you eventually.",
            "Don’t worry, I’ll be gentle... Maybe."
        };

        PlayRandomDialogue(searchingLines);
    }

    public void PlayStalkDialogue()
    {
        string[] stalkingLines = {
            "I see you~... Don’t turn around!",
            "You’re making this too easy for me.",
            "Keep running. I love a good chase!",
            "Oh, what a shame... You’re so slow!",
            "You feel it, don’t you? The inevitability..."
        };

        PlayRandomDialogue(stalkingLines);
    }

    private void PlayRandomDialogue(string[] lines)
    {
        int randomIndex = Random.Range(0, lines.Length);
        Debug.Log(lines[randomIndex]);
    }
}
