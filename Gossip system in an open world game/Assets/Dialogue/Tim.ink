//Timmy's dialogue
VAR name = "Timmy"

-> main
=== main ===
 Yo pal, what's up? #speaker:Timmy
 + [It's {name}!] Long time no see {name}, miss you so bad! # speaker: MainCharacter #SocialAction: Praise
    I miss you too! # speaker: Timmy
 + [...Nerd!] (Speaking something offensive to {name}) # speaker: MainCharacter #SocialAction: Insult
    ... #speaker: Timmy
-
-> END
