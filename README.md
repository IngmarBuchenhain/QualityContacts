# QualityContacts
Einfacher Kontaktsplitter, der einen Namen mit Anrede in seine Bestandteile auftrennen und eine Briefanrede generieren kann. Prüfungsaufgabe für die Vorlesung "Softwarequalität" an der DHBW Stuttgart Campus Horb SoSe 2023

![Screenshot der Eingabe](https://github.com/IngmarBuchenhain/QualityContacts/blob/2289f96f5d3df126c42bebd0a36db0a41a8ea3f0/Documentation/Images/QualityContacts_0.png)

## Schnellstart

* Die ausführbare `QualityContacts.exe`-Datei finden Sie beim neusten [Release](https://github.com/IngmarBuchenhain/QualityContacts/releases/tag/v1.0.0) unter `Assets`. Nach dem Download kann diese lokal ausgeführt werden. Bitte beachten Sie die Systemvoraussetzungen. Ggf. muss die Runtime durch Windows installiert werden ![Runtime-Prompt](https://github.com/IngmarBuchenhain/QualityContacts/blob/cde3c948ddc99dde262f60753136c93fab0f4dc7/Documentation/Images/Runtime_Prompt.png)
* Im Ordner [Documentation](https://github.com/IngmarBuchenhain/QualityContacts/tree/main/Documentation) finden Sie die Dokumentation im PDF-Format sowie als Markdown-Dateien für die Ansicht auf Github.
* Im Ordner [App](https://github.com/IngmarBuchenhain/QualityContacts/tree/main/App) finden Sie den Quellcode der Anwendung. Das Projekt wurde mit Visual Studio 2022 erstellt.

## Systemvoraussetzungen

* Betriebssystem: Windows 10 (64-bit) oder höher.

## Betrieb

Unter `Hilfe und Beschreibung` gibt es eine kurze Erklärung.

### Freie Kontakteingabe
* Im Bereich `Kontakteingabe` kann ein freier Text eingegeben oder per Drag&Drop fallen gelassen werden.
* Falls das Programm schwierige Eingaben erkennt, werden Warnungen unterhalb des Eingabefeldes angezeigt.
* Durch `Enter` oder den Button `Kontakt splitten` wird versucht die Eingabe zu parsen und Kontakt-Informationen zu extrahieren, die im Bearbeitungsbereich angezeigt werden.

### Kontaktbearbeitung
* Hier kann entweder direkt oder nach dem Parsen der Kontakt weiter bearbeitet werden.
* Bei jeder Eingabe findet eine Validierung statt. Warnungen und Fehler werden unterhalb angezeigt.
* Auch hier kann per Drag&Drop oder direkte Eingabe gearbeitet werden.
* Wenn keine Fehler vorliegen, kann über den Button `Speichern` der Kontakt für die laufende Sitzung gespeichert werden. 
* Die erfassten Kontakte werden im unteren Bereich angezeigt.
* Erlaubte Eingaben für Geschlecht: `ohne` (wird automatisch gesetzt wenn leer), `weiblich`, `männlich` und `divers`.
* Für erlaubte Eingaben der anderen Felder schauen Sie bitte in der ausführlichen [Documentation](https://github.com/IngmarBuchenhain/QualityContacts/tree/main/Documentation)

### Titel
* Im Bereich `Titel` werden bekannte akademische Titel angezeigt.
* Hier können auch weitere Titel aufgenommen werden, sodass diese beim Parsen automatisch erkannt werden.
* Falls bei der Kontaktbearbeitung in das Titel-Feld Titel aufgenommen werden, die nicht bekannt sind, werden diese als neuer Titel vorgeschlagen.

![Screenshot mit Warnungen und Fehlern](Documentation/Images/QualityContacts_0.png)

## Dokumentation

Weiterführende Dokumentation finden Sie unter:

* [Ausführliche Release-Notes](https://github.com/IngmarBuchenhain/QualityContacts/blob/01472ce3d41946e3fa34575147105fe39406e675/Documentation/ReleaseNotes.md)

* [Design/Architektur](https://github.com/IngmarBuchenhain/QualityContacts/blob/0b4c99dfb8cfdbb5aa530511bcdbec818ff41a1d/Documentation/Design.md)

* [User Stories](https://github.com/IngmarBuchenhain/QualityContacts/blob/0b4c99dfb8cfdbb5aa530511bcdbec818ff41a1d/Documentation/UserStories.md)

* [Definition of Done](https://github.com/IngmarBuchenhain/QualityContacts/blob/0b4c99dfb8cfdbb5aa530511bcdbec818ff41a1d/Documentation/DefinitionOfDone.md)

* [Testdokumentation](https://github.com/IngmarBuchenhain/QualityContacts/blob/0b4c99dfb8cfdbb5aa530511bcdbec818ff41a1d/Documentation/Tests.md)
