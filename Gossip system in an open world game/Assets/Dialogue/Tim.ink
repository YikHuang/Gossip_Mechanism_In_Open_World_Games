//Timmy's dialogue
INCLUDE globals.ink
VAR name = "Timmy"
VAR Affinity = 0
VAR Admiration = 0
VAR Trust = 0
{Affinity >= 30: ->mid_affinity | -> main}

=== main ===
 Hi there, what's up? #speaker:Timmy
 + [It's {name}!] Long time no see {name}, miss you so bad! # speaker: MainCharacter #SocialAction: Praise
    I miss you too! # speaker: Timmy
 + [...Nerd!] (Speaking something offensive to {name}) # speaker: MainCharacter #SocialAction: Insult
    ... #speaker: Timmy
-
-> END

=== mid_affinity ===
Nice to see you again! #speaker: Timmy


-> END
