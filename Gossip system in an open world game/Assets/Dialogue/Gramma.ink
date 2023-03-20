INCLUDE globals.ink
VAR name = "Gramma"
VAR Affinity = 0
VAR Admiration = 0
VAR Trust = 0
//VAR status = 
->MeetNGreet
//We need to pass MainCharacter's name in here!
=== MeetNGreet ===
Hi sweetie, what a nice weather! # speaker: Gramma
{name}!!! Glad to see you alive. # speaker: MainCharacter
{CheckAQuest("RunErrandsForDousey") == 3: ->ChooceAction}
{ CheckAQuest("RunErrandsForGramma"):
- 0: ->RunErrandsForGrammaOne
- 2: ->RunErrandsForGrammaThree
- else: ->WaitForResponses
}
-> DONE

===ChooceAction===
~temp status = CheckAQuest("RunErrandsForGramma")
What are you here for? #speaker: Gramma
+ [Steal from {name}!] 
    ->RunErrandsForDouseyThree
+ {status == 0} [Chit-chat with {name}] 
    -> RunErrandsForGrammaOne
+ {status== 2} [Run errands for {name}] 
    -> RunErrandsForGrammaThree
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

===RunErrandsForGrammaThree===
Thanks for your help sweetie. Wanna try some <color=\#20C149>casserole</color>? #speaker: Gramma #SocialAction: Help 
~StartAQuest("RunErrandsForGramma", 3)
->DONE

===WaitForResponses===
... #speaker: Gramma
->DONE

