using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class TestMessage : MonoBehaviour
{
    public DialogManager DialogManager;

    public GameObject[] Example;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("Your haircut looks so/speed:down//speed:down/..../click/ /speed:init/stupid", "Li"));

        dialogTexts.Add(new DialogData("How about you go and /color:pink//size:down//size:down/ fuck /size:init/ /color:white/ yourself, Jen. You guys are the ones who pressured me to do it!", "Sa"));
        
        dialogTexts.Add(new DialogData("Ah! You haven't forgotten your promise, right? The /color:green/Swear jar /color:white/ is waiting, Zack!", "Li"));

        dialogTexts.Add(new DialogData("One day you'll all realize how stupid it was to push this stupid swear jar thing on me. I'm a grown a-... I'm a fully grown man for cryin' out loud!", "Li"));

        dialogTexts.Add(new DialogData("Ok guys, you can mock Zack all you want at the party, if we don't get goin' now we're gonna be late!", "Li"));

        dialogTexts.Add(new DialogData("Atleast somebody'ds minimally sane in our friend group. Let's just get going, I'm getting weird looks from everybody here.", "Li"));

        dialogTexts.Add(new DialogData("If you need an emphasis effect, /wait:0.5/wait... /click/or click command.", "Li", () => Show_Example(3)));

        dialogTexts.Add(new DialogData("Text can be /speed:down/slow... /speed:init//speed:up/or fast.", "Li", () => Show_Example(4)));

        dialogTexts.Add(new DialogData("You don't even need to click on the window like this.../speed:0.1/ tada!/close/", "Li", () => Show_Example(5)));

        dialogTexts.Add(new DialogData("/speed:0.1/AND YOU CAN'T SKIP THIS SENTENCE.", "Li", () => Show_Example(6), false));

        dialogTexts.Add(new DialogData("And here we go, the haha sound! /click//sound:haha/haha.", "Li", null, false));

        dialogTexts.Add(new DialogData("That's it! Please check the documents. Good luck to you.", "Sa"));

        DialogManager.Show(dialogTexts);
    }

    private void Show_Example(int index)
    {
        Example[index].SetActive(true);
    }
}
