INCLUDE globals.ink
{InitalJohnMATESDialogue == "" : -> Intro | -> Mood }
== Intro ==
...
-> DONE

==Mood==
{InitalJohnMATESDialogue == "true" : -> Happy | -> Sad}

==Happy==
im happy
-> DONE

==Sad==
im sad
-> DONE
