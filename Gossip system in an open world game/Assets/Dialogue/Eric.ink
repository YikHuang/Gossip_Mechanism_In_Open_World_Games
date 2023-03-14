INCLUDE globals.ink
VAR name = "Eric"
VAR Affinity = 0
VAR Admiration = 0
VAR Trust = 0
{ CheckAQuest("MakeUpWithClaire"):
- 0: ->MeetNGreet
- 2: ->MakeUpWithClaireThree
- else: ->WaitForResponses
}

//We need to pass MainCharacter's name in here!
=== MeetNGreet ===
{name}! I finally got time to come back and checked everyone out. #speaker:MainCharacter
How is everyone doing? #speaker:MainCharacter
What a surprise! Well you know, Gramma's dementia got worse... #speaker:Eric

+ [Show some sympathy] That is not good. Will check her out later! Hope she still remembers me. # speaker: MainCharacter #SocialAction: Greeting

+ [Tell the cold truth] Who knows? It might be a blessing in disguise. Doesn't she always complain about what you did? Now she can't hold you back anymore. # speaker: MainCharacter #SocialAction: StraightForward
    That is ... can't say you are wrong. #speaker: Eric
-
-> MakeUpWithClaireOne

===MakeUpWithClaireOne===
Besides that, I'm actually having a disagreement with <color=\#F54D1C>Claire</color>. She is so pissed and wouldn't talk to me. Can you talk to her? #speaker: Eric

+ [Yes] I will talk to her right away! Thanks for your trust. #speaker: MainCharacter #SocialAction: Bonding 
     ~StartAQuest("MakeUpWithClaire", 1)
     

+ [No] It's not like I don't want to help, but it's a bit awakward... You should talk to her yourself. #speaker: MainCharacter #SocialAction: Rejection
-
-> DONE

===MakeUpWithClaireThree===
Hey {name}, I just talked to Claire. She's deeply regretful and she want you to have this. #speaker: MainCharacter
{Affinity >=50: ->Three_HighAffinity|->Three_LowAffinity}

===Three_HighAffinity===
#SocialAction: Help
Wow isn't that the superb "Diablo IV beta early access code"? I've  wanted it for so long... # speaker: Eric
Thank you so much!! Please tell her I love her, and meet me at our spot at dawn. # speaker: Eric
~StartAQuest("MakeUpWithClaire", 3)
->DONE
===Three_LowAffinity===
I appreciate her gift, but the fact she send you here only proves she doesn't care about me at all! #speaker: Eric
<color=\#F5DA1C>Hint: Gain more affinity with {name} to proceed the story!</color> #speaker:God

+[Restart the quest]
~StartAQuest("MakeUpWithClaire", 0)

->DONE
===WaitForResponses===
... #speaker: Eric
->DONE

