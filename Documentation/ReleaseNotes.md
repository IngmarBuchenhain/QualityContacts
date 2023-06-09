# Release Notes

Dieses Dokument enthält die momentan unterstützten Features, Einschränkungen und Nutzungshinweise.

## Features
* Auftrennen einer freien Texteingabe in Kontaktinformationen.
* Erkennung von Anrede, akademischen Titeln und Adelstitel.
* Erkennung von umgedrehter Eingabe von Nachname und Vorname, wenn der Nachname mit `,` endet (z. B. `Mustermann, Max`).
* Zusätzliche grundlegende Erkennung der Anreden in den Sprachen Englisch, Italienisch, Spanisch und Französisch.
* Automatische Generierung einer Briefanrede.
* Bearbeitungsmöglichkeit für Kontaktinformationen.
* Möglichkeit neue akademische Titel temporär hinzuzufügen.
* Vorschlag neue akademische Titel hinzuzufügen, wenn diese bei den Kontaktinformationen erkannt werden.
* Möglichkeit Kontaktinformationen temporär zu speichern.
* Komfortfunktionen, wie Drag&Drop, Auftrennen per `Enter`-Taste.
* Validierung der Kontaktinformationen vor Speicherung und Anzeige der Fehler.

## Einschränkungen/Nutzungshinweise
* Freie Eingabe:
	* Es wird davon ausgegangen, dass einzelne Kontaktinformationen durch Leerzeichen getrennt sind.
	* Einzelnes Wort: Dieses wird als Nachname genommen, selbst, wenn es einer Anrede, Titel, usw. entspricht.
	* Adelstitel werden dem Nachname zugeordnet.
	* Falls Adels-Präfixe/Suffixe erkannt werden, werden alle folgenden Wörter dem Nachname zugeordnet, auch wenn Anrede, akademische Titel oder Komma-getrennter Nachname enthalten sind.
	* Das letzte Wort wird als Nachname erkannt.
	* Falls noch keine Anrede erkannt wurde, wird dies akademischen Titeln vorgezogen (falls ein Titel hinzugefügt wird, der einer Anrede entspricht.)
	* Falls eine umgedrehte Eingabe erkannt wurde, wird eine weitere umgedrehte Eingabe als Vorname behandelt und eine Warnung angezeigt.
	* Titel werden nach Möglichkeit der Wichtigkeit nach geordnet. Das kann ggf. zu unerwünschten Ergebnissen führen, die von Hand korrigiert werden können.
* Registrierte Daten:
	* Erlaubte Geschlechter: `männlich`, `weiblich`, `divers`, `ohne`
	* Erlaubte Anreden: `Herr`, `Frau`, `Mrs`, `Mr`, `Ms`, `Signora`, `Signor`, `Sig.`, `Mme`, `M`, `Señora`, `Señor`, ``
		* Die englische Anrede `Mrs` wird zwar erkannt, aber in der Briefanrede in `Ms` gewandelt, da `Mrs` nicht mehr gebräuchlich ist.
	* Erkannte akademische Titel: `Professorin`, `Professor`, `Prof.`, `Dr.`, `Dr.-Ing.`, `rer.`, `nat.`, `mult.`, `h.c.`, `Dipl.-Ing.`, `Dipl.`, `Ing.`, `B.S.`, `M.S.`, `B.A.`, `M.A.` und selbst hinzugefügte.
	* Erkannte Adels-Titel: `Prinz`, `Prinzessin`, `Sir`, `Dame`, `Freiherrin`, `Freiherr`, `Baron`, `Baronesse`, `Ritter`, `Graf`, `Gräfin`, `Fürst`, `Fürstin`, `Markgraf`, `Pfalzgraf`, `Landgraf`, `Herzog`, `Herzogin`, `Kurfürst`, `Großherzog`, `Erzherzog`, `König`, `Königin`
	* Erkannte akademische Präfixe/Suffixe: `von`, `vom`, `van`, `de`, `zu`, `zur`
* Kontaktbearbeitung:
	* Beim Auftrennen wird, falls eine Anrede erkannt wird, das Geschlecht bestimmt. Wenn die Anrede in den Kontaktinformationen bearbeitet wird, wird das Geschlecht nicht automatisch angepasst, um auch den Eintrag `divers` zu ermöglichen, da ohne Anrede standardmäßig ein leeres Geschlecht gesetzt wird.
	* Bei der Briefanrede wird 
		* ohne Geschlecht eine allgemeine Anrede verwendet, 
		* bei `divers` eine Anrede mit Vorname und Nachname verwendet,
		* bei `männlich` oder `weiblich` der Vorname weggelassen.
		* bei Titeln nur der erste verwendet.
		* ACHTUNG: Sobald ein Feld in den Kontaktinformationen geändert wird, wird die Briefanrede neu generiert!
* Speicherung:
	* In diesem Prototyp werden die Daten nur temporär während der Laufzeit des Programms gespeichert.
