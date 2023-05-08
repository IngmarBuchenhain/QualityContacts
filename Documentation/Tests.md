# Tests

Getestet werden die öffentlichen Schnittstellen des Backends, da diese die eigentliche relevante Funktionalität kapseln. Zusätzlich wird die Schnittstelle für die Briefanreden-Generierung getestet, da diese hohe relevanz hat, diese ist jedoch Backend-Intern. Die Schnittstelle zum Daten-Repository wird nicht getestet, da diese nicht auf eine echte Datenschicht verweist, sondern während der Prototyping-Phase diese direkt implementiert, und anschließend getauscht wird (dort sollten dann Tests stattfinden).

## Unit Tests
Getestet wird also:

* ContactParser.ParseContactFreeInput(string contactCandidate)
* ContactValidator.Validate(string input)
* ContactValidator.Validate(IContact contact)
* SalutationService.GenerateLetterSalutation(IContact contact)

## Manuelle Tests
Zusätzlich wird ein manueller Testlauf mit den Beispielen des Kunden durchgeführt.

## Ergebnisse Unit Tests

## Ergebnisse manuelle Tests

| Eingabe | Erwarteter Vorname | Erwarteter Nachname | Erwartete Titel | Erwartetes Geschlecht | Erwartete Anrede | Testergebnis |
| - | - | - | - | - | - | - |
Frau Sandra Berger | Sandra | Berger | --- | weiblich | Frau | ✅ |
Herr Dr. Sandro Gutmensch | Sandro | Gutmensch | Dr. | männlich | Herr | ✅ |
Professor Heinreich vom Wald | Heinreich | vom Wald | Professor | ohne | --- | ✅ |
Mrs. Doreen Faber | Doreen | Faber | --- | weiblich | Ms | ✅ |
Mme. Charlotte Noir | Charlotte | Noir | --- | weiblich | Mme | ✅ |
Frau Prof. Dr. rer. nat. Maria von Leuthäuser-Schnarrenberger | Maria | von Leuthäuser-Schnarrenberger | Prof. Dr. rer. nat. | weiblich | Frau | ✅ |
Herr Dipl. Ing. Max von Müller | Max | von Müller | Dipl. Ing. | männlich | Herr | ✅ |
Dr. Russwurm, Winfried | Winfried | Russwurm | Dr. | ohne | --- | ✅ |
Herr Dr.-Ing. Dr. rer. nat. Dr. h.c. mult. Paul Steffens | Paul | Steffens | Dr. Dr. Dr.-Ing. rer. nat. h.c. mult. | männlich | Herr | ✅ |

<!-- ❌  oder ✅ -->

