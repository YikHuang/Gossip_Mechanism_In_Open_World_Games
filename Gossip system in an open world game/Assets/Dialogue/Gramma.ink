INCLUDE globals.ink
VAR name = "Gramma"
VAR Affinity = 0
VAR Admiration = 0
VAR Trust = 0
// { CheckAQuest("RunErrandsForDousey"):
// - 0: ->MeetNGreet
// - 1: ->RunErrandsForDouseyOne
// - 2: ->RunErrandsForDouseyTwo
// - 3: ->RunErrandsForDouseyThree
// - 11: -> BecameEnemy
// - else: ->WaitForResponses
// }
->MeetNGreet
//We need to pass MainCharacter's name in here!
=== MeetNGreet ===
Hi sweetie, what a nice weather! # speaker: Gramma
{name}!!! Glad to see you alive. # speaker: MainCharacter
{CheckAQuest("RunErrandsForDousey") == 3: ->ChooceAction}
{ CheckAQuest("RunErrandsForGramma"):
- 0: ->RunErrandsForGrammaOne
- 1: ->RunErrandsForGrammaTwo
- else: ->WaitForResponses
}
-> DONE

===ChooceAction===
What are you here for? #speaker: Gramma
~ temp Status =CheckAQuest("RunErrandsForGramma") 
+ [Steal from {name}!] ->RunErrandsForDouseyThree
+ {Status == 0} [Chit-chat with {name}] -> RunErrandsForGrammaOne
+ {Status > 0} [Run errands for {name}] -> RunErrandsForGrammaTwo

->DONE
===RunErrandsForDouseyThree===
(Carefully reach your hand to {name}'s pocket...) #speaker: 
~ temp prob_get_caught = RANDOM(1, 3)
{prob_get_caught == 1: -> GetCaught}
//Successfully steal it!
(Gotcha!) #speaker: 
(Now head back to Doozy for reward) #speaker: 
~StartAQuest("RunErrandsForDousey", 4)
->DONE
===GetCaught===
A ha! Busted! What do you think your're doing? #speaker: Gramma #SocialAction: Insult
~StartAQuest("RunErrandsForDousey", 5)
->DONE

===RunErrandsForGrammaOne===
Have you seen Timmy? I just made a super delicious <color=\#20C149>vegetable casserole</color> and I want him to try it.  #speaker: Gramma
However, I can barely see anything...Can you tell him to come for the dinner? #speaker: Gramma

+[Yes] Of course! I'll find him Gramma :) #speaker: MainCharacter #SocialAction: Bonding
    ~StartAQuest("RunErrandsForGramma", 1)
+[No] Do I look like I have lots of time? #speaker: MainCharacter #SocialAction: Rejection
~StartAQuest("RunErrandsForGramma", 0)
-
->DONE

===RunErrandsForGrammaTwo===
Haven't implemented anything yet.

->DONE
===WaitForResponses===
... #speaker: Gramma
->DONE

