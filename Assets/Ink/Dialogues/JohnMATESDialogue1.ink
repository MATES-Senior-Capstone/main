INCLUDE globals.ink
{InitalJohnMATESDialogue == "" : -> Intro | -> Tail1 }
==Intro==
Hello I am John MATES! The creator of MATES!
-> MATESEx

== MATESEx==
MATES is a school for gifted students who want to learn about the Marine sciences and Technology!
-> HPJoke

==HPJoke==
It's like Hogwarts but for nerds!
-> MATESLOVE

==MATESLOVE==
How much do you love MATES?

* I love MATES!
    Huzzah!
    -> TASK1
* I hate MATES!
    Bro why are you here?{InitalJohnMATESDialogue}
    -> DONE
    
==TASK1==
~ InitalJohnMATESDialogue = "true"
Say my dear chap, why don't you do me a favor ask another MATES teacher when our PTSO meeting is?
-> DONE

==Tail1==
Have you asked when the PTSO meeting is?
-> DONE