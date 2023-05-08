# User Stories

## 1. Eingeben von Namen mit Titel und Anrede
Der Anwender soll die Möglichkeit haben Namen mit Titel und Anrede einzugeben (z. B. von einer Visitenkarte), um seine Eingabe in ihre Bestandteile aufspalten zu lassen und so die einzelnen Kontakt-Informationen zu erhalten.
* Prio: *hoch*
* Akzeptanzkriterien:
	* Der Anwender kann eine freie Eingabe machen, die per Knopfdruck/Enter in einen Kontakt getrennt wird.
	* Falls vorhanden wird die freie Eingabe in folgende Bestandteile zerlegt:
		* Anrede
		* Geschlecht
		* Titel
		* Vorname
		* Nachname

## 2. Bestimmung des Geschlechts
Der Anwender soll die Möglichkeit haben, dass basierend auf der Anrede, die in der freien Eingabe erkannt wurde, das Geschlecht bestimmt wird. Dabei soll die Möglichkeit bestehen, dieses manuell anzupassen, um auch nicht-binären Menschen zu entsprechen.
* Prio: *hoch*
* Akzeptanzkriterium:
	* Das Geschlecht kann aus der Eingabe-Anrede ermittelt werden und in die formale Briefanrede überführt werden.


## 3. Generierung von brieflichen Anreden
Der Anwender möchte potenziellen neuen Kontakte Briefe schreiben, für die automatisiert eine Anrede generiert werden soll, basierend auf Anrede und Geschlecht.
* Prio: *Mittel*
* Akzeptanzkriterien:
	* Bei der Auftrennung von freier Eingabe und der Bearbeitung des Kontaktes wird eine standardisierte Briefanrede generiert.
	* Die generierte Briefanrede kann manuell angepasst werden.


## 4. Definieren von weiteren Titeln
Der Anwender soll die Möglichkeit haben innerhalb der Sitzung weitere akademische Titel hinzufügen zu können, um diese bei der Aufspaltung erkennen zu lassen, um nicht von der Anwendungsentwicklung abhängig zu sein.
* Prio: *mittel*
* Akzeptanzkriterien:
	* Der Anwender kann einen neuen Titel eingeben und abspeichern.
	* Der gespeicherte Titel wird im weiteren Verlauf der Sitzung bei der Auftrennung erkannt.

 
## 5. Erkennung von Adels-Titeln
Der Anwender soll die Möglichkeit haben, Adels-Titel in der freien Eingabe erkennen zu lassen, um diese nicht von Hand zu erfassen.
* Prio: *niedrig*
* Akzeptanzkriterium:
	* Aus der freien Eingabe wird der Adels-Titel erkannt und dem Nachnamen zugeordnet.
