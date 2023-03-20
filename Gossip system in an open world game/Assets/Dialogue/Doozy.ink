INCLUDE globals.ink
VAR name = "Doozy"
VAR Affinity = 0
VAR Admiration = 0
VAR Trust = 0
{ CheckAQuest("RunErrandsForDousey"):
- 0: ->MeetNGreet
- 1: ->RunErrandsForDouseyOne
- 2: ->RunErrandsForDouseyTwo
- 4: ->RunErrandsForDouseyFour
- 5: ->RunErrandsForDouseyFive
- 11: -> BecameEnemy
- else: ->WaitForResponses
}

//We need to pass MainCharacter's name in here!
=== MeetNGreet ===
(smirking) # speaker: Doozy
(What a strange guy...) #speaker: MainCharacter
~StartAQuest("RunErrandsForDousey", 1)
-
->RunErrandsForDouseyOne

===RunErrandsForDouseyOne===
Hey you! Wanna earn some extra money? # speaker: Doozy
+ Sure [why not?] what is it? #speaker: MainCharacter #SocialAction: Bonding 
    ~StartAQuest("RunErrandsForDousey", 2)
    -> RunErrandsForDouseyTwo
+ [Think it over] Maybe let me think for a bit... #speaker: MainCharacter #SocialAction: Rejection 
->DONE

===RunErrandsForDouseyTwo===
Here is the thing. See that <color=\#F54D1C>Gramma</color> over there?  # speaker: Doozy
She took my precious <color=\#20C149>Necklace</color> and refused to return! What an old cunning fox... # speaker: Doozy
I want you to steal it for me. I'll reward you with $500. What do you say?# speaker: Doozy

+ [Yes] If it's like you said, I'll help you out. #speaker: MainCharacter #SocialAction: Bonding 
    I know you are a good guy! Listen up. She is half blind and half deaf, you have a good chance not to get caught. Just go to her! #speaker: Doozy
    ~StartAQuest("RunErrandsForDousey", 3)
    //-> RunErrandsForDouseyThree
+ [Let me talk to Gramma first] I'm pretty sure it's a misunderstanding. Let me talk to her. #speaker: MainCharacter #SocialAction: Rejection
    No no no!! She woudln't listen. Just don't talk to her #speaker: Doozy
+ [Stealing? Definitely no!] Can't believe you ask me to do that! Who is the cunning fox now?? #speaker: MainCharacter #SocialAction: Rejection #SocialAction: Insult
    ~StartAQuest("RunErrandsForDousey", 11)
-
-> DONE

===BecameEnemy===
Get out of my face! Never let me see you again!  # speaker: Doozy
-> DONE

===RunErrandsForDouseyFour=== //successfully stole it
{name} I got it I got it!! She didn't even notice just like you said. #speaker: MainCharacter #SocialAction: help
Great job kid! Now we're best buddies! Let me know if you need anything in the future. # speaker: Doozy
~StartAQuest("RunErrandsForDousey", 12)
->DONE

===RunErrandsForDouseyFive=== //failed to steal
Sorry {name}, I failed...  #speaker: MainCharacter #SocialAction: Insult
I knew it!! Shoud have done it myself! # speaker: Doozy
Do you want to try again?  # speaker: God
+[Yes]
~StartAQuest("RunErrandsForDousey", 1)
+[No]
~StartAQuest("RunErrandsForDousey", 12)
-
->DONE
===WaitForResponses===
(Smirking) #speaker: Doozy
->DONE

