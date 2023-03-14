INCLUDE globals.ink
VAR name = "Claire"
VAR Affinity = 0
VAR Admiration = 0
VAR Trust = 0

// Call external function to check if player accept the quest from Eric
{ CheckAQuest("MakeUpWithClaire"):
- 1: ->MakeUpWithClaireTwo
- 3: ->MakeUpWithClaireFour
- else: ->WaitForResponses
}

//Call external function to check if enters quest two

//We need to pass MainCharacter's name in here!
===WaitForResponses===
... #speaker: Claire
->DONE

=== MakeUpWithClaireTwo ===
{name}!! Long time no see! You are still pretty and shine. #speaker:MainCharacter
Thanks! #speaker: Claire

(It seems like a good time to ask {name} about Eric! How do you want to approach?) #speaker:God
+ [Ask {name} straightforwardly]
    I heard from Eric that you guys are having a fight? What happened? #speaker:MainCharacter #SocialAction: StraightForward
        ... #speaker: Claire
    
+ [Ask {name} carefully]
    Hey... I haven't met Eric ever since I came back. How is he? #speaker:MainCharacter #SocialAction: Bonding
    ({name} looks like she is about to cry) # speaker:

- It's kinda embarrassed talking about it. Yesterday we have a discussion on "Who should replace the toilet paper roll? The person who finishes it or the person who starts it.". #speaker: Claire
It started as a discussion and later it became an argument... #speaker: Claire
Now I feel silly fighting over things like this, but I don't want to appologize either... #speaker: Claire
To be honest that is actually an interesting question to discuss about. # speaker: MainCharacter
I understand you need an out. Is there anything I can help? # speaker: MainCharacter
Actually there is. If you can bring this "Diablo IV beta early access code" to Eirc, maybe he will forgive me. #speaker: Claire

+ [Willingly accept!] Of course! No worries, we can fix this! #speaker: MainCharacter #SocialAction: Bonding
    ~StartAQuest("MakeUpWithClaire", 2) 

+ [I'm so tired of walking between you two!] A "Diablo IV beta early access code"? I'm gonna keep it myself. Thanks!  #speaker: MainCharacter #SocialAction: Rejection
    ... #speaker: Claire
-
-> DONE

=== MakeUpWithClaireFour ===
#SocialAction: Help
He really loved your gift. We did it! He also wants you to meet him at "your spot" at down. #speaker: MainCharacter
Can't thank you enough...Let me know if you need anything! #speaker: Claire
~StartAQuest("MakeUpWithClaire", 5) 
-
->END
