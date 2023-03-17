//Timmy's dialogue
INCLUDE globals.ink
VAR name = "Timmy"
VAR Affinity = 0
VAR Admiration = 0
VAR Trust = 0

{ CheckAQuest("RunErrandsForGramma"):
- 0: ->MeetNGreet
- 1: ->RunErrandsForGrammaTwo
- else: ->WaitForResponses
}

=== MeetNGreet ===
 Hi there, what's up? #speaker:Timmy
 + [It's {name}!] Long time no see {name}, miss you so bad! # speaker: MainCharacter #SocialAction: Praise
    I miss you too {GetPlayerName()}! # speaker: Timmy
 + [...Nerd!] (Speaking something offensive to {name}) # speaker: MainCharacter #SocialAction: Insult
    ... #speaker: Timmy
-
-> END

=== mid_affinity ===
Nice to see you again! #speaker: Timmy
-> END

===RunErrandsForGrammaTwo===
Hey {name}! Where have you been?? Gramma just made a <color=\#20C149>super delicious vegetable casserole</color> for you. #speaker: MainCharacter #SocialAction: Help
Is she?? I'm going right away. Thank you! #speaker: Timmy
~StartAQuest("RunErrandsForGramma", 2)
->DONE

===WaitForResponses===
... #speaker: Timmy
->DONE
