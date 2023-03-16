INCLUDE globals.ink
VAR name = "Elvis"
VAR Affinity = 0
VAR Admiration = 0
VAR Trust = 0

{Affinity > 30: -> HighAffinityQuest}
{Affinity > 15: -> MidAffinityQuest}
{Affinity < 0: -> LowAffinityQuest}

{ CheckAQuest("MetElvis"):
- 0: ->MeetNGreet
- else: ->WaitForResponses
}

//We need to pass MainCharacter's name in here!
=== MeetNGreet ===
Hello kiddo! #speaker: Elvis
You look familiar... #speaker: MainCharacter
~StartAQuest("MetElvis", 1)
->DONE

===LowAffinityQuest===
Kid, perhaps you don't know, but you really need to build a better reputation! People <color=\#F54D1C>GOSSIP</color>. #speaker: Elvis
->DONE
===MidAffinityQuest===
Awesome job kid! I see you already gained some respect in the village.#speaker: Elvis
->DONE
===HighAffinityQuest===
Alright. Alright. Being adorable is important, but try not to steal my thunder. #speaker: Elvis
->DONE
===WaitForResponses===
My charm is irresistible! #speaker: Elvis
->DONE

