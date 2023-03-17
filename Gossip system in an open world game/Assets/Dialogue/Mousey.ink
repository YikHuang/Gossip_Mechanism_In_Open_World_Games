INCLUDE globals.ink
VAR name = "Mousey"
VAR Affinity = 0
VAR Admiration = 0
VAR Trust = 0
// { CheckAQuest("RunErrandsForDousey"):
// - 0: ->MeetNGreet
// - 1: ->RunErrandsForDouseyOne
// - 2: ->RunErrandsForDouseyTwo
// - 4: ->RunErrandsForDouseyFour
// - 5: ->RunErrandsForDouseyFive
// - 11: -> BecameEnemy
// - else: ->WaitForResponses
// }
{Affinity>30: -> HighAffinityQuest}
{Affinity<-30: -> LowAffinityQuest}
{Trust>20: ->HighTrustQuest}

->MeetNGreet
//We need to pass MainCharacter's name in here!
=== MeetNGreet ===
CheeseyCheeseyLemonSqueezy # speaker: Mousey
<color=\#20C149>???</color> #speaker: MainCharacter
What? Can't a mouse love <color=\#F5DA1C>CHEESE</color> ? #speaker: Mousey
->DONE

===HighAffinityQuest===
Ohh! Aren't you the <color=\#F5DA1C>legendary boy</color> that everyone is talking about?  #speaker: Mousey
<color=\#20C149>???</color> #speaker: MainCharacter
->DONE

===HighTrustQuest===
{GetPlayerName()} Here comes my favorite person in town! #speaker: Mousey
+[I love you too!] #speaker: MainCharacter
+[Do I know you??] #speaker: MainCharacter
-
->DONE

===LowAffinityQuest===
I know you!! You are the villain that people talked about! # speaker: Mousey
<color=\#20C149>???</color> #speaker: MainCharacter
->DONE

===WaitForResponses===
(Squeaking) #speaker: Mousey
->DONE

